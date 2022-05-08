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

        ReadMessageViewModel ReadMessage(string messageId);

        Task<bool> ReplyMessageAsync(ReadMessageViewModel replyInput, string sender);
    }
}
