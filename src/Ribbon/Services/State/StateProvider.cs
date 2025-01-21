using System.ComponentModel;
using System.Text.Json;
using Ribbon.Services.Manager.Writer;

namespace Ribbon.Services.State;

public class StateProvider
{
    
    public readonly StateOptions Options = GetOptions();
    
    private const string _stateFileName = "~/.config/ribbon-options.json";

    public StateProvider()
    {
        Options.PropertyChanged += SaveOptions;
        Options.ModWriterOptions.PropertyChanged += SaveOptions;
    }
    
    private static StateOptions GetOptions()
    {
        if (File.Exists(_stateFileName) == false)
        {
            return new StateOptions();
        }
        
        string content = File.ReadAllText(_stateFileName);
        if (string.IsNullOrEmpty(content))
        {
            return new StateOptions();
        }
        
        return JsonSerializer.Deserialize<StateOptions>(content)!;
    }
    
    public void SaveOptions(object? sender, PropertyChangedEventArgs e)
    {
        var content = JsonSerializer.Serialize(Options);
        File.WriteAllText(_stateFileName, content);
    }
    
}