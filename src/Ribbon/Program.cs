using Microsoft.Extensions.DependencyInjection;
using Ribbon;
using Ribbon.Commands.Mods;
using Ribbon.Commands.State;
using Ribbon.Services.Adapter;
using Ribbon.Services.Manager;
using Ribbon.Services.State;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();

var stateProvider = new StateProvider();
registrations.AddSingleton<StateProvider>(_ => stateProvider);

var modManagerBuilder = new ModManagerBuilder(stateProvider);
modManagerBuilder.AddWriter();
registrations.AddSingleton<ModManager>(_ => modManagerBuilder.Build());

var modAdapterBuilder = new ModAdapterBuilder(stateProvider);
modAdapterBuilder.AddRepository();
registrations.AddSingleton<ModAdapter>(_ => modAdapterBuilder.Build());

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
        add.AddCommand<ConfigureModWriterCommand>("modOutput")
            .WithDescription("Configure location to output saved mods");
    });
    config.AddCommand<ListModsCommand>("list")
        .WithDescription("Lists all mods.");
    config.AddCommand<AddModCommand>("add")
        .WithDescription("Gets the file size for a directory.");
    config.AddCommand<ClearModsCommand>("clear")
        .WithDescription("Clears all mods.");
});

return app.Run(args);