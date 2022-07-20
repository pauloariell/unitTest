using Teste.Entity;

namespace Teste.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool isSucess, int? id, string? error)> Create(Customer customer);
    }
}
