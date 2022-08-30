using RestaurantSystem.Data;
using RestaurantSystem.Services.Contacts;
using RestaurantSystem.Services.Notifications;
using RestaurantSystem.Services.Reservations;
using RestaurantSystem.Web.Controllers.Contacts;

using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using RestaurantSystem.Web.ViewModels.Contacts;
using RestaurantSystem.Data.Models.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Net;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace RestaurantSystem.Tests.RestaurantsSystem.Controllers.Tests
{
    public class ContactsControllerTests
    {
        private ApplicationDbContext dbContext;
        private ContactService contactService;

        public ContactsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationTestDb").Options;

            this.dbContext = new ApplicationDbContext(options);
            var notificationService = new Mock<INotificationService>();
            this.contactService = new ContactService(this.dbContext, notificationService.Object);
        }

        [Fact]
        public void ControllerTest()
        {
            var reservationService = new Mock<IReservationService>();
            var controller = new ContactsController(this.contactService, reservationService.Object);
           
            controller.Index();

        }
    }
}
