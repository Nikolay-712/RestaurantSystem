namespace RestaurantSystem.Services.Payments
{
    using System;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Web.ViewModels.Payments;

    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PaymentService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<string> MakePaymentAsync(string orderId, PaymentsInputModel paymentsInput)
        {
            var payment = new Payment
            {
                CreatedOn = DateTime.Now,
                PaymentType = paymentsInput.PaymentType,
                OrderId = orderId,
            };

            switch (paymentsInput.PaymentType)
            {
                case PaymentType.Cash:
                    payment.IsSuccessful = true;
                    break;
                case PaymentType.DebitCard:
                    break;
                default:
                    break;
            }

            if (!payment.IsSuccessful)
            {
                return null;
            }

            await this.applicationDbContext.Payments.AddAsync(payment);
            await this.applicationDbContext.SaveChangesAsync();

            return payment.Id;
        }
    }
}
