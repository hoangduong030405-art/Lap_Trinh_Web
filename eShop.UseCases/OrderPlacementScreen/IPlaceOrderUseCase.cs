using System.Threading.Tasks;
using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.OrderPlacementScreen;

// Alias forwarding to eShop.UseCases.ShoppingCartScreen.IPlaceOrderUseCase
public interface IPlaceOrderUseCase : eShop.UseCases.ShoppingCartScreen.IPlaceOrderUseCase
{
}

public class PlaceOrderUseCase : eShop.UseCases.ShoppingCartScreen.PlaceOrderUseCase, IPlaceOrderUseCase
{
    public PlaceOrderUseCase(
        IOrderService orderService,
        IOrderRepository orderRepository,
        IShoppingCart shoppingCart,
        IShoppingCartStateStore shoppingCartStateStore)
        : base(orderService, orderRepository, shoppingCart, shoppingCartStateStore)
    {
    }
}
