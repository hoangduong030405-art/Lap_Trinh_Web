using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;

namespace eShop.UseCases.ViewProductScreen;

public class ViewProductUseCase : IViewProductUseCase, IViewProduct
{
    private readonly IProductRepository productRepository;

    public ViewProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public Product? Execute(int id)
    {
        return productRepository.GetProduct(id) ?? productRepository.GetProductById(id);
    }

    public Product? GetProductById(int id)
    {
        return Execute(id);
    }
}

public class ViewProduct : ViewProductUseCase
{
    public ViewProduct(IProductRepository productRepository) : base(productRepository)
    {
    }
}
