namespace RestaurantSystem.Web.ViewModels.Contacts
{
    using System;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;

    public class ReplyMessageViewModel : IMapFrom<MessageReply>
    {
        public DateTime CreatedOn { get; init; }

        public string Text { get; init; }

        public string MessageId { get; init; }

        public string Sender { get; init; }

        public bool IsRead { get; init; }
    }
}
