using System.Threading.Tasks;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ShoppingCartScreen;

public class DeleteProductUseCase : IDeleteProductUseCase, IDeleteProductFromCartUseCase
{
    private readonly IShoppingCart shoppingCart;
    private readonly IShoppingCartStateStore shoppingCartStateStore;

    public DeleteProductUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore shoppingCartStateStore)
    {
        this.shoppingCart = shoppingCart;
        this.shoppingCartStateStore = shoppingCartStateStore;
    }

    public async Task<Order> Execute(int productId)
    {
        var order = await this.shoppingCart.DeleteProductAsync(productId);
        this.shoppingCartStateStore.UpdateLineItemsCount();
        return order;
    }

    public Task<Order> ExecuteAsync(int productId)
    {
        return Execute(productId);
    }
}

public class DeleteProductFromCartUseCase : DeleteProductUseCase
{
    public DeleteProductFromCartUseCase(IShoppingCart shoppingCart, IShoppingCartStateStore shoppingCartStateStore)
        : base(shoppingCart, shoppingCartStateStore)
    {
    }
}
