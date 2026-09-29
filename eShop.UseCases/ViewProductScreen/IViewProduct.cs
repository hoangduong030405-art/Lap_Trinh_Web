using eShop.CoreBusiness.Models;

namespace eShop.UseCases.ViewProductScreen;

public interface IViewProduct
{
    Product? Execute(int id);
    Product? GetProductById(int id);
}
