using System.Threading.Tasks;
using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ShoppingCartScreen;

public interface IDeleteProductFromCartUseCase
{
    Task<Order> ExecuteAsync(int productId);
}

// Alias for flexibility
public interface IDeleteProductUseCase : IDeleteProductFromCartUseCase
{
}
