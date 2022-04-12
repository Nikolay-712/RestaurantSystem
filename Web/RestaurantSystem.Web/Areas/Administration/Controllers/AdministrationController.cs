namespace RestaurantSystem.Web.Areas.Administration.Controllers
{
    using RestaurantSystem.Common;
    using RestaurantSystem.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
