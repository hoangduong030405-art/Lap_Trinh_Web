using System;
using eShop.CoreBusiness.Models;

namespace eShop.CoreBusiness.Services;

public class OrderService : IOrderService
{
    public bool ValidateCreateOrder(Order order)
    {
        if (order == null) return false;

        // Đơn hàng phải có ít nhất 1 sản phẩm
        if (order.LineItems == null || order.LineItems.Count == 0) return false;

        // Từng mặt hàng phải hợp lệ
        foreach (var item in order.LineItems)
        {
            if (item.ProductId <= 0 || item.Price <= 0 || item.Quantity <= 0)
                return false;
        }

        // Bắt buộc phải có đầy đủ thông tin khách hàng
        if (string.IsNullOrWhiteSpace(order.CustomerName) ||
            string.IsNullOrWhiteSpace(order.CustomerAddress) ||
            string.IsNullOrWhiteSpace(order.CustomerCity) ||
            string.IsNullOrWhiteSpace(order.CustomerStateProvince) ||
            string.IsNullOrWhiteSpace(order.CustomerCountry))
            return false;

        return true;
    }

    public bool ValidateUpdateOrder(Order order)
    {
        if (order == null || !order.OrderId.HasValue || order.OrderId.Value <= 0)
            return false;

        return ValidateCreateOrder(order);
    }

    public bool ValidateProcessOrder(Order order)
    {
        if (!order.DateProcessed.HasValue || string.IsNullOrWhiteSpace(order.AdminUser))
            return false;

        return true;
    }
}
