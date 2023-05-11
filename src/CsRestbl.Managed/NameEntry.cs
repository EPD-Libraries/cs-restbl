using CommunityToolkit.Mvvm.ComponentModel;

namespace CsRestbl.Managed;

public partial class NameEntry : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private uint _size;

    public NameEntry(string name, uint size)
    {
        _name = name;
        _size = size;
    }
}
