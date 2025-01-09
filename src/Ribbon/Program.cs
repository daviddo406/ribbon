using Microsoft.Extensions.DependencyInjection;
using Ribbon;
using Ribbon.Commands;
using Ribbon.State;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();
registrations.AddSingleton<StateProvider>();
registrations.AddSingleton<ModRepository>();
registrations.AddSingleton<ModAdapter>();
registrations.AddSingleton<ModWriter>();
registrations.AddSingleton<ModManager>();

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var registrar = new TypeRegistrar(registrations);

var app = new CommandApp(registrar);
app.Configure(config =>
{
    config.AddBranch<ConfigureSettings>("configure", add =>
    {
        add.AddCommand<ConfigureGameVersionCommand>("gameVersion")
            .WithDescription("Configure game version");
        add.AddCommand<ConfigureModLoaderCommand>("modLoader")
            .WithDescription("Configure mod loader");
    });
    config.AddCommand<ListModsCommand>("list")
        .WithDescription("Lists all mods.");
    config.AddCommand<AddModCommand>("add")
        .WithDescription("Gets the file size for a directory.");
    config.AddCommand<ClearModsCommand>("clear")
        .WithDescription("Clears all mods.");
});

return app.Run(args);