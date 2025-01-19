using Ribbon.Repositories;
using Ribbon.Services.State;

namespace Ribbon.Services.Adapter;

public class ModAdapterBuilder
{
    private readonly ModAdapter _modAdapter = new ModAdapter();
    
    private ModRepository? _modRepository;

    private readonly StateProvider _stateProvider;
    
    public ModAdapterBuilder(StateProvider stateProvider)
    {
        _stateProvider = stateProvider;
        _modAdapter.StateProvider = stateProvider;
    }
    
    public ModAdapter AddRepository()
    {
        _modRepository = new ModRepository(_stateProvider);
        _modAdapter.ModRepository = _modRepository;
        return _modAdapter;
    }
}