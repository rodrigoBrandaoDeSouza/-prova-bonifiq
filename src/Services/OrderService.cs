using ProvaPub.Models;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class OrderService
    {
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;

        public OrderService(IPaymentProcessorFactory paymentProcessorFactory)
        {
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            var service = _paymentProcessorFactory.GetByType(paymentMethod);

            await service.ProcessPaymentAsync(paymentValue, customerId);

            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow
            });
        }
    }
}
