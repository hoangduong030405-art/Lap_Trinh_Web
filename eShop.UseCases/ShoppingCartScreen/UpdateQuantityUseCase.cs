using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ShoppingCartScreen;

public class UpdateQuantityUseCase : IUpdateQuantityUseCase
{
    private readonly IShoppingCart shoppingCart;
    private readonly IShoppingCartStateStore stateStore;

    public UpdateQuantityUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore stateStore)
    {
        this.shoppingCart = shoppingCart;
        this.stateStore = stateStore;
    }

    public async Task<Order> ExecuteAsync(int productId, int quantity)
    {
        var order = await shoppingCart.UpdateQuantityAsync(productId, quantity);
        this.stateStore.UpdateLineItemsCount();
        return order;
    }

    public Task<Order> Execute(int productId, int quantity)
    {
        return ExecuteAsync(productId, quantity);
    }
}
