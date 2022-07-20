using Teste.Entity;

namespace Teste.Interfaces
{
    public interface ICustomerSerasaService
    {
        Task<(bool isSucess, string? error)> Validade(Customer customer);
    }
}
