using System.Buffers.Binary;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace CsRestbl.Managed;

public class Restbl
{
    public unsafe static Restbl FromBinary(ReadOnlySpan<byte> data)
    {
        if (!data[0..6].SequenceEqual("RESTBL"u8)) {
            throw new InvalidDataException("Invalid RESTBL magic");
        }

        Restbl restbl = new() {
            Unknown1 = BinaryPrimitives.ReadUInt32LittleEndian(data[6..10]),
            Unknown2 = BinaryPrimitives.ReadUInt32LittleEndian(data[10..14])
        };

        uint crcTableCount = BinaryPrimitives.ReadUInt32LittleEndian(data[14..18]);
        uint nameTableCount = BinaryPrimitives.ReadUInt32LittleEndian(data[18..22]);

        int offset = 22;

        for (int i = 0; i < crcTableCount; i++) {
            restbl.CrcTable.Add(new(
                BinaryPrimitives.ReadUInt32LittleEndian(data[offset..(offset += 4)]),
                BinaryPrimitives.ReadUInt32LittleEndian(data[offset..(offset += 4)])
            ));
        }

        for (int i = 0; i < nameTableCount; i++) {
            ReadOnlySpan<byte> raw = data[offset..(offset += 160)];
            fixed (byte* ptr = raw) {
                restbl.NameTable.Add(new(
                    Utf8StringMarshaller.ConvertToManaged(ptr)!,
                    BinaryPrimitives.ReadUInt32LittleEndian(data[offset..(offset += 4)])
                ));;
            }
        }

        return restbl;
    }

    public byte[] ToBinary()
    {
        byte[] src = new byte[0x16 + (CrcTable.Count * 0x8) + (NameTable.Count * 0xA4)];
        MemoryStream ms = new(src);

        Span<byte> dword = stackalloc byte[4];

        ms.Write("RESTBL"u8);

        BinaryPrimitives.WriteUInt32LittleEndian(dword, Unknown1);
        ms.Write(dword);

        BinaryPrimitives.WriteUInt32LittleEndian(dword, Unknown2);
        ms.Write(dword);

        BinaryPrimitives.WriteInt32LittleEndian(dword, CrcTable.Count);
        ms.Write(dword);

        BinaryPrimitives.WriteInt32LittleEndian(dword, NameTable.Count);
        ms.Write(dword);

        foreach (var entry in CrcTable.OrderBy(x => x.Hash)) {
            BinaryPrimitives.WriteUInt32LittleEndian(dword, entry.Hash);
            ms.Write(dword);

            BinaryPrimitives.WriteUInt32LittleEndian(dword, entry.Size);
            ms.Write(dword);
        }

        foreach (var entry in NameTable.OrderBy(x => x.Name)) {
            ms.Write(Encoding.UTF8.GetBytes(entry.Name.PadRight(160, '\0')));

            BinaryPrimitives.WriteUInt32LittleEndian(dword, entry.Size);
            ms.Write(dword);
        }

        return src;
    }

    public uint Unknown1 { get; set; }
    public uint Unknown2 { get; set; }

    public ObservableCollection<CrcEntry> CrcTable { get; set; } = new();
    public ObservableCollection<NameEntry> NameTable { get; set; } = new();
}
