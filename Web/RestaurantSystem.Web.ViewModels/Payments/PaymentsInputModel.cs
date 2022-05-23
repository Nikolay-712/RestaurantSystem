namespace RestaurantSystem.Web.ViewModels.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models.Payments;

    using static RestaurantSystem.Common.GlobalConstants;

    public class PaymentsInputModel
    {
        public string CardNumber { get; init; }

        public string CardName { get; init; }

        public DateTime? Expiration { get; init; }

        public string CVV { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [EnumDataType(typeof(PaymentType), ErrorMessage = CategoryErrorMesage)]
        public PaymentType PaymentType { get; init; }
    }
}
