namespace RestaurantSystem.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Users;
    using RestaurantSystem.Services.Mapping;

    using static RestaurantSystem.Common.GlobalConstants;

    public class OwnerViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public IEnumerable<string> Roles { get; init; }

        public IEnumerable<RestaurantInfoViewModel> MyResaturants { get; init; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Message { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, OwnerViewModel>()
               .ForMember(x => x.Roles, opt =>
                   opt.MapFrom(x => x.Roles.Select(x => x.RoleId)))
               .ForMember(x => x.MyResaturants, opt =>
                    opt.MapFrom(x => x.Restaurants));
        }
    }
}
