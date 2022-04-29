namespace RestaurantSystem.Services.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                .OrderByDescending(x => x.CreatedOn)
                .OrderBy(x => x.MessageType)
                .OrderByDescending(x => x.IsRead)
                .Where(x => !x.Id.Contains("answer"))
                .ToList();

            var messages = new AllMessagesViewModel
            {
                Messages = messagesList,
                UnreadMessagesCount = messagesList.Where(x => x.IsRead == false).Count(),
            };

            return messages;
        }

        public async Task<bool> ReturnАnswerAsync(AdminMessageViewModel adminMessage)
        {
            if (!await this.ChangeMessageStatmentAsync(adminMessage.Id))
            {
                return false;
            }

            var answer = new ApplicationMessage
            {
                Id = $"answer{adminMessage.Id}",
                CreatedOn = DateTime.UtcNow,
                MessageType = MessageType.Саобщение,
                Sender = "admin@abv.bg",
                Message = adminMessage.Text,
            };

            await this.applicationDbContext.Messages.AddAsync(answer);
            await this.applicationDbContext.SaveChangesAsync();

            await this.emailSender
                 .SendEmailAsync(answer.Sender, "Administration", adminMessage.Sender, answer.Message, null);

            return true;
        }

        public AdminMessageViewModel GetMessageAnswers(string messageId)
        {
            var message = this
                .GetMessages<AdminMessageViewModel>()
                .FirstOrDefault(x => x.Id == messageId);

            if (message == null)
            {
                return message;
            }

            message.Answer = this.GetMessages<AdminMessageViewModel>()
                .FirstOrDefault(x => x.Id == $"answer{message.Id}");

            return message;
        }

        public async Task<bool> ChangeMessageStatmentAsync(string messageId)
        {
            var message = this.applicationDbContext
                .Messages.
                FirstOrDefault(x => x.Id == messageId);

            if (message == null)
            {
                return false;
            }

            message.IsRead = true;

            this.applicationDbContext.Messages.Update(message);
            await this.applicationDbContext.SaveChangesAsync();

            return true;
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
