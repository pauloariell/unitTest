using FluentValidation;
using Teste.Entity;
using Teste.Interfaces;

namespace Teste.Repository
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerBoaVistaService _customerBoaVistaService;
        private readonly ICustomerSerasaService _customerSerasaService;
        private readonly IValidator<Customer> _validator;

        public CustomerService(ICustomerBoaVistaService customerBoaVistaService, 
                               ICustomerSerasaService customerSerasaService, 
                               IValidator<Customer> validator)
        {
            _customerBoaVistaService = customerBoaVistaService;
            _customerSerasaService = customerSerasaService;
            _validator = validator;
        }

        public async Task<(bool isSucess, int? id, string? error)> Create(Customer customer)
        {
            var validation = _validator.Validate(customer);
            if (!validation.IsValid)
                return (false, null, string.Join(", ", validation.Errors.Select(x => x.ErrorMessage)));

            var ( isSucess, error) = await _customerBoaVistaService.Validade(customer);
            if (!isSucess)
                return (false, null, error);

            var (isSucessSerasa, errorSerasa) = await _customerSerasaService.Validade(customer);
            if (!isSucessSerasa)
                return (false, null, errorSerasa);

            return (true, 2, null);
        }
    }
}
