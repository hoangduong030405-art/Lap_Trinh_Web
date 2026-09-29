using System.Threading.Tasks;

namespace eShop.UseCases.ViewProductScreen;

public interface IAddProductToCartUseCase
{
    Task ExecuteAsync(int productId, int quantity = 1);
    void Execute(int productId);
}

public interface IAddProductToShoppingCartUseCase : IAddProductToCartUseCase
{
}
