using Ribbon.Models;
using Ribbon.Services.Adapter;
using Ribbon.Services.Manager;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ribbon.Commands.Mods;

public class AddModCommand : Command<AddModCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<ModId>")]
        public required string ModId { get; set; }
        
        [CommandOption("-n|--name")]
        public bool UseName { get; set; }
    
    }
    
    private readonly ModAdapter _modAdapter;
    
    private readonly ModManager _modManager;
    
    public AddModCommand(ModAdapter modAdapter, ModManager modManager)
    {
        _modAdapter = modAdapter;
        _modManager = modManager;
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        DetailedModFile? dmf = _modAdapter.Process(settings.ModId, settings.UseName);
        if (dmf == null)
        {
            AnsiConsole.MarkupLineInterpolated($"No mod found with id {settings.ModId}");
            return 0;
        }
        
        StageView(dmf);
        PromptConfirmation(dmf);
        
        return 0;
    }

    private void StageView(DetailedModFile dmf)
    {
        Rule rule = new Rule($"Found Mod: [bold green]{Markup.Escape(dmf.Name)}[/] -- {Markup.Escape(dmf.File.DisplayName)}");
        rule.Justification = Justify.Left;
        AnsiConsole.Write(rule);

        if (dmf.FileDependencies.Count > 0)
        {
            Rows rows = new Rows(dmf.FileDependencies.Select(x => new Text(x.DisplayName)));
            Panel panel = new Panel(rows);
            panel.Header = new PanelHeader("[blue]Dependencies[/]");
            AnsiConsole.Write(panel);
        }
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