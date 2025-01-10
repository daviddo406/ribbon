using CurseForge.APIClient.Models.Mods;
using Ribbon.Services.State;
using Spectre.Console.Cli;

namespace Ribbon.Commands.State;

public class ConfigureSettings : CommandSettings
{
}

public class ConfigureGameVersionSettings : ConfigureSettings
{
    [CommandArgument(0, "<version>")]
    public string GameVersion { get; set; }
}

public class ConfigureModLoaderSettings : ConfigureSettings
{
    [CommandArgument(0, "<modloader>")]
    public ModLoaderType? ModLoaderType { get; set; }
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
        stateProvider.Options.ModLoaderType = settings.ModLoaderType ?? ModLoaderType.Any;
        stateProvider.SaveOptions();
        return 0;
    }
}