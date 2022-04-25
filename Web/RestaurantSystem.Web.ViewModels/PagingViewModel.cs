namespace RestaurantSystem.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; init; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ItemsCount / this.ItemsPerPage);

        public int ItemsCount { get; init; }

        public int ItemsPerPage { get; init; }
    }
}
