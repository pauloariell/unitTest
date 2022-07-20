using FluentValidation;
using Teste.Entity;

namespace Teste.Configurations
{
    public static class FluentValidationExtention
    {
        //Metodo de extensão
        public static IServiceCollection AddValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Customer>, CustomerValidator>();
            return services;
        }
    }
}
