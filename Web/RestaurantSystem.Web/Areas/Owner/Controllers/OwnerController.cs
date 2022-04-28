namespace RestaurantSystem.Web.Areas.Owner.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static RestaurantSystem.Common.GlobalConstants;

    [Authorize(Roles = OwnerRoleName)]
    [Area("Owner")]
    public class OwnerController : Controller
    {
    }
}
