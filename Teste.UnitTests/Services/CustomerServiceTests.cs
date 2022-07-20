using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Entity;
using Teste.Interfaces;
using Teste.Repository;
using Xunit;

namespace Teste.UnitTests.Services
{
    public class CustomerServiceTests
    {
        private ICustomerService _sut;
        private readonly Mock<ICustomerBoaVistaService> _customerBoaVistaService = new();
        private readonly Mock<ICustomerSerasaService> _customerSerasaService = new();
        private readonly IValidator<Customer> _validator = new CustomerValidator();
        public CustomerServiceTests()
        {
            _sut = new CustomerService(_customerBoaVistaService.Object,
                                       _customerSerasaService.Object,
                                       _validator
                                      );
        }

        [Fact]
        public async Task Create_VadationInputDadosFalse_ReturnFail()
        {
            //ARRENGE - MOCK - RETURNO INTERFACES


            //ACT - AÇÃO - ONDE PLAY ACONTECE
            var (isSucess, id, error) = await _sut.Create(new Customer());

            //ASSERT - VALIDAÇÃO DA SAÍDAS
            Assert.False(isSucess);
            Assert.Null(id);
            Assert.NotNull(error);
        }

        [Fact]
        public async Task Create_BoaVistaNegaCustomer_ReturnFail()
        {
            //ARRENGE - MOCK - RETURNO INTERFACES
            _customerBoaVistaService
                .Setup(mock => mock.Validade(It.IsAny<Customer>()))
                .ReturnsAsync((false, "Erro de score"));


            //ACT - AÇÃO - ONDE PLAY ACONTECE
            var (isSucess, id, error) = await _sut
                .Create(new Customer { Id = 1, Name = "José", Document = "12345678910"});

            //ASSERT - VALIDAÇÃO DA SAÍDAS
            Assert.False(isSucess);
            Assert.Null(id);
            Assert.NotNull(error);
            Assert.Contains("Erro de score", error);
        }

        [Fact]
        public async Task Create_SerasaNegaCustomer_ReturnFail()
        {
            //ARRENGE - MOCK - RETURNO INTERFACES
            _customerBoaVistaService
                .Setup(mock => mock.Validade(It.IsAny<Customer>()))
                .ReturnsAsync((true, null));

            _customerSerasaService
                .Setup(mock => mock.Validade(It.IsAny<Customer>()))
                .ReturnsAsync((false, "Negativacao Serasa"));

            //ACT - AÇÃO - ONDE PLAY ACONTECE
            var (isSucess, id, error) = await _sut
                .Create(new Customer { Id = 1, Name = "José", Document = "12345678910" });

            //ASSERT - VALIDAÇÃO DA SAÍDAS
            Assert.False(isSucess);
            Assert.Null(id);
            Assert.NotNull(error);
            Assert.Contains("Negativacao Serasa", error);
        }

        [Fact]
        public async Task Create_ReturnSucess()
        {
            //ARRENGE - MOCK - RETURNO INTERFACES
            _customerBoaVistaService
                .Setup(mock => mock.Validade(It.IsAny<Customer>()))
                .ReturnsAsync((true, null));

            _customerSerasaService
                .Setup(mock => mock.Validade(It.IsAny<Customer>()))
                .ReturnsAsync((true, null));

            //ACT - AÇÃO - ONDE PLAY ACONTECE
            var (isSucess, id, error) = await _sut
                .Create(new Customer { Id = 1, Name = "José", Document = "12345678910" });

            //ASSERT - VALIDAÇÃO DA SAÍDAS
            Assert.True(isSucess);
            Assert.NotNull(id);
            Assert.Null(error);
        }

    }
}
