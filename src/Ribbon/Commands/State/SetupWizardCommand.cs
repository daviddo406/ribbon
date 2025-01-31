using CurseForge.APIClient.Models.Mods;
using Ribbon.Services.State;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ribbon.Commands.State;

public class SetupWizardCommand : Command<SetupWizardCommand.SetupWizardCommandSettings>
{
    public class SetupWizardCommandSettings : CommandSettings
    {
        [CommandOption("-f|--full")]
        public bool IsFullWizard { get; set; }
    }

    private readonly StateProvider _stateProvider;
    
    public SetupWizardCommand(StateProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }
    
    public override int Execute(CommandContext context, SetupWizardCommandSettings settings)
    {
        PromptGameVersion();
        PromptModLoaderType();
        if (settings.IsFullWizard || _stateProvider.Options.isFirstTimeSetup)
        {
            _stateProvider.Options.isFirstTimeSetup = false;
            PromptModWriterOutput();
        }
        
        AnsiConsole.Markup("[blue]Setup Wizard Done! Thanks for using this software![/]");
        return 0;
    }

    private void PromptGameVersion()
    {
        var gameVersion = AnsiConsole.Prompt(new TextPrompt<string>("[yellow]Enter Game Version:[/]"));
        _stateProvider.Options.SetGameVersion(gameVersion);
    }

    private void PromptModLoaderType()
    {
        var modLoaderType = AnsiConsole
            .Prompt(
                new TextPrompt<ModLoaderType>("[yellow]Choose Mod Loader Type:[/]")
                    .AddChoice(ModLoaderType.Forge)
                    .AddChoice(ModLoaderType.Cauldron)
                    .AddChoice(ModLoaderType.LiteLoader)
                    .AddChoice(ModLoaderType.Fabric)
                    .AddChoice(ModLoaderType.Quilt)
                    .AddChoice(ModLoaderType.NeoForge)
                );
        _stateProvider.Options.SetModLoaderType(modLoaderType);
    }

    private void PromptModWriterOutput()
    {
        var output = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Enter Path to Output Mods:[/]")
                .Validate(x => Path.Exists(x))
            );
        _stateProvider.Options.ModWriterOptions.OutputDirectory = output;
    }
    
}