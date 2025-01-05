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

    public DetailedModFile Process(Mod mod)
    {
        List<File> modFiles = _modRepository.GetModFiles(mod.Id, 0).OrderBy(x => x.DisplayName).ToList();

        File actualModFile = modFiles.Last(); // last is most up-to-date version
        
        var dependencies = GetDependencies(actualModFile);
        
        DetailedModFile dmf = new(mod.Id, mod.Name, actualModFile, _stateProvider.Options.ModLoaderType, _stateProvider.Options.GameVersion, dependencies);
        
        return dmf;
    }

    private List<File> GetDependencies(File file)
    {
        List<File> dependencies = new List<File>();
        foreach (var dependency in file.Dependencies)
        {
            List<File> files = _modRepository.GetModFiles(dependency.ModId, 0);
            dependencies.Add(files.Last());
        }
        
        return dependencies;
    }
    
}