using CsRestbl;
using Native.IO.Services;

NativeLibraryManager.RegisterAssembly(typeof(Program).Assembly, out bool isCommonLoaded)
    .Register(RestblLibrary.Shared, out bool isRestblLoaded);

Console.WriteLine($"Common Loaded: {isCommonLoaded}");
Console.WriteLine($"Restbl Loaded: {isRestblLoaded}");

byte[] data = File.ReadAllBytes(@"D:\Bin\RSTB\totk\ResourceSizeTable.Product.100.rsizetable");
using Restbl restbl = Restbl.FromBinary(data);

using FileStream fs = File.Create(@"D:\Bin\RSTB\totk\COUT_ResourceSizeTable.Product.100.rsizetable");
fs.Write(restbl.ToBinary());