namespace RestaurantSystem.Web.Areas.Owner.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static RestaurantSystem.Common.GlobalConstants;

    [Area("Owner")]
    [Authorize(Roles = OwnerRoleName)]
    public class OwnerController : Controller
    {
    }
}
