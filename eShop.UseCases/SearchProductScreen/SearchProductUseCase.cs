using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.SearchProductScreen;

public class SearchProductUseCase : ISearchProductUseCase, ISearchProduct
{
    private readonly IProductRepository productRepository;

    public SearchProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public IEnumerable<Product> Execute(string? filter = null)
    {
        return productRepository.GetProducts(filter);
    }

    public IEnumerable<Product> GetProducts(string? filter = null)
    {
        return Execute(filter);
    }
}

public class Searchproduct : SearchProductUseCase
{
    public Searchproduct(IProductRepository productRepository) : base(productRepository)
    {
    }
}
