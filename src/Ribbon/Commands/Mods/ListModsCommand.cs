using Ribbon.State;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ribbon.Commands;

public class ListModsCommand : Command<ListModsCommand.Settings>
{
    public class Settings : CommandSettings
    {
    }

    private ModManager _modManager;
    
    private StateProvider _stateProvider;
    
    public ListModsCommand(ModManager modManager, StateProvider stateProvider)
    {
        _modManager = modManager;
        _stateProvider = stateProvider;
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        var mods = _modManager.GetMods();
        var table = ListModsView.ListModsAsTable(_stateProvider, mods);
        
        AnsiConsole.Write(table);
        return 0;
    }
}