using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.PaymentProcessors
{
    public class PaypalPaymentProcessor : IPaymentProcessor
    {
        public string Type => "paypal";

        public Task ProcessPaymentAsync(decimal paymentValue, int customerId)
        {
            return Task.CompletedTask;
        }
    }
}
