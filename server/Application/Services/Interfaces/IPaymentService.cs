namespace API.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<bool> ProceedPayment();
        public Task<bool> CancelPayment();
    }
}
