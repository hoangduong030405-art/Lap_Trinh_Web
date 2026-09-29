using System;
using System.Threading.Tasks;
using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.OrderPlacementScreen;

public class PlaceOrderUseCase : IPlaceOrderUseCase
{
    private readonly IOrderService orderService;
    private readonly IOrderRepository orderRepository;
    private readonly IShoppingCart shoppingCart;
    private readonly IShoppingCartStateStore stateStore;

    public PlaceOrderUseCase(
        IOrderService orderService,
        IOrderRepository orderRepository, 
        IShoppingCart shoppingCart,
        IShoppingCartStateStore stateStore)
    {
        this.orderService = orderService;
        this.orderRepository = orderRepository;
        this.shoppingCart = shoppingCart;
        this.stateStore = stateStore;
    }

    public async Task<string?> ExecuteAsync(Order order)
    {
        if (order == null || !orderService.ValidateCreateOrder(order)) 
            return null;

        order.DatePlaced = DateTime.UtcNow;
        if (string.IsNullOrEmpty(order.UniqueId))
        {
            order.UniqueId = Guid.NewGuid().ToString();
        }

        orderRepository.CreateOrder(order);
        await shoppingCart.EmptyAsync();
        stateStore.BroadcastStateChange();

        return order.UniqueId;
    }
}
