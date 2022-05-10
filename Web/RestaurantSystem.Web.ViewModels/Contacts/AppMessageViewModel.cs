namespace RestaurantSystem.Web.ViewModels.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RestaurantSystem.Common;
    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;

    public class AppMessageViewModel : IMapFrom<AppMessage>
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string MessageType { get; init; }

        public string Message { get; init; }

        public string UserId { get; init; }

        public string Status { get; init; }

        public bool IsOpen { get; set; }

        public IEnumerable<ReplyMessageViewModel> Replies { get; init; }

        public ReplyInputViewModel ReplyInput { get; set; }

        public int NewUserRepliesCount => this.Replies
                  .Where(x => x.Sender == GlobalConstants.Message.UserSender)
                  .Where(x => x.IsRead == false).Count();

        public int NewAdminRepliesCount => this.Replies
                  .Where(x => x.Sender == GlobalConstants.Message.AdminSender)
                  .Where(x => x.IsRead == false).Count();
    }
}
