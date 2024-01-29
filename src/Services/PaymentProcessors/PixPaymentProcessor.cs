using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.PaymentProcessors
{
    public class PixPaymentProcessor : IPaymentProcessor
    {
        public string Type => "pix";

        public Task ProcessPaymentAsync(decimal paymentValue, int customerId)
        {
            return Task.CompletedTask;
        }
    }
}
