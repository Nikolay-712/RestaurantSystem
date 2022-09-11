namespace RestaurantSystem.Web.ViewModels.Payments
{
    using System.Collections.Generic;

    public class ProcessPaymentResult
    {
        public ProcessPaymentResult()
        {
            this.Errors = new HashSet<string>();
        }

        public string PaymentId { get; set; }

        public string TransactionId { get; set; }

        public bool IsSuccessful { get; set; }

        public HashSet<string> Errors { get; set; }
    }
}
