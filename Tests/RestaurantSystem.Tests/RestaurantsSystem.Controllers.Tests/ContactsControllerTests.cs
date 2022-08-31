using RestaurantSystem.Data;
using RestaurantSystem.Services.Contacts;
using RestaurantSystem.Services.Reservations;
using RestaurantSystem.Web.Controllers.Contacts;

using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using RestaurantSystem.Web.ViewModels.Contacts;
using RestaurantSystem.Data.Models.Contacts;
using Microsoft.AspNetCore.Http;
using RestaurantSystem.Data.Models.Users;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Tests.RestaurantsSystem.Controllers.Tests
{
    public class ContactsControllerTests
    {
        private ApplicationDbContext dbContext;
        private ContactsController contactController;

        public ContactsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationTestDb").Options;
            this.dbContext = new ApplicationDbContext(options);

            var reservationService = new Mock<IReservationService>();
            var contactService = new Mock<IContactService>();

            this.contactController = new ContactsController(contactService.Object, reservationService.Object);
        }

        [Fact]
        public async Task SendMessageWithInvalidModelState()
        {
            var testUser = await this.AddTestUserAsync();
            this.AddTestClaims(testUser.Email, testUser.Id);

            var mockTempData = new Mock<ITempDataDictionary>();
            this.contactController.TempData = mockTempData.Object;

            this.contactController.ModelState.AddModelError("emptyMessage", "some text");

            var result = await this.contactController
                .Index(new MessageInputVewModel 
                {
                    Message = "eeeeeeeeeeeee",
                    MessageType = MessageType.Саобщение,
                });

           var aaa= contactController.ViewData.ModelState.Count;

            Assert.True(!contactController.ModelState.IsValid, "This must be InValid");
        }


        private void AddTestClaims(string email, string userId)
        {
            var claims = new List<Claim>()
             {
                 new Claim(ClaimTypes.Name, email),
                 new Claim(ClaimTypes.NameIdentifier, userId),
             };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            this.contactController
                .ControllerContext
                .HttpContext = new DefaultHttpContext { User = user };
        }

        private async Task<ApplicationUser> AddTestUserAsync()
        {
            await this.dbContext.Users.AddAsync(new ApplicationUser
            {
                CreatedOn = DateTime.UtcNow,
                Email = "testUser@abv.bg",
                IsDeleted = false,
                PasswordHash = "123456"
            });

            await this.dbContext.SaveChangesAsync();
            return this.dbContext.Users.FirstOrDefault();
        }
    }
}

