using System.IO.Compression;
using Ribbon.Services.State;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ribbon.Commands.Mods;

public class ExportModsCommand : Command<ExportModsCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<path>")]
        public required string OutputPath { get; set; }
        
        [CommandOption("-z|--zip")]
        public bool AsZip { get; set; }
    }

    private readonly StateProvider _stateProvider;
    
    public ExportModsCommand(StateProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        string finalOutput = settings.OutputPath;
        Directory.CreateDirectory(Path.GetDirectoryName(settings.OutputPath) ?? string.Empty);
        if (settings.AsZip)
        {
            ZipFile.CreateFromDirectory(_stateProvider.Options.ModWriterOptions.OutputDirectory,
                settings.OutputPath + ".zip");
            finalOutput = settings.OutputPath + ".zip";
        }
        else
        {
            Directory.CreateDirectory(settings.OutputPath);

            // Get the files in the source directory and copy to the destination directory
            foreach (string file in Directory.GetFiles(_stateProvider.Options.ModWriterOptions.OutputDirectory))
            {
                string filename = Path.GetFileName(file);
                string targetFilePath = Path.Combine(settings.OutputPath, filename);
                File.Copy(file, targetFilePath, true);
            }
        }
        
        AnsiConsole.MarkupLineInterpolated($"[green]Success![/] Saved to {finalOutput}");
        return 0;
    }
}