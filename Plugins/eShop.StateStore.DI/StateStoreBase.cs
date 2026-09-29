using System;
using eShop.UseCases.PluginInterfaces.StateStore;

namespace eShop.StateStore.DI;

public class StateStoreBase : IStateStore
{
    protected Action? listeners;

    public void AddStateChangeListeners(Action listener)
    {
        listeners += listener;
    }

    public void RemoveStateChangeListeners(Action listener)
    {
        listeners -= listener;
    }

    public void BroadCastStateChange()
    {
        listeners?.Invoke();
    }

    public void BroadcastStateChange()
    {
        listeners?.Invoke();
    }
}
