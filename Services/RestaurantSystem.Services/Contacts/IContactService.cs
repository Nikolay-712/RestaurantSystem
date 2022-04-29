namespace RestaurantSystem.Services.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Administration.Messages;
    using RestaurantSystem.Web.ViewModels.Contacts;

    public interface IContactService
    {
        Task SendMessageAsync(MessageInputVewModel messageInput, string sender);

        IEnumerable<T> GetMessages<T>();

        AllMessagesViewModel AllMessages();

        Task<bool> ReturnАnswerAsync(AdminMessageViewModel adminMessage);

        AdminMessageViewModel GetMessageAnswers(string messageId);

        Task<bool> ChangeMessageStatmentAsync(string messageId);
    }
}
