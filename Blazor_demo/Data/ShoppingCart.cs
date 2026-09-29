using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;

namespace Blazor_demo.Data;

public class ShoppingCart : IShoppingCart
{
    private const string cstrShoppingCart = "eShop.ShoppingCart";
    private readonly ProtectedLocalStorage protectedLocalStorage;
    private Order? cachedOrder;

    public ShoppingCart(ProtectedLocalStorage protectedLocalStorage)
    {
        this.protectedLocalStorage = protectedLocalStorage;
    }

    public async Task<Order> GetOrderAsync()
    {
        try
        {
            var result = await protectedLocalStorage.GetAsync<Order>(cstrShoppingCart);
            if (result.Success && result.Value != null)
            {
                cachedOrder = result.Value;
                return cachedOrder;
            }
        }
        catch
        {
            // Xảy ra khi đang Prerender hoặc chưa sẵn sàng JS interop
        }

        cachedOrder ??= new Order();
        return cachedOrder;
    }

    public async Task<Order> AddProductAsync(Product product, int quantity = 1)
    {
        var order = await GetOrderAsync();
        order.AddProduct(product, quantity);
        await SetOrderAsync(order);
        return order;
    }

    public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
    {
        var order = await GetOrderAsync();
        order.UpdateQuantity(productId, quantity);
        await SetOrderAsync(order);
        return order;
    }

    public async Task<Order> DeleteProductAsync(int productId)
    {
        var order = await GetOrderAsync();
        order.RemoveProduct(productId);
        await SetOrderAsync(order);
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        await SetOrderAsync(order);
        return order;
    }

    public async Task EmptyAsync()
    {
        cachedOrder = new Order();
        await SetOrderAsync(cachedOrder);
    }

    private async Task SetOrderAsync(Order order)
    {
        cachedOrder = order;
        try
        {
            await protectedLocalStorage.SetAsync(cstrShoppingCart, order);
        }
        catch
        {
            // Bỏ qua nếu chưa sẵn sàng JS interop
        }
    }
}
