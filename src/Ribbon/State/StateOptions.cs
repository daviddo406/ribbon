using System.Text;
using CurseForge.APIClient.Models.Mods;

namespace Ribbon.State;

public class StateOptions
{
    public ModLoaderType ModLoaderType { get; set; } = ModLoaderType.Forge;
    
    public string GameVersion { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"SETTINGS:");
        sb.AppendLine($"Game version: {GameVersion}");
        sb.AppendLine($"Mod loader type: {ModLoaderType}");
        return sb.ToString();
    }
}