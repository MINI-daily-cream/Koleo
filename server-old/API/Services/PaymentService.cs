using API.Services.Interfaces;
namespace Koleo.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> ProceedPayment()
        {
            return true;
            // redirect to outside service like Stripe/Przelewy24.pl?
        }
        public async Task<bool> CancelPayment() {
            return true;
        }
    }
}
