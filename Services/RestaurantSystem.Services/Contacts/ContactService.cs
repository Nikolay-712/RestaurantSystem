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
        private const int MessagesPerPage = 15;
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

        public AllMessagesViewModel AllMessages(int page)
        {
            var messagesList = this.GetMessages<MessageViewModel>()
                .OrderByDescending(x => x.CreatedOn).ThenBy(x => x.IsOpen);

            var messages = new AllMessagesViewModel
            {
                ItemsPerPage = MessagesPerPage,
                ItemsCount = messagesList.Count(),
                PageNumber = page,
                Messages = messagesList
                    .Skip((page - 1) * MessagesPerPage)
                    .Take(MessagesPerPage),
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

        public AppMessage GetMessageById(string messageId)
        {
            return this.applicationDbContext
                .AppMessages.
                FirstOrDefault(x => x.Id == messageId);
        }

        public async Task ChangeMessageStatusAsync(AppMessage message, MessageStatus status)
        {
            message.Status = status;

            this.applicationDbContext.AppMessages.Update(message);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task CloseDiscussionAsync(AppMessage message)
        {
            message.IsOpen = true;

            var sender = "Administration";
            var reply = new ReadMessageViewModel()
            {
                Id = message.Id,
                Text = "Дискусията е затворена,надявам се да сме били полезни",
            };

            await this.ReplyMessageAsync(reply, sender);

            this.applicationDbContext.AppMessages.Update(message);
            await this.applicationDbContext.SaveChangesAsync();
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
