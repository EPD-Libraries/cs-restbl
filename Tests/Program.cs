using CsRestbl;
using Native.IO.Services;

NativeLibraryManager.RegisterAssembly(typeof(Program).Assembly, out bool isCommonLoaded)
    .Register(RestblLibrary.Shared, out bool isRestblLoaded);

Console.WriteLine($"Common Loaded: {isCommonLoaded}");
Console.WriteLine($"Restbl Loaded: {isRestblLoaded}");