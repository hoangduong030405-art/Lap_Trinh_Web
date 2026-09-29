using System.Threading.Tasks;
using eShop.CoreBusiness.Models;

namespace eShop.UseCases.PluginInterfaces.UI;

public interface IShoppingCart
{
    Task<Order> GetOrderAsync();
    Task<Order> AddProductAsync(Product product, int quantity = 1);
    Task<Order> UpdateQuantityAsync(int productId, int quantity);
    Task<Order> UpdateOrderAsync(Order order);
    Task<Order> DeleteProductAsync(int productId);
    Task EmptyAsync();
}
