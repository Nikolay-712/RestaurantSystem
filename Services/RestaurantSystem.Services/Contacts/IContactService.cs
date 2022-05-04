namespace RestaurantSystem.Services.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Administration.Messages;
    using RestaurantSystem.Web.ViewModels.Contacts;

    public interface IContactService
    {
        Task SendMessageAsync(MessageInputVewModel messageInput, string userId);

        IEnumerable<T> GetMessages<T>();

        AllMessagesViewModel AllMessages();

        Task<bool> ReplyMessage(AllMessagesViewModel replyInput, string messageId);
    }
}
