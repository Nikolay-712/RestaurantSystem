namespace RestaurantSystem.Web.ViewModels.Restaurants
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Comments;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Ratings;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public DateTime CreatedOn { get; init; }

        public string Text { get; init; }

        public string UserId { get; init; }

        public string RestaurantId { get; init; }

        public IEnumerable<RatingViewModel> Ratings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                 .ForMember(x => x.Ratings, opt =>
                     opt.MapFrom(x => x.User.Ratings
                     .Where(x => x.RestaurantId != null)))
                 .ForMember(x => x.Email, opt =>
                    opt.MapFrom(x => x.User.Email));
        }
    }
}
