using CurseForge.APIClient.Models.Files;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using Ribbon.Repositories;
using Ribbon.Services.State;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon.Services.Adapter;

public class ModAdapter
{
    public ModRepository ModRepository;
    
    public StateProvider StateProvider;
    
    /// <summary>
    /// Accepts a modId and creates a <c>DetailedModFile</c> from the original mod.
    /// Used to determine the latest file for a mod and its dependencies.
    /// <param name="modId">The name of a mod.</param>
    /// </summary>
    public DetailedModFile? Process(string modId, bool isName = false)
    {
        Mod? mod = isName ? ModRepository.GetModByName(modId) : ModRepository.GetModById(Int32.Parse(modId));
        
        if (mod == null || mod.IsAvailable == false) return null;
        
        List<File> modFiles = ModRepository.GetModFiles(mod.Id, 0).OrderByDescending(x => x.FileDate).ToList();
        if (modFiles.Count == 0) return null;
        
        File actualModFile = modFiles.First();
        
        var dependencies = GetDependencies(actualModFile);
        
        DetailedModFile dmf = new(mod.Id, mod.Name, actualModFile, StateProvider.Options.ModLoaderType, StateProvider.Options.GameVersion, dependencies.Values.ToList());
        
        return dmf;
    }

    /// <summary>
    /// Recursively get file dependencies.
    /// </summary>
    /// <param name="file"></param>
    /// <returns>Flattened list of <c>File</c>s, representing all dependencies.</returns>
    private Dictionary<int, File> GetDependencies(File file)
    {
        Dictionary<int, File> dependencies = new();
        foreach (var dependency in file.Dependencies.Where(x => x.RelationType == FileRelationType.RequiredDependency))
        {
            List<File> files = ModRepository.GetModFiles(dependency.ModId, 0).OrderByDescending(x => x.FileDate).ToList();
            if (files.Count == 0) continue;
            
            var f = files.First();
            dependencies.TryAdd(f.ModId, f);
            dependencies = dependencies.Concat(GetDependencies(f).Where(x => !dependencies.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
        }
        
        return dependencies;
    }
}