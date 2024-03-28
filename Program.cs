using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace ModShardPacker;
internal static class Program
{
    static async Task Main(string[] args)
    {
        Option<string> folderOption = new("--folder")
        {
            Description = "Folder of the mod code source.",
            IsRequired = true
        };
        folderOption.AddAlias("-f");
        
        Option<string?> outputOption = new("--output")
        {
            Description = "Output folder."
        };
        outputOption.AddAlias("-o");
        outputOption.SetDefaultValue(null);

        Option<string?> dllOption = new("--dll")
        {
            Description = "Folder where all dlls needed to compile are located.",
        };
        dllOption.AddAlias("-d");
        dllOption.SetDefaultValue(null);

        RootCommand rootCommand = new("A CLI tool to pack mod code source from MSL.")
        {
            folderOption,
            outputOption,
            dllOption
        };

        rootCommand.SetHandler(MainOperations.MainCommand, folderOption, outputOption, dllOption);

        CommandLineBuilder commandLineBuilder = new(rootCommand);

        commandLineBuilder.AddMiddleware(async (context, next) =>
        {
            await next(context);
        });

        commandLineBuilder.UseDefaults();
        Parser parser = commandLineBuilder.Build();

        await parser.InvokeAsync(args);
    }
}
