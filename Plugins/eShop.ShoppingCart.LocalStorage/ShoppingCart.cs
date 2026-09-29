using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.ShoppingCart.LocalStorage;

public class ShoppingCart : IShoppingCart
{
    private const string cstrShoppingCart = "eShop.ShoppingCart";
    private readonly IJSRuntime jsRuntime;
    private Order? cachedOrder;

    public ShoppingCart(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public async Task<Order> GetOrderAsync()
    {
        if (cachedOrder != null) return cachedOrder;

        Order? order = null;
        try
        {
            var strOrder = await jsRuntime.InvokeAsync<string>("localStorage.getItem", cstrShoppingCart);
            if (!string.IsNullOrEmpty(strOrder))
            {
                order = JsonConvert.DeserializeObject<Order>(strOrder);
            }
        }
        catch
        {
            // Prerendering or JS not yet available
        }

        if (order == null)
        {
            order = new Order();
            await SetOrder(order);
        }

        cachedOrder = order;
        return order;
    }

    public async Task<Order> AddProductAsync(Product product, int quantity = 1)
    {
        var order = await GetOrderAsync();
        order.AddProduct(product, quantity);
        await SetOrder(order);
        return order;
    }

    public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
    {
        var order = await GetOrderAsync();
        order.UpdateQuantity(productId, quantity);
        await SetOrder(order);
        return order;
    }

    public async Task<Order> DeleteProductAsync(int productId)
    {
        var order = await GetOrderAsync();
        order.RemoveProduct(productId);
        await SetOrder(order);
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        await SetOrder(order);
        return order;
    }

    public async Task EmptyAsync()
    {
        cachedOrder = new Order();
        await SetOrder(cachedOrder);
    }

    private async Task SetOrder(Order order)
    {
        cachedOrder = order;
        try
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", cstrShoppingCart, JsonConvert.SerializeObject(order));
        }
        catch
        {
            // Prerendering or JS not yet available
        }
    }
}
