using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Runtime.InteropServices;

namespace CsRestbl;

public partial class CrcTable : SafeHandleZeroOrMinusOneIsInvalid, IEnumerable<KeyValuePair<uint, uint>>, IEnumerable
{
    [LibraryImport("cs_restbl")]
    private static partial int CrcTableCount(CrcTable table);

    [LibraryImport("cs_restbl")]
    private static partial uint CrcTableGet(CrcTable table, uint hash);

    [LibraryImport("cs_restbl")]
    private static partial void CrcTableSet(CrcTable table, uint hash, uint size);

    [LibraryImport("cs_restbl")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool CrcTableContains(CrcTable table, uint hash);

    [LibraryImport("cs_restbl")]
    private static partial void CrcTableRemove(CrcTable table, uint hash);

    [LibraryImport("cs_restbl")]
    private static partial void CrcTableClear(CrcTable table);

    public CrcTable() : base(true) { }

    public int Count => CrcTableCount(this);

    public uint this[uint hash] {
        get => CrcTableGet(this, hash);
        set => CrcTableSet(this, hash, value);
    }

    public bool Contains(uint hash) => CrcTableContains(this, hash);
    public void Remove(uint hash) => CrcTableRemove(this, hash);
    public void Clear() => CrcTableClear(this);

    public IEnumerator GetEnumerator()
        => new Enumerable(this);

    IEnumerator<KeyValuePair<uint, uint>> IEnumerable<KeyValuePair<uint, uint>>.GetEnumerator()
        => new Enumerable(this);

    [LibraryImport("cs_restbl")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool CrcTableAdvance(CrcTable table, IntPtr iterator, out IntPtr next);

    [LibraryImport("cs_restbl")]
    private static partial void CrcTableCurrent(IntPtr iterator, out uint hash, out uint size);

    private struct Enumerable : IEnumerator<KeyValuePair<uint, uint>>, IEnumerator
    {
        private readonly CrcTable _table;
        private IntPtr _iterator = IntPtr.Zero;

        public Enumerable(CrcTable table)
        {
            _table = table;
        }

        object IEnumerator.Current => Current;
        public KeyValuePair<uint, uint> Current {
            get {
                CrcTableCurrent(_iterator, out uint hash, out uint size);
                return new(hash, size);
            }
        }

        public bool MoveNext() => CrcTableAdvance(_table, _iterator, out _iterator);

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
