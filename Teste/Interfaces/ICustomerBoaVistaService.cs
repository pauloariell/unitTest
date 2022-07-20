using Teste.Entity;

namespace Teste.Interfaces
{
    public interface ICustomerBoaVistaService
    {
        Task<(bool isSucess, string? error)> Validade(Customer customer);
    }
}
