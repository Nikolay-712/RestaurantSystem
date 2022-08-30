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
        }

        [Fact]
        public async void GetAppMessagesCountShouldReturnCorrectNumber()
        {
            await this.contactService.SendMessageAsync(new MessageInputVewModel
            {
                Message = "test method message",
                MessageType = MessageType.Саобщение,
            }, "usserId");

            await this.contactService.SendMessageAsync(new MessageInputVewModel
            {
                Message = "test method message",
                MessageType = MessageType.Кандидатсване,
            }, "usserId1");

            await this.contactService.SendMessageAsync(new MessageInputVewModel
            {
                Message = "test method message",
                MessageType = MessageType.Кандидатсване,
            }, "usserId2");

            Assert.Equal(3, dbContext.AppMessages.Count());
        }

        [Fact]
        public async Task SendAppMessageShouldReturnValidMessage()
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
            var notificationService = new Mock<INotificationService>();
            var service = new ContactService(this.dbContext, notificationService.Object);

            await Assert.ThrowsAsync<DbUpdateException>(()
                  => service.SendMessageAsync(new
                  MessageInputVewModel
                  { Message = messageText, MessageType = messageType }, userId));
        }

        [Fact]
        public async Task GetMessageByIdShouldReturnNotNull()
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
    }
}
