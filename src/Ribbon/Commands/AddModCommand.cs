using CurseForge.APIClient.Models.Files;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon.Commands;

public class AddModCommand : Command<AddModCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<ModId>")]
        public string ModId { get; set; }
        
        [CommandOption("-n|--name")]
        public bool UseName { get; set; }
    
    }

    private readonly ModRepository _modRepository;
    
    private readonly ModManager _modManager;
    
    public AddModCommand(ModRepository modRepository, ModManager modManager)
    {
        _modRepository = modRepository;
        _modManager = modManager;
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        Mod? mod = settings.UseName ? _modRepository.GetModByName(settings.ModId) : _modRepository.GetModById(Int32.Parse(settings.ModId));
        if (mod == null || mod.IsAvailable == false)
        {
            AnsiConsole.MarkupLineInterpolated($"No mod found with id {settings.ModId}");
            return 0;
        }
        AnsiConsole.MarkupLineInterpolated($"Found Mod: [bold green]{mod.Name}[/]");

        var res = GetModLoaders(mod);

        bool isConfirmed = AnsiConsole.Prompt(new ConfirmationPrompt("\nAdd mod?"));
        if (isConfirmed)
        {
            AnsiConsole.MarkupInterpolated($"[bold blue]Adding mod: {mod.Id} - {mod.Name}[/]");
            _modManager.AddMod(mod);
        }
        else
        {
            AnsiConsole.MarkupInterpolated($"[bold yellow]No action on mod: {mod.Id} - {mod.Name}[/].");
        }
        
        return 0;
    }

    private List<FileDetail> GetModLoaders(Mod mod)
    {
        List<FileDetail> fileDetails = new();
        
        // mods can have different dependencies based on mod loader
        if (mod.LatestFiles.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No files found. No action taken.[/]");
        }
        
        foreach (File file in mod.LatestFiles)
        {
            FileDetail fileDetail = new();
            foreach (string version in file.GameVersions)
            {
                if (Enum.TryParse(version, true, out ModLoaderType loader))
                {
                    fileDetail.ModLoaderType = loader;
                }
                else
                {
                    fileDetail.GameVersion = version;
                }
            }

            fileDetail.Name = file.DisplayName;
            fileDetail.File = file;
            fileDetail.FileDependencies = file.Dependencies ?? new List<FileDependency>();
            fileDetail.ModDependencies = file.Dependencies?.Count > 0 ? DetermineDependencies(file.Dependencies) : new List<Mod>(); 
            
            fileDetails.Add(fileDetail);
        }
        
        return fileDetails;
    }
    
    private List<Mod> DetermineDependencies(List<FileDependency> dependencies)
    {
        List<Mod> dependencyMods = new();
        foreach (FileDependency dependency in dependencies)
        {
            Mod? mod = _modRepository.GetModById(dependency.ModId);
            if (mod != null) dependencyMods.Add(mod);
        }
        return dependencyMods;
    }
    
}