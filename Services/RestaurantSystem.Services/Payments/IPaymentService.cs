namespace RestaurantSystem.Services.Payments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Web.ViewModels.Payments;

    public interface IPaymentService
    {
        Task<ProcessPaymentResult> MakePaymentAsync(string userId, string orderId, PaymentsInputModel paymentsInput, decimal amount);

        IEnumerable<OnlinePaymentViewModel> OnlinePaymentsHistory(string userId);
    }
}
