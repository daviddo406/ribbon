using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;

namespace Ribbon;

public class ModManager : INotifyCollectionChanged
{
    private Dictionary<int, DetailedModFile> _installedMods { get; set; } = new();
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    
    private readonly ModWriter _modWriter;

    public ModManager(ModWriter modWriter)
    {
        Initialize();
        _modWriter = modWriter;

        CollectionChanged += (o, e) => _modWriter.Write(_installedMods, e);
    }

    private void Initialize()
    {
        var content = File.ReadAllText("ribbon-saved-mods.json");
        _installedMods = JsonSerializer.Deserialize<Dictionary<int, DetailedModFile>>(content);
    }

    public IEnumerable<DetailedModFile> GetMods()
    {
        return _installedMods.Values;
    }
    
    public void AddMod(DetailedModFile dmf)
    {
        _installedMods[dmf.Id] = dmf;
        CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, dmf));
        // TODO
        // Would be good to separate this into its own service, i.e. ClientModDownloader
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
        _installedMods.Remove(mod.Id);
    }

    public void Clear()
    {
        _installedMods.Clear();
        System.IO.DirectoryInfo di = new DirectoryInfo("./mods");
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete(); 
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true); 
        }
        CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

}