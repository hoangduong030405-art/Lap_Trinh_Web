using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.SearchProductScreen;

public class Searchproduct : ISearchProduct
{
    private readonly IProductRepository productRepository;

    public Searchproduct(IProductRepository productRepository)
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

public class SearchProduct : Searchproduct
{
    public SearchProduct(IProductRepository productRepository) : base(productRepository)
    {
    }
}