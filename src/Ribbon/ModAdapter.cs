using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon;

public class ModAdapter
{

    private readonly ModRepository _modRepository;
    
    public ModAdapter(ModRepository modRepository)
    {
        _modRepository = modRepository;
    }

    public void Initialize()
    {
        
    }

    public DetailedModFile Process(Mod mod)
    {
        List<File> modFiles = _modRepository.GetModFiles(mod.Id, 0).OrderBy(x => x.DisplayName).ToList();
        
        return new DetailedModFile();
    }
    
    // private List<DetailedModFile> ProcessMod(Mod mod)
    // {
    //     List<DetailedModFile> fileDetails = new();
    //     
    //     // mods can have different dependencies based on mod loader
    //     if (mod.LatestFiles.Count == 0)
    //     {
    //         AnsiConsole.MarkupLine("[red]No files found. No action taken.[/]");
    //     }
    //     
    //     foreach (File file in mod.LatestFiles)
    //     {
    //         DetailedModFile detailedModFile = new();
    //         foreach (string version in file.GameVersions)
    //         {
    //             if (Enum.TryParse(version, true, out ModLoaderType loader))
    //             {
    //                 detailedModFile.ModLoaderType = loader;
    //             }
    //             else
    //             {
    //                 detailedModFile.GameVersion = version;
    //             }
    //         }
    //         
    //         detailedModFile.Name = file.DisplayName;
    //         detailedModFile.File = file;
    //         detailedModFile.FileDependencies = file.Dependencies ?? new List<FileDependency>();
    //         detailedModFile.ModDependencies = file.Dependencies?.Count > 0 ? DetermineDependencies(file.Dependencies) : new List<Mod>(); 
    //         
    //         fileDetails.Add(detailedModFile);
    //     }
    //     
    //     return fileDetails;
    // }
}