using eShop.CoreBusiness.Models;

namespace eShop.CoreBusiness.Services;

public interface IOrderService
{
    bool ValidateCreateOrder(Order order);
    bool ValidateUpdateOrder(Order order);
    bool ValidateProcessOrder(Order order);
}
