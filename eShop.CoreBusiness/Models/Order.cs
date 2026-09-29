using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eShop.CoreBusiness.Models;

public class Order
{
    public int? Id { get; set; }
    public int? OrderId
    {
        get => Id;
        set => Id = value;
    }
    public DateTime? DatePlaced { get; set; }
    public DateTime? DateFulfilled { get; set; }
    public DateTime? DateProcessed
    {
        get => DateFulfilled;
        set => DateFulfilled = value;
    }

    [Required(ErrorMessage = "Customer name is required.")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string? CustomerAddress { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string? CustomerCity { get; set; }

    public string? CustomerStateProvince { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    public string? CustomerCountry { get; set; }

    public string? AdminUser { get; set; }
    public List<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
    public string? UniqueId { get; set; }

    // Business Rules / Domain Calculations
    public double TotalPrice => LineItems.Sum(x => x.Price * x.Quantity);
    public int TotalItems => LineItems.Sum(x => x.Quantity);
    public int LineItemsCount => LineItems.Count;

    // Business Rules: Thêm sản phẩm vào đơn hàng
    public void AddProduct(Product product, int quantity = 1)
    {
        var item = LineItems.FirstOrDefault(x => x.ProductId == product.Id);
        if (item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            LineItems.Add(new OrderLineItem
            {
                ProductId = product.Id,
                Product = product,
                Price = product.Price,
                Quantity = quantity,
                OrderId = this.OrderId
            });
        }
    }

    public void AddProduct(int productId, int quantity, double price)
    {
        var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            LineItems.Add(new OrderLineItem
            {
                ProductId = productId,
                Price = price,
                Quantity = quantity,
                OrderId = this.OrderId
            });
        }
    }

    // Business Rules: Xóa sản phẩm khỏi đơn hàng
    public void RemoveProduct(int productId)
    {
        var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            LineItems.Remove(item);
        }
    }

    // Business Rules: Cập nhật số lượng sản phẩm
    public void UpdateQuantity(int productId, int quantity)
    {
        var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            if (quantity <= 0)
                LineItems.Remove(item);
            else
                item.Quantity = quantity;
        }
    }

    // Business Rules: Kiểm tra tính hợp lệ của đơn hàng
    public bool IsValid()
    {
        if (LineItems == null || LineItems.Count == 0) return false;
        foreach (var item in LineItems)
        {
            if (item.Quantity <= 0 || item.Price < 0) return false;
        }
        return true;
    }
}
