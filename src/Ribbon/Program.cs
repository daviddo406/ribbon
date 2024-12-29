using CurseForge.APIClient.Models.Mods;
using Ribbon;
using Spectre.Console;
using System.Numerics;


var repo = new ModRepository("$2a$10$96h3LX0zd4NF9fTroK0Du.06R.mear0mOhpN.ax9B.8DUH.JM6A4K");

var mods = await repo.SearchMods();

var table = new Table();
table.Border(TableBorder.Rounded);

table.AddColumn("id");
table.AddColumn("name");
table.AddColumn("desc");

List<int> ids = [];
List<string> names = [];
List<string> summaries = [];
foreach (Mod mod in mods.Data)
{
    ids.Add(mod.Id);
    names.Add(mod.Name);
    summaries.Add(mod.Summary);
}

for (int i = 0; i < ids.Count; i++)
{
    Text id = new Text(ids[i].ToString(), new Style(Color.Teal, Color.Black));
    Text name = new Text(names[i], new Style(Color.Yellow, Color.Black));
    Text summary = new Text(summaries[i]);
    Text[] row =
    [
        id, name, summary
    ];
    table.AddRow(row);
}

AnsiConsole.Write(table);