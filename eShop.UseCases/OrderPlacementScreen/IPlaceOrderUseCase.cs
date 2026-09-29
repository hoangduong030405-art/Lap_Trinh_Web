using System.Threading.Tasks;
using eShop.CoreBusiness.Models;

namespace eShop.UseCases.OrderPlacementScreen;

public interface IPlaceOrderUseCase
{
    Task<string?> ExecuteAsync(Order order);
}
