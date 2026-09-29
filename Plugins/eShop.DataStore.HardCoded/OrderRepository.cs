using System;
using System.Collections.Generic;
using System.Linq;
using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;

namespace eShop.DataStore.HardCoded;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> orders = new List<Order>();
    private readonly Dictionary<int, Order> ordersDict = new Dictionary<int, Order>();

    public int CreateOrder(Order order)
    {
        order.OrderId = orders.Count + 1;
        order.Id = order.OrderId;
        order.DatePlaced = DateTime.UtcNow;
        if (string.IsNullOrEmpty(order.UniqueId))
        {
            order.UniqueId = Guid.NewGuid().ToString();
        }

        int lineItemId = 1;
        foreach (var item in order.LineItems)
        {
            item.OrderId = order.OrderId.Value;
            item.Id = lineItemId++;
        }

        orders.Add(order);
        ordersDict[order.OrderId.Value] = order;
        return order.OrderId.Value;
    }

    public Order? GetOrder(int id)
    {
        ordersDict.TryGetValue(id, out var order);
        return order;
    }

    public Order? GetOrderByUniqueId(string uniqueId)
    {
        return orders.FirstOrDefault(x => x.UniqueId == uniqueId);
    }

    public void UpdateOrder(Order order)
    {
        if (order.OrderId.HasValue && ordersDict.ContainsKey(order.OrderId.Value))
        {
            ordersDict[order.OrderId.Value] = order;
        }
    }

    public IEnumerable<Order> GetOrders()
    {
        return orders;
    }

    public IEnumerable<OrderLineItem> GetLineItemsByOrderId(int orderId)
    {
        var order = GetOrder(orderId);
        return order?.LineItems ?? Enumerable.Empty<OrderLineItem>();
    }
}
