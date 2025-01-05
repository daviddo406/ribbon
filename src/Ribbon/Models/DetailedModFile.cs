using System.Diagnostics;
using CurseForge.APIClient.Models.Files;
using CurseForge.APIClient.Models.Mods;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon.Models;

public class DetailedModFile(
    int id,
    string name,
    File file,
    ModLoaderType modLoaderType,
    string gameVersion,
    List<File> fileDependencies)
{
    public int Id { get; set; } = id;
    
    public string Name { get; set; } = name;

    public File File { get; set; } = file;

    public ModLoaderType ModLoaderType { get; set; } = modLoaderType;

    public string GameVersion { get; set; } = gameVersion;

    public List<File> FileDependencies { get; set; } = fileDependencies;
}