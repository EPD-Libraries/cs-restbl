using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace CsRestbl;

public unsafe partial class NameTable : SafeHandleZeroOrMinusOneIsInvalid, IEnumerable<KeyValuePair<string, uint>>, IEnumerable
{
    [LibraryImport("cs_restbl")]
    private static partial int NameTableCount(NameTable table);

    [LibraryImport("cs_restbl", StringMarshalling = StringMarshalling.Utf8)]
    private static partial uint NameTableGet(NameTable table, string name);

    [LibraryImport("cs_restbl", StringMarshalling = StringMarshalling.Utf8)]
    private static partial void NameTableSet(NameTable table, string name, uint size);

    [LibraryImport("cs_restbl", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool NameTableContains(NameTable table, string name);

    [LibraryImport("cs_restbl", StringMarshalling = StringMarshalling.Utf8)]
    private static partial void NameTableRemove(NameTable table, string name);

    [LibraryImport("cs_restbl")]
    private static partial void NameTableClear(NameTable table);

    public NameTable() : base(true) { }

    public int Count => NameTableCount(this);

    public uint this[string name] {
        get => NameTableGet(this, name);
        set => NameTableSet(this, name, value);
    }

    public bool Contains(string name) => NameTableContains(this, name);
    public void Remove(string name) => NameTableRemove(this, name);
    public void Clear() => NameTableClear(this);

    public IEnumerator<KeyValuePair<string, uint>> GetEnumerator()
        => new Enumerable(this);

    IEnumerator IEnumerable.GetEnumerator()
        => new Enumerable(this);

    [LibraryImport("cs_restbl")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool NameTableAdvance(NameTable table, IntPtr iterator, out IntPtr next);

    [LibraryImport("cs_restbl")]
    private unsafe static partial void NameTableCurrent(IntPtr iterator, out byte* name_ptr, out uint size);

    private struct Enumerable : IEnumerator<KeyValuePair<string, uint>>, IEnumerator
    {
        private readonly NameTable _table;
        private IntPtr _iterator = IntPtr.Zero;

        public Enumerable(NameTable table)
        {
            _table = table;
        }

        object IEnumerator.Current => Current;
        public KeyValuePair<string, uint> Current {
            get {
                NameTableCurrent(_iterator, out byte* namePtr, out uint size);
                return new(Utf8StringMarshaller.ConvertToManaged(namePtr)!, size);
            }
        }

        public bool MoveNext() => NameTableAdvance(_table, _iterator, out _iterator);

        public void Reset()
        {
            _iterator = IntPtr.Zero;
        }

        public void Dispose() { }
    }

    protected override bool ReleaseHandle()
    {
        // This object will be freed
        // by the owning Restbl handle
        return true;
    }
}
