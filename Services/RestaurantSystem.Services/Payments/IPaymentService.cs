namespace RestaurantSystem.Services.Payments
{
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Web.ViewModels.Payments;

    public interface IPaymentService
    {
        Task<string> MakePaymentAsync(string orderId, PaymentsInputModel paymentsInput);
    }
}
