using Spectre.Console.Cli;

namespace Ribbon.Commands.Mods;

public class ExportModsCommand : Command<ExportModsCommand.Settings>
{
    public class Settings : CommandSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}