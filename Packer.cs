using System.Diagnostics;
using Serilog;
using ModShardPackerReference;

namespace ModShardPacker;
internal static class MainOperations
{
    public static async Task MainCommand(string? name, string packingFolder, string? outputFolder, string? dllFolder, bool isVerbose)
    {
        dllFolder ??= Environment.CurrentDirectory;
        outputFolder ??= packingFolder;

        if (isVerbose)
        {
            LoggerConfiguration logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(string.Format("logs/log_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmm")));

            Log.Logger = logger.CreateLogger();
        }   
        

        Console.WriteLine($"Packing {packingFolder} in {outputFolder} using dll from {dllFolder}");
        
        if (Pack(name, packingFolder, outputFolder, dllFolder))
        {
            Console.WriteLine($"Successfully packed {packingFolder}");
        }
        else
        {
            Console.WriteLine($"Failed packing {packingFolder}");
        }
        
        await Log.CloseAndFlushAsync();
    }
    public static bool Pack(string? namePacked, string folderToPack, string outputfolder, string dllfolder)
    {
        // work around to find the FileVersion of ModShardLauncher.dll for single file publishing
        // see: https://github.com/dotnet/runtime/issues/13051
        string mslVersion;
        try
        {
            mslVersion = "v" + FileVersionInfo.GetVersionInfo(typeof(ModShardLauncher.Mods.Mod).Assembly.Location).FileVersion;
        }
        catch(FileNotFoundException ex)
        {
            Log.Error(ex, "Cannot find the dll of ModShardLauncher");
            return false;
        }

        bool resultPacking = false;

        try
        {
            resultPacking = FilePacker.Pack(
                namePacked, 
                folderToPack, 
                outputfolder, 
                dllfolder, 
                mslVersion, 
                new Type[2] {typeof(ModShardLauncher.Mods.Mod), typeof(UndertaleModLib.Models.UndertaleCode)}
            );
        }
        catch(Exception ex)
        {
            if (ex is ArgumentNullException || ex is ArgumentException || ex is IOException || ex is DirectoryNotFoundException)
            {
                Log.Error(ex.ToString());
            }
            else
            {
                Log.Error(ex, "Unexpected error");
            }
            Console.WriteLine(ex.Message);
            Log.Error(ex.ToString());
        }

        return resultPacking;
    }
}

