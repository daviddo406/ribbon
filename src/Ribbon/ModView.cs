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
        _table = new Table
        {
            ShowRowSeparators = true
        };

        _table.AddColumn("id");
        _table.AddColumn("name");
        _table.AddColumn("desc");
        
        _table.Collapse();
    }

    public Table Register(IEnumerable<IRenderable[]> rows)
    {
        foreach (var row in rows) _table.AddRow(row);

        return _table;
    }

    public Table GetRenderable()
    {
        return _table;
    }
}