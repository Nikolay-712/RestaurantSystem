namespace RestaurantSystem.Services.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Services.Messaging;
    using RestaurantSystem.Web.ViewModels.Administration.Messages;
    using RestaurantSystem.Web.ViewModels.Contacts;

    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IEmailSender emailSender;

        public ContactService(ApplicationDbContext applicationDbContext, IEmailSender emailSender)
        {
            this.applicationDbContext = applicationDbContext;
            this.emailSender = emailSender;
        }

        public async Task SendMessageAsync(MessageInputVewModel messageInput, string sender)
        {
            var message = new ApplicationMessage
            {
                CreatedOn = DateTime.UtcNow,
                MessageType = messageInput.MessageType,
                Sender = sender,
                Message = messageInput.Message,
                IsRead = false,
            };

            await this.applicationDbContext.Messages.AddAsync(message);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public AllMessagesViewModel AllMessages()
        {
            var messagesList = this.GetMessages<MessageViewModel>()
                .OrderBy(x => x.CreatedOn)
                .ToList();

            var messages = new AllMessagesViewModel
            {
                Messages = messagesList,
                UnreadMessagesCount = messagesList.Where(x => x.IsRead == false).Count(),
            };

            return messages;
        }

        public void ReturnАnswer(string messageId)
        {
            
        }

        public IEnumerable<T> GetMessages<T>()
        {
            var messages = this.applicationDbContext
                .Messages
                .To<T>();

            return messages;
        }
    }
}
