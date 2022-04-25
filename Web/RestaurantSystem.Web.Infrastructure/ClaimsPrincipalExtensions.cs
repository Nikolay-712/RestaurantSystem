namespace RestaurantSystem.Web.Infrastructure
{
    using System.Linq;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.Claims.Count() == 0 ? null : user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
