namespace RestaurantSystem.Services.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Web.ViewModels.Administration.Messages;
    using RestaurantSystem.Web.ViewModels.Contacts;

    public interface IContactService
    {
        Task SendMessageAsync(MessageInputVewModel messageInput, string userId);

        IEnumerable<T> GetMessages<T>();

        AllMessagesViewModel AllMessages(int page);

        ReadMessageViewModel ReadMessage(string messageId);

        AppMessage GetMessageById(string messageId);

        Task ChangeMessageStatusAsync(AppMessage message, MessageStatus status);

        Task CloseDiscussionAsync(AppMessage message);

        Task<bool> ReplyMessageAsync(ReadMessageViewModel replyInput, string sender);
    }
}
