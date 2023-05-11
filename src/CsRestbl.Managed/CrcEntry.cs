using CommunityToolkit.Mvvm.ComponentModel;

namespace CsRestbl.Managed;

public partial class CrcEntry : ObservableObject
{
    [ObservableProperty]
    private uint _hash;

    [ObservableProperty]
    private uint _size;

    public CrcEntry(uint hash, uint size)
    {
        _hash = hash;
        _size = size;
    }
}
