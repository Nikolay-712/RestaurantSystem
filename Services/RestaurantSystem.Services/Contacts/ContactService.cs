namespace RestaurantSystem.Services.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Web.ViewModels.Contacts;

    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ContactService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
    }
}
