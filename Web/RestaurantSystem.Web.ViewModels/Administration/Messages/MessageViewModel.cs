namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System;
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;

    public class MessageViewModel : IMapFrom<AppMessage>
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string MessageType { get; init; }

        public string Message { get; init; }

        public string UserId { get; init; }

        public string Status { get; init; }

        public IEnumerable<ReplieViewModel> Replies { get; set; }
    }
}
