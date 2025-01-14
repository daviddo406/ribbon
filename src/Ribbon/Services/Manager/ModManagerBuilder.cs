using Ribbon.Services.Manager.Writer;
using Ribbon.Services.State;

namespace Ribbon.Services.Manager;

public class ModManagerBuilder
{
    private ModManager _modManager = new ModManager();

    private ModWriter? _modWriter;

    private readonly StateProvider _stateProvider;
    
    public ModManagerBuilder(StateProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public ModManager Build()
    {
        _modManager.Initialize();
        return _modManager;
    }
    
    public void AddWriter()
    {
        _modWriter = new ModWriter(_stateProvider.Options.ModWriterOptions);
        _modManager.ModWriterOptions = _stateProvider.Options.ModWriterOptions;
        _modManager.Subscribe(_modWriter.Write);
    }
    
    
}