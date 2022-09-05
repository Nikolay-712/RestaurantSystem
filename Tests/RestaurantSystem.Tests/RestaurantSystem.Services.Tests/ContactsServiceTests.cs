using RestaurantSystem.Data;
using RestaurantSystem.Data.Models.Contacts;
using RestaurantSystem.Services.Contacts;
using RestaurantSystem.Services.Notifications;
using RestaurantSystem.Web.ViewModels.Contacts;

using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

using static RestaurantSystem.Common.GlobalConstants;

namespace RestaurantSystem.Tests.RestaurantSystem.Services.Tests
{
    public class ContactsServiceTests
    {
        private ApplicationDbContext dbContext;
        private ContactService contactService;

        public ContactsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationTestDb").Options;

            this.dbContext = new ApplicationDbContext(options);
            var notificationService = new Mock<INotificationService>();
            this.contactService = new ContactService(this.dbContext, notificationService.Object);

            if (this.dbContext.AppMessages.Count() == 0)
            {
                this.AddMessagesToDbContext();
            }
        }

        [Fact]
        public async void GetAppMessagesCountShouldReturnCorrectNumber()
        {
            await this.contactService.SendMessageAsync(new MessageInputVewModel
            {
                Message = "test method message",
                MessageType = MessageType.Саобщение,
            }, "usserId");

            Assert.Equal(5, dbContext.AppMessages.Count());
        }

        [Fact]
        public void SendAppMessageShouldReturnValidMessage()
        {
            var message = this.dbContext.AppMessages.FirstOrDefault();

            Assert.NotNull(message.Id);
            Assert.NotNull(message.UserId);
            Assert.Equal(MessageStatus.Pending, message.Status);
        }

        [Theory]
        [InlineData(null, MessageType.Саобщение, "testUserId")]
        [InlineData("some text", MessageType.Саобщение, null)]
        [InlineData(null, MessageType.Кандидатсване, null)]
        public async void SendAppMessagesWithInvalidParametersShouldReturnDbUpdateException
            (string messageText, MessageType messageType, string userId)
        {
            await Assert.ThrowsAsync<DbUpdateException>(()
                  => this.contactService.SendMessageAsync(new
                  MessageInputVewModel
                  { Message = messageText, MessageType = messageType }, userId));
        }

        [Fact]
        public void GetMessageByIdShouldReturnNotNull()
        {
            var message = this.dbContext.AppMessages.FirstOrDefault();

            var targetMessages = this.contactService.GetMessageById(message.Id);

            Assert.NotNull(targetMessages);
        }

        [Fact]
        public void GetMessageByIdWithNotЕxistingIdShouldReturnNull()
        {
            var messages = this.contactService.GetMessageById("fakeMessageId");

            Assert.Null(messages);
        }

        [Fact]
        public async Task SendReplyMessageWithValidParamitersShouldReturnRepliesCount()
        {
            var message = this.dbContext.AppMessages.FirstOrDefault();

            var replyText = "new reply for this message";
            var sender = Message.AdminSender;

            await this.contactService.ReplyMessageAsync(message.Id, replyText, sender);
            await this.contactService.ReplyMessageAsync(message.Id, replyText, sender);

            Assert.Equal(2, message.Replies.Count());
        }

        [Theory]
        [InlineData("new reply for this message", null)]
        [InlineData(null, Message.AdminSender)]
        [InlineData(null, null)]
        public async Task SendReplyMessageWithInValidParamitersShouldReturnDbUpdateException
            (string messageText, string sender)
        {
            var message = this.dbContext.AppMessages.FirstOrDefault();

            await Assert.ThrowsAsync<DbUpdateException>(()
                  => this.contactService.ReplyMessageAsync(message.Id, messageText, sender));
        }

        [Fact]
        public async Task SendReplyMessageForNotЕxistingMessageShouldReturnFalse()
        {
            var replyText = "new reply for this message";
            var sender = Message.AdminSender;

            var result = await this.contactService
                .ReplyMessageAsync("fake messageId", replyText, sender);

            Assert.False(result);
        }

        [Theory]
        [InlineData(MessageStatus.Answered)]
        [InlineData(MessageStatus.Canceled)]
        [InlineData(MessageStatus.Approved)]
        public async Task ChangeMessageSatusShouldReturnCorectStatus(MessageStatus messageStatus)
        {
            var message = this.dbContext.AppMessages
                .FirstOrDefault(x => x.UserId == "testUserId5");

            Assert.NotNull(message);
            await this.contactService.ChangeMessageStatusAsync(message, messageStatus);

            Assert.Equal(messageStatus, message.Status);
        }

        [Fact]
        public async Task CloseDiscussionAndSuccessfullyChangeMessage()
        {
            var message = this.dbContext.AppMessages
                .FirstOrDefault(x => x.UserId == "testUserId4");

            Assert.NotNull(message);

            await this.contactService.CloseDiscussionAsync(message);

            Assert.True(message.IsOpen);
            Assert.Equal(MessageStatus.Answered, message.Status);
            Assert.True(message.Replies.Count() != 0);
        }

        private void AddMessagesToDbContext()
        {
            var appMessages = new List<AppMessage>()
            {
                new AppMessage
                {
                    MessageType = MessageType.Кандидатсване,
                    Message = "some text message 1",
                    UserId = "testUserId",
                    Status = MessageStatus.Pending,
                },
                new AppMessage
                {
                    MessageType = MessageType.Саобщение,
                    Message = "some text message 2",
                    UserId = "testUserId3",
                    Status = MessageStatus.Pending,
                },
                new AppMessage
                {
                    MessageType = MessageType.Кандидатсване,
                    Message = "some text message 3",
                    UserId = "testUserId4",
                    Status = MessageStatus.Pending,
                },
                new AppMessage
                {
                    MessageType = MessageType.Саобщение,
                    Message = "some text message 4",
                    UserId = "testUserId5",
                    Status = MessageStatus.Pending,
                },
            };

            this.dbContext.AppMessages.AddRange(appMessages);
            this.dbContext.SaveChanges();
        }
    }
}
