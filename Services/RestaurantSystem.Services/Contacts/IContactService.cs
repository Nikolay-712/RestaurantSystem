namespace RestaurantSystem.Services.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Web.ViewModels.Contacts;

    public interface IContactService
    {
        Task<string> SendMessageAsync(MessageInputVewModel messageInput, string userId);

        IEnumerable<T> GetMessages<T>();

        AllMessagesViewModel AllMessages(int page);

        AppMessageViewModel ReadMessage(string messageId);

        AppMessage GetMessageById(string messageId);

        Task ChangeMessageStatusAsync(AppMessage message, MessageStatus status);

        Task CloseDiscussionAsync(AppMessage message);

        Task<bool> ReplyMessageAsync(string messageId, string text, string sender);
    }
}
