﻿using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace ModShardPacker;
internal static class Program
{
    static async Task Main(string[] args)
    {
        Option<string> nameOption = new("--name")
        {
            Description = "Name of the output.",
            IsRequired = false
        };
        nameOption.AddAlias("-n");
        nameOption.SetDefaultValue(null);
        
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
        
        Option<bool> logOption = new("--is-verbose")
        {
            Description = "Enable log output into a log file in the cwd.",
            Arity = ArgumentArity.Zero,
        };
        logOption.AddAlias("-v");
        logOption.SetDefaultValue(false);

        RootCommand rootCommand = new("A CLI tool to pack mod source from MSL.")
        {
            nameOption,
            folderOption,
            outputOption,
            dllOption,
            logOption
        };

        rootCommand.SetHandler(MainOperations.MainCommand, nameOption, folderOption, outputOption, dllOption, logOption);

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
