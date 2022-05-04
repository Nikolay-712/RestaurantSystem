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

        public ContactService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task SendMessageAsync(MessageInputVewModel messageInput, string userId)
        {
            var message = new AppMessage
            {
                MessageType = messageInput.MessageType,
                Message = messageInput.Message,
                UserId = userId,
                Status = MessageStatus.Pending,
            };

            await this.applicationDbContext.AppMessages.AddAsync(message);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public AllMessagesViewModel AllMessages()
        {
            var messagesList = this.GetMessages<MessageViewModel>()
                .OrderByDescending(x => x.CreatedOn).ToList();

            var messages = new AllMessagesViewModel
            {
                Messages = messagesList,
                UnreadMessagesCount = messagesList
                    .Where(x => x.Status == "Pending")
                    .Count(),
            };

            return messages;
        }

        public async Task<bool> ReplyMessage(AllMessagesViewModel replyInput, string messageId)
        {
            var message = this.applicationDbContext
                .AppMessages
                .FirstOrDefault(x => x.Id == messageId);

            if (message != null)
            {
                var replyMessage = new MessageReply
                {
                    Text = replyInput.Reply,
                    MessageId = message.Id,
                };

                await this.applicationDbContext.Replies.AddAsync(replyMessage);

                message.Status = MessageStatus.Answered;

                this.applicationDbContext.AppMessages.Update(message);
                await this.applicationDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public IEnumerable<T> GetMessages<T>()
        {
            var messages = this.applicationDbContext
                .AppMessages
                .To<T>();

            return messages;
        }
    }
}
