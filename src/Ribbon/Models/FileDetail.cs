using CurseForge.APIClient.Models.Files;
using CurseForge.APIClient.Models.Mods;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon.Models;

public class FileDetail
{
    public string Name { get; set; }
    
    public File File { get; set; }
    
    public ModLoaderType ModLoaderType { get; set; }
    
    public string GameVersion { get; set; }
    
    public List<FileDependency> FileDependencies { get; set; }
    
    public List<Mod> ModDependencies { get; set; }
    
}