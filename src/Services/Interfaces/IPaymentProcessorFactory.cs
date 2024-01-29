namespace ProvaPub.Services.Interfaces
{
    public interface IPaymentProcessorFactory
    {
        IPaymentProcessor GetByType(string type);
    }
}
