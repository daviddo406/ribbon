using System.Collections.Specialized;
using System.Net;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;

namespace Ribbon;

public class ModManager : INotifyCollectionChanged
{
    public Dictionary<int, DetailedModFile> InstalledMods { get; protected set; } = new ();
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    
    private readonly ModWriter _modWriter;

    public ModManager(ModWriter modWriter)
    {
        _modWriter = modWriter;

        CollectionChanged += (o, e) => _modWriter.Write(InstalledMods, e);
    }
    
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