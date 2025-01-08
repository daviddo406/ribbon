using System.Collections.Specialized;
using System.Text.Json;
using Ribbon.Models;

namespace Ribbon;

public class ModWriter
{
    public void Write(Dictionary<int, DetailedModFile>? data, NotifyCollectionChangedEventArgs e)
    {
        var content = JsonSerializer.Serialize(data);
        File.WriteAllText(Path.Combine("ribbon-saved-mods.json"), content);
    }
}