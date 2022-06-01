using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Ratings
{
    public interface IRatingService
    {
        Task AddRateAsync(string id, int stars, string userId, string controllerName);
    }
}
