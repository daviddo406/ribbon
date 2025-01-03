using System.Text.Json;

namespace Ribbon.State;

public class StateProvider
{
    public StateOptions Options;

    public StateProvider()
    {
        GetOptions();
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
        JsonSerializer.Deserialize<StateOptions>(content);
        Options =  new StateOptions();
    }

    public void SaveOptions()
    {
        var content = JsonSerializer.Serialize(Options);
        File.WriteAllText("ribbon.json", content);
    }
    
}