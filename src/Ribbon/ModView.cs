using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Mods;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Ribbon;

public class ModView
{
    private Table _table;
    
    public ModView()
    {
        _table = new Table();
        _table.Border(TableBorder.Rounded);
        _table.ShowRowSeparators = true;

        _table.AddColumn("id");
        _table.AddColumn("name");
        _table.AddColumn("desc");
    }

    public async Task Show(ModRepository repository)
    {
        //int page = 0;
        //while (true)
        //{
        //    var mods = await repository.SearchMods(page);
            
        //    List<int> ids = [];
        //    List<string> names = [];
        //    List<string> summaries = [];
        //    foreach (Mod mod in mods.Data)
        //    {
        //        ids.Add(mod.Id);
        //        names.Add(mod.Name);
        //        summaries.Add(mod.Summary);
        //    }

        //    for (int i = 0; i < ids.Count; i++)
        //    {
        //        Text id = new Text(ids[i].ToString(), new Style(Color.Teal));
        //        Text name = new Text(names[i], new Style(Color.Yellow));
        //        Text summary = new Text(summaries[i]);
        //        IRenderable[] row =
        //        [
        //            id, name, summary
        //        ];
        //        _table.AddRow(row);
        //    }

        //    AnsiConsole.Write(_table);
        //    if (NavigationPrompt()) page += 1;
        //    else
        //    {
        //        if (page > 0) page -= 1;
        //    };
        //}
    }

    private bool NavigationPrompt()
    {
        var nav = AnsiConsole.Prompt(
            new TextPrompt<bool>("Next page?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "n" : "p"));
        return nav;
    }
}