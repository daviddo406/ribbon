using Ribbon.Repositories;
using Ribbon.Services.State;

namespace Ribbon.Services.Adapter;

public class ModAdapterBuilder
{
    private Adapter.ModAdapter? _modAdapter;
    
    private ModRepository _modRepository;

    private readonly StateProvider _stateProvider;
    
    public ModAdapterBuilder(StateProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public ModAdapter Build()
    {
        _modAdapter = new Adapter.ModAdapter(_modRepository, _stateProvider);
        return _modAdapter;
    }
    
    public void AddRepository()
    {
        _modRepository = new ModRepository(_stateProvider);
    }
}