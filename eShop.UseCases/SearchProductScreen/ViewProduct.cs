using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;

namespace eShop.UseCases.SearchProductScreen;

public class ViewProduct : IViewProduct
{
    private readonly IProductRepository productRepository;

    public ViewProduct(IProductRepository productRepository)
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
