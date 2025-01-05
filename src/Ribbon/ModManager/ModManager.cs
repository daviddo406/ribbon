using System.Net;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon;

public class ModManager
{
    public Dictionary<int, DetailedModFile> InstalledMods { get; protected set; } = new ();
    
    public void AddMod(DetailedModFile dmf)
    {
        InstalledMods[dmf.Id] = dmf;
        
        using (var client = new WebClient())
        {
            foreach (var dependency in dmf.FileDependencies)
            {
                client.DownloadFile(dependency.DownloadUrl, "./mods/" + dependency.FileName);
            }
            client.DownloadFile(dmf.File.DownloadUrl, "./mods/" + dmf.File.FileName);
        }
    }
    
    public void RemoveMod(Mod mod)
    {
        InstalledMods.Remove(mod.Id);
    }

    public void Clear()
    {
        InstalledMods.Clear();
    }

}