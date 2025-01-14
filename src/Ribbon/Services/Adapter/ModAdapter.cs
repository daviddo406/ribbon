using CurseForge.APIClient.Models.Files;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using Ribbon.Repositories;
using Ribbon.Services.State;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon.Services.Adapter;

public class ModAdapter
{

    private readonly ModRepository _modRepository;
    
    private readonly StateProvider _stateProvider;
    
    public ModAdapter(ModRepository modRepository, StateProvider stateProvider)
    {
        _modRepository = modRepository;
        _stateProvider = stateProvider;
    }
    
    /// <summary>
    /// Accepts a modId and creates a <c>DetailedModFile</c> from the original mod.
    /// Used to determine the latest file for a mod and its dependencies.
    /// <param name="modId">The name of a mod.</param>
    /// </summary>
    public DetailedModFile? Process(string modId, bool isName = false)
    {
        Mod? mod = isName ? _modRepository.GetModByName(modId) : _modRepository.GetModById(Int32.Parse(modId));
        
        if (mod == null || mod.IsAvailable == false) return null;
        
        List<File> modFiles = _modRepository.GetModFiles(mod.Id, 0).OrderByDescending(x => x.FileDate).ToList();
        if (modFiles.Count == 0) return null;
        
        File actualModFile = modFiles.First();
        
        var dependencies = GetDependencies(actualModFile);
        
        DetailedModFile dmf = new(mod.Id, mod.Name, actualModFile, _stateProvider.Options.ModLoaderType, _stateProvider.Options.GameVersion, dependencies);
        
        return dmf;
    }

    private List<File> GetDependencies(File file)
    {
        // TODO
        // make this function recursively search dependencies, not just one level down
        List<File> dependencies = new List<File>();
        foreach (var dependency in file.Dependencies.Where(x => x.RelationType == FileRelationType.RequiredDependency))
        {
            List<File> files = _modRepository.GetModFiles(dependency.ModId, 0).OrderByDescending(x => x.FileDate).ToList();
            if(files.Count != 0) dependencies.Add(files.First());
        }
        
        return dependencies;
    }
    
}