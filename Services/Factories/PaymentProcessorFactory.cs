using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.Factories
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentProcessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProcessor GetByType(string type)
        {
            var services = _serviceProvider.GetServices<IPaymentProcessor>();

            return services.FirstOrDefault(service => string.Equals(service.Type, type, StringComparison.OrdinalIgnoreCase))
                ?? throw new ArgumentException("Invalid service name", nameof(type));
        }
    }
}
