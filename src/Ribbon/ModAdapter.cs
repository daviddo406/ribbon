using System.Net;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using Ribbon.State;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon;

public class ModAdapter
{

    private readonly ModRepository _modRepository;
    
    private readonly StateProvider _stateProvider;
    
    public ModAdapter(ModRepository modRepository, StateProvider stateProvider)
    {
        _modRepository = modRepository;
        _stateProvider = stateProvider;
    }

    public void Initialize()
    {
    }
    
    /// <summary>
    /// Accepts a modId and creates a <c>DetailedModFile</c> from the original mod.
    /// Used to determine the latest file for a mod and its dependencies.
    /// <param name="modId">The name of a mod.</param>
    /// </summary>
    public DetailedModFile? Process(string modId)
    {
        Mod? mod = _modRepository.GetModByName(modId);
        if (mod == null || mod.IsAvailable == false) return null;
        
        List<File> modFiles = _modRepository.GetModFiles(mod.Id, 0).OrderByDescending(x => x.FileDate).ToList();

        File actualModFile = modFiles.First();
        
        var dependencies = GetDependencies(actualModFile);
        
        DetailedModFile dmf = new(mod.Id, mod.Name, actualModFile, _stateProvider.Options.ModLoaderType, _stateProvider.Options.GameVersion, dependencies);
        
        return dmf;
    }

    private List<File> GetDependencies(File file)
    {
        List<File> dependencies = new List<File>();
        foreach (var dependency in file.Dependencies)
        {
            List<File> files = _modRepository.GetModFiles(dependency.ModId, 0).OrderByDescending(x => x.FileDate).ToList();
            dependencies.Add(files.First());
        }
        
        return dependencies;
    }
    
}