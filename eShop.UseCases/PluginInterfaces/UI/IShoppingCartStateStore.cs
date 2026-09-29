using System;
using System.Threading.Tasks;

namespace eShop.UseCases.PluginInterfaces.UI;

public interface IShoppingCartStateStore
{
    void AddStateChangeListeners(Action listener);
    void RemoveStateChangeListeners(Action listener);
    void BroadcastStateChange();
    Task<int> GetItemsCount();
}
