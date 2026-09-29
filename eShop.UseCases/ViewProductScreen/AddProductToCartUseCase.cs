using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;

namespace eShop.UseCases.ViewProductScreen;

public class AddProductToCartUseCase : IAddProductToCartUseCase, IAddProductToShoppingCartUseCase
{
    private readonly IProductRepository productRepository;
    private readonly IShoppingCart shoppingCart;
    private readonly IShoppingCartStateStore stateStore;

    public AddProductToCartUseCase(
        IProductRepository productRepository, 
        IShoppingCart shoppingCart,
        IShoppingCartStateStore stateStore)
    {
        this.productRepository = productRepository;
        this.shoppingCart = shoppingCart;
        this.stateStore = stateStore;
    }

    public async Task ExecuteAsync(int productId, int quantity = 1)
    {
        var product = productRepository.GetProductById(productId) ?? productRepository.GetProduct(productId);
        if (product != null)
        {
            await shoppingCart.AddProductAsync(product, quantity);
            this.stateStore.UpdateLineItemsCount();
        }
    }

    public Task Execute(int productId)
    {
        return ExecuteAsync(productId, 1);
    }
}
