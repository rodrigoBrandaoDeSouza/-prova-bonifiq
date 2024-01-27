using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.PaymentProcessors
{
    public class CreditCardPaymentProcessor : IPaymentProcessor
    {
        public string Type => "creditCard";

        public Task ProcessPaymentAsync(decimal paymentValue, int customerId)
        {
            return Task.CompletedTask;
        }
    }
}
