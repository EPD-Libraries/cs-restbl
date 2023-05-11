using Native.IO;
using Native.IO.Services;

namespace CsRestbl;

public class RestblLibrary : NativeLibrary<RestblLibrary>, INativeLibrary
{
    protected override string Name { get; } = "cs_restbl";
}
