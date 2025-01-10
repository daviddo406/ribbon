using CurseForge.APIClient.Models.Mods;
using Ribbon.Services.State;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ribbon.Commands.State;

public class ConfigureSettings : CommandSettings
{
}

public class ConfigureGameVersionSettings : ConfigureSettings
{
    [CommandArgument(0, "<version>")]
    public required string GameVersion { get; set; }
}

public class ConfigureModLoaderSettings : ConfigureSettings
{
    [CommandArgument(0, "<modloader>")]
    public required ModLoaderType ModLoaderType { get; set; }
}

public class ConfigureModWriterSettings : ConfigureSettings
{
    [CommandArgument(0, "<path>")]
    public required string OutputDirectory { get; set; }
}

public class ConfigureGameVersionCommand(StateProvider stateProvider) : Command<ConfigureGameVersionSettings>
{
    public override int Execute(CommandContext context, ConfigureGameVersionSettings settings)
    {
        stateProvider.Options.GameVersion = settings.GameVersion;
        stateProvider.SaveOptions();
        return 0;
    }
}

public class ConfigureModLoaderCommand(StateProvider stateProvider) : Command<ConfigureModLoaderSettings>
{
    public override int Execute(CommandContext context, ConfigureModLoaderSettings settings)
    {
        stateProvider.Options.ModLoaderType = settings.ModLoaderType;
        stateProvider.SaveOptions();
        return 0;
    }
}

public class ConfigureModWriterCommand(StateProvider stateProvider) : Command<ConfigureModWriterSettings>
{
    public override int Execute(CommandContext context, ConfigureModWriterSettings settings)
    {
        stateProvider.Options.ModWriterOptions.OutputDirectory = settings.OutputDirectory.ToString();
        stateProvider.SaveOptions();
        return 0;
    }
    
    public override ValidationResult Validate(CommandContext context, ConfigureModWriterSettings settings)
    {
        if (!Directory.Exists(settings.OutputDirectory))
        {
            return ValidationResult.Error($"Path not found - {settings.OutputDirectory}");
        }

        return base.Validate(context, settings);
    }
}