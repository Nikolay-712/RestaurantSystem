namespace RestaurantSystem.Services.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Data.Models.Users;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Web.ViewModels.Addresses;

    using static RestaurantSystem.Common.GlobalConstants;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContactService contactService;

        public UserService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            IContactService contactService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.contactService = contactService;
        }

        public async Task<bool> АpproveUserAsync(string messageId, string appruve)
        {
            var message = this.contactService.GetMessageById(messageId);
            var validRoute = appruve == "true" || appruve == "false";

            if (message != null && validRoute)
            {
                var sender = Message.AdminSender;
                var text = appruve == "true" ? Message.АpproveOwnerMessage : Message.RefuseOwnerMessage;
                var messageStatus = appruve == "true" ? MessageStatus.Approved : MessageStatus.Canceled;

                await this.FinishedOwnerApplication(messageId, text, sender, message, messageStatus);
                return true;
            }

            return false;
        }

        public async Task SavePhoneNumberAsync(string userId, string phoneNumber)
        {
            var user = await this.GetUserByIdAsync(userId);

            user.PhoneNumber = "+359" + phoneNumber;

            this.applicationDbContext.Update(user);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task SaveAddressAsync(string userId, AddresInputModel addresInput)
        {
            var user = await this.GetUserByIdAsync(userId);
            var addres = this.GetUserAddress(userId);

            if (addres == null)
            {
                var newAddress = new Address
                {
                    UseId = userId,
                    Country = addresInput.Country,
                    Town = addresInput.Town,
                    ShippingAddress = addresInput.ShippingAddress,
                };

                await this.applicationDbContext.Addresses.AddAsync(addres);
            }
            else
            {
                addres.Town = addresInput.Town;
                addres.Country = addresInput.Country;
                addres.ShippingAddress = addresInput.ShippingAddress;

                this.applicationDbContext.Addresses.Update(addres);
            }

            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }

        public Address GetUserAddress(string userId)
        {
            var address = this.applicationDbContext
                .Addresses
                .FirstOrDefault(x => x.UseId == userId);

            return address;
        }

        private async Task FinishedOwnerApplication(
            string messageId, string text, string sender, AppMessage message, MessageStatus status)
        {
            await this.contactService.ReplyMessageAsync(messageId, text, sender);
            await this.contactService.CloseDiscussionAsync(message);
            await this.contactService.ChangeMessageStatusAsync(message, status);
        }
    }
}
