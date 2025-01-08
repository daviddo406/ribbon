using Spectre.Console.Cli;

namespace Ribbon.Commands;

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
        return 0;
    }
}