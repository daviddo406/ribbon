using System.Text;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Services.Manager.Writer;

namespace Ribbon.Services.State;

public class StateOptions
{
    public ModLoaderType ModLoaderType { get; set; } = ModLoaderType.Forge;
    
    public string GameVersion { get; set; }

    public ModWriter.ModWriterOptions ModWriterOptions { get; set; } = new();
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"SETTINGS:");
        sb.AppendLine($"Game version: {GameVersion}");
        sb.AppendLine($"Mod loader type: {ModLoaderType}");
        return sb.ToString();
    }
}