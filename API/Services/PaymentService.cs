namespace Koleo.Services
{
    public class PaymentService
    {
        public bool ProceedPayment()
        {
            return true;
            // redirect to outside service like Stripe/Przelewy24.pl?
        }
        public bool CancelPayment() {
            return true;
        }
    }
}
