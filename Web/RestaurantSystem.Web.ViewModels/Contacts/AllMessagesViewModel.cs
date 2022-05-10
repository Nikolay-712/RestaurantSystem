namespace RestaurantSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    public class AllMessagesViewModel : PagingViewModel
    {
        public IEnumerable<AppMessageViewModel> Messages { get; set; }

        public int UnreadMessagesCount { get; set; }
    }
}
