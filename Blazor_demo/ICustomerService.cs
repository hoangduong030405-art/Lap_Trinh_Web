namespace Blazor_demo
{
    public interface ICustomerService
    {
        string Uid { get; set; }
        Customer? GetCustomerById(int id);
    }
}
