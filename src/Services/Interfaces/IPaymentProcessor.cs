namespace ProvaPub.Services.Interfaces
{
    public interface IPaymentProcessor
    {

        string Type { get; }

        Task ProcessPaymentAsync(decimal paymentValue, int customerId);
    }
}
