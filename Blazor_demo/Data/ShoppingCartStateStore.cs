using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace Blazor_demo.Data;

public class ShoppingCartStateStore : IShoppingCartStateStore
{
    protected Action? listeners;
    private readonly IShoppingCart shoppingCart;

    public ShoppingCartStateStore(IShoppingCart shoppingCart)
    {
        this.shoppingCart = shoppingCart;
    }

    public void AddStateChangeListeners(Action listener)
    {
        listeners += listener;
    }

    public void RemoveStateChangeListeners(Action listener)
    {
        listeners -= listener;
    }

    public void BroadcastStateChange()
    {
        listeners?.Invoke();
    }

    public async Task<int> GetItemsCount()
    {
        var order = await shoppingCart.GetOrderAsync();
        return order?.LineItems?.Sum(x => x.Quantity) ?? 0;
    }

    public void UpdateLineItemsCount()
    {
        BroadcastStateChange();
    }
}
