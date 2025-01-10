using System.Text.Json;

namespace Ribbon.Services.State;

public class StateProvider
{
    public StateOptions Options;

    public StateProvider()
    {
        GetOptions();
        CreateDirectories();
    }
    
    private void GetOptions()
    {
        if (File.Exists("ribbon.json") == false)
        {
            File.CreateText("ribbon.json");
            Options = new StateOptions();
            return;
        }
        
        string content = File.ReadAllText("ribbon.json");
        if (string.IsNullOrEmpty(content))
        {
            Options = new StateOptions();
            return;                        
        }
        
        Options = JsonSerializer.Deserialize<StateOptions>(content);;
    }

    private void CreateDirectories()
    {
        Directory.CreateDirectory("./mods");
    }
    
    public void SaveOptions()
    {
        var content = JsonSerializer.Serialize(Options);
        File.WriteAllText("ribbon.json", content);
    }
    
}