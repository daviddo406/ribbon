using Ribbon.Services.Manager;
using Ribbon.Services.Manager.Writer;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ribbon.Commands.Mods;

public class ClearModsCommand : Command<ClearModsCommand.Settings>
{
    public class Settings : CommandSettings
    {
    }
    
    private readonly ModManager _modManager;
    
    public ClearModsCommand(ModManager modManager)
    {
        _modManager = modManager;
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        _modManager.Clear();
        AnsiConsole.MarkupLineInterpolated($"[blue b]All mods removed[/]");
        return 0;
    }
}