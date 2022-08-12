﻿namespace RestaurantSystem.Web.ViewModels.Notifications
{
    using System;

    using RestaurantSystem.Data.Models.Notifications;
    using RestaurantSystem.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string UserId { get; init; }

        public string Message { get; init; }

        public string OrderId { get; init; }

        public string ReservationId { get; init; }

        public bool IsSeen { get; set; }
    }
}
