namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;

    public class MessageViewModel : IMapFrom<ApplicationMessage>
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string MessageType { get; init; }

        public string Sender { get; init; }

        public string Message { get; init; }

        public bool IsRead { get; init; }
    }
}
