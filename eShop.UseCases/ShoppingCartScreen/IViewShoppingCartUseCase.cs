using System.Threading.Tasks;
using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ShoppingCartScreen;

public interface IViewShoppingCartUseCase
{
    Task<Order> ExecuteAsync();
}
