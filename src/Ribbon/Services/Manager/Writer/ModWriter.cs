using System.Collections.Specialized;
using System.Text.Json;
using Ribbon.Models;

namespace Ribbon.Services.Manager.Writer;

public class ModWriter(ModWriter.ModWriterOptions options)
{
    public class ModWriterOptions
    {
        public string OutputDirectory;
    }

    private readonly ModWriterOptions _options = options;
    
    public void Write(Dictionary<int, DetailedModFile>? data, NotifyCollectionChangedEventArgs e)
    {
        var content = JsonSerializer.Serialize(data);
        File.WriteAllText(Path.Combine(_options.OutputDirectory), content);
    }
}