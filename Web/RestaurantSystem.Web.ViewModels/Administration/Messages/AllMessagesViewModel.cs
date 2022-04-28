namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System.Collections.Generic;

    public class AllMessagesViewModel : PagingViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }

        public int UnreadMessagesCount { get; set; }
    }
}
