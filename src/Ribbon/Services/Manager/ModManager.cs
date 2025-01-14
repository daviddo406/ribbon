using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using Ribbon.Services.Manager.Writer;

namespace Ribbon.Services.Manager;

public class ModManager : INotifyCollectionChanged
{
    private Dictionary<int, DetailedModFile> _installedMods { get; set; } = new();
    
    public ModWriter.ModWriterOptions ModWriterOptions { get; set; }
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    
    public delegate void CollectionChangedObserver(Dictionary<int, DetailedModFile> sender, NotifyCollectionChangedEventArgs e);
    
    public void Initialize()
    {
        if (File.Exists(ModWriterOptions.OutputFullPath) == false) return;
        var content = File.ReadAllText(ModWriterOptions.OutputFullPath);
        _installedMods = JsonSerializer.Deserialize<Dictionary<int, DetailedModFile>>(content);
    }

    public void Subscribe(CollectionChangedObserver subscriber)
    {
        CollectionChanged += (o, e) => subscriber(_installedMods, e);
    }

    public IEnumerable<DetailedModFile> GetMods()
    {
        return _installedMods.Values;
    }
    
    public void AddMod(DetailedModFile dmf)
    {
        _installedMods[dmf.Id] = dmf;
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, dmf));
        // TODO
        // Would be good to separate this into its own service, i.e. ClientModDownloader
        using (var client = new WebClient())
        {
            foreach (var dependency in dmf.FileDependencies)
            {
                client.DownloadFile(dependency.DownloadUrl, ModWriterOptions.OutputDirectory + dependency.FileName);
            }
            client.DownloadFile(dmf.File.DownloadUrl, ModWriterOptions.OutputDirectory + dmf.File.FileName);
        }
    }
    
    public void RemoveMod(Mod mod)
    {
        _installedMods.Remove(mod.Id);
    }
    
    public void Clear()
    {
        DirectoryInfo di = new DirectoryInfo(ModWriterOptions.OutputDirectory);
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete(); 
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true); 
        }
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

}