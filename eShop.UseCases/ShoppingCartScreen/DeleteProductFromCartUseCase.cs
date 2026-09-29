using System.Threading.Tasks;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ShoppingCartScreen;

public class DeleteProductFromCartUseCase : IDeleteProductFromCartUseCase, IDeleteProductUseCase
{
    private readonly IShoppingCart shoppingCart;
    private readonly IShoppingCartStateStore stateStore;

    public DeleteProductFromCartUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore stateStore)
    {
        this.shoppingCart = shoppingCart;
        this.stateStore = stateStore;
    }

    public async Task<Order> ExecuteAsync(int productId)
    {
        var order = await shoppingCart.DeleteProductAsync(productId);
        stateStore.BroadcastStateChange();
        return order;
    }
}
