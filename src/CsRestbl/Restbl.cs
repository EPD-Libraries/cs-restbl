using Microsoft.Win32.SafeHandles;
using Native.IO.Handles;
using System.Runtime.InteropServices;

namespace CsRestbl;

public unsafe partial class Restbl : SafeHandleZeroOrMinusOneIsInvalid
{
    [LibraryImport("cs_restbl")]
    private static partial Restbl FromBinary(byte* src, int src_len);

    [LibraryImport("cs_restbl")]
    private static partial DataMarshal ToBinary(Restbl handle);

    [LibraryImport("cs_restbl")]
    private static partial CrcTable GetCrcTable(Restbl handle);

    [LibraryImport("cs_restbl")]
    private static partial NameTable GetNameTable(Restbl handle);

    [LibraryImport("cs_restbl")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool Free(IntPtr handle);

    public Restbl() : base(true) { }

    public CrcTable CrcTable => GetCrcTable(this);
    public NameTable NameTable => GetNameTable(this);

    public static Restbl FromBinary(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data) {
            return FromBinary(ptr, data.Length);
        }
    }

    public DataMarshal ToBinary()
    {
        return ToBinary(this);
    }

    protected override bool ReleaseHandle()
    {
        return Free(handle);
    }
}
