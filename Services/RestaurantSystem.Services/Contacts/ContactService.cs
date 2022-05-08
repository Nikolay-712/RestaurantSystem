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

        public ReadMessageViewModel ReadMessage(string messageId)
        {
            var message = this.GetMessages<ReadMessageViewModel>()
                .FirstOrDefault(x => x.Id == messageId);

            return message;
        }

        public async Task<bool> ReplyMessageAsync(ReadMessageViewModel replyInput, string sender)
        {
            var message = this.applicationDbContext
                .AppMessages
                .FirstOrDefault(x => x.Id == replyInput.Id);

            if (message != null)
            {
                var replyMessage = new MessageReply
                {
                    Text = replyInput.Text,
                    MessageId = message.Id,
                    Sender = sender,
                };

                await this.applicationDbContext.Replies.AddAsync(replyMessage);
                await this.applicationDbContext.SaveChangesAsync();

                await this.UpdateLastReplies(sender, message.Id);

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

        private async Task UpdateLastReplies(string sender, string messageId)
        {
            var replySender = sender is "Administration" ? "user" : "administration";

            var lastReplies = this.applicationDbContext.Replies
                .Where(x => x.MessageId == messageId)
                .OrderBy(x => x.CreatedOn)
                .Where(x => x.Sender.ToLower() == replySender)
                .Where(x => x.IsRead == false)
                .ToList();

            if (lastReplies.Count > 0)
            {
                lastReplies.ForEach(x => x.IsRead = true);

                this.applicationDbContext.Replies.UpdateRange(lastReplies);
                await this.applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
