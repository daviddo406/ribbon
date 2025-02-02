using Ribbon.Models;
using Ribbon.Services.State;
using Spectre.Console;

namespace Ribbon.Views;

public static class ListModsView
{
    public static Table ListModsAsTable(StateProvider stateProvider, IEnumerable<DetailedModFile> mods)
    {
        var table = new Table();
        
        table.Title = new TableTitle($"[bold yellow]Current Mods ({stateProvider.Options.ModLoaderType}:{stateProvider.Options.GameVersion})[/]");
        
        table.ShowRowSeparators = true;
        table.Border(TableBorder.Rounded);
        
        table.AddColumn("Name");
        table.AddColumn("Dependencies");
        table.AddColumn("Release Date");

        foreach (var mod in mods)
        {
            table.AddRow(mod.Name, mod.FileDependencies.Count.ToString(), mod.File.FileDate.ToString("dd/MM/yyyy"));
        }


        Console.WriteLine();
        

        return table;
    }
    
}