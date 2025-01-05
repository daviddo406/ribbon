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
    
    private readonly ModAdapter _modAdapter;
    
    private readonly ModManager _modManager;
    
    public AddModCommand(ModRepository modRepository, ModAdapter modAdapter, ModManager modManager)
    {
        _modRepository = modRepository;
        _modAdapter = modAdapter;
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

        DetailedModFile dmf = _modAdapter.Process(mod);
        StageAdd(dmf);

        PromptConfirmation(dmf);
        
        return 0;
    }

    private void StageAdd(DetailedModFile dmf)
    {
        Rule rule = new Rule($"[bold green]{dmf.Name}[/] -- {dmf.File.DisplayName}");
        rule.Justification = Justify.Left;
        Rows rows = new Rows(dmf.FileDependencies.Select(x => new Text(x.DisplayName)));
        Panel panel = new Panel(rows);
        panel.Header = new PanelHeader("[blue]Dependencies[/]");
        
        AnsiConsole.Write(rule);
        AnsiConsole.Write(panel);
    }
    
    private bool PromptConfirmation(DetailedModFile dmf)
    {
        bool isConfirmed = AnsiConsole.Prompt(new ConfirmationPrompt("\nAdd mod?"));
        if (isConfirmed)
        {
            _modManager.AddMod(dmf);
            AnsiConsole.MarkupInterpolated($"[bold blue]Added mod: {dmf.Id} - {dmf.Name}[/]");
        }
        else
        {
            AnsiConsole.MarkupInterpolated($"[bold yellow]No action on mod: {dmf.Id} - {dmf.Name}[/].");
        }
        return isConfirmed;
    }
    
}