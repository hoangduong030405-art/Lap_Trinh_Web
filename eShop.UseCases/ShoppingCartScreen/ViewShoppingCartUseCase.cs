using System.Threading.Tasks;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ShoppingCartScreen;

public class ViewShoppingCartUseCase : IViewShoppingCartUseCase
{
    private readonly IShoppingCart shoppingCart;

    public ViewShoppingCartUseCase(IShoppingCart shoppingCart)
    {
        this.shoppingCart = shoppingCart;
    }

    public Task<Order> ExecuteAsync()
    {
        return shoppingCart.GetOrderAsync();
    }

    public Task<Order> Execute()
    {
        return ExecuteAsync();
    }
}
