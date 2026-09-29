using System.Threading.Tasks;
using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ShoppingCartScreen;

public interface IUpdateQuantityUseCase
{
    Task<Order> ExecuteAsync(int productId, int quantity);
    Task<Order> Execute(int productId, int quantity);
}
