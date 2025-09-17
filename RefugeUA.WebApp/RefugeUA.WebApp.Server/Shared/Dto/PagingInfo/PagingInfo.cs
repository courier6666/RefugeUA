using System.Text.Json.Serialization;

namespace RefugeUA.WebApp.Server.Shared.Dto.PagingInfo
{
    /// <summary>
    /// Represents information about a paged collection of items.
    /// </summary>
    /// <typeparam name="TModel">The type of items in the paged collection.</typeparam>
    public class PagingInfo<TModel>
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingInfo{TModel}"/> class with paged items, total count, current page, and page length.
        /// </summary>
        /// <param name="pagedItems">The collection of items on the current page.</param>
        /// <param name="totalCount">The total count of items available.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="pageLength">The number of items per page.</param>
        public PagingInfo(IEnumerable<TModel> pagedItems, int totalCount, int page, int pageLength)
        {
            Items = pagedItems.ToList();
            TotalCount = totalCount;
            Page = page;
            PageLength = pageLength;
            PagesCount = (int)Math.Ceiling((float)TotalCount / PageLength);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingInfo{TModel}"/> class using JSON constructor.
        /// <remarks>This constructor is used for JSON deserialization and should not be invoked directly.</remarks>
        /// </summary>
        [JsonConstructor]
        public PagingInfo()
        {
        }

        /// <summary>
        /// Gets the collection of items on the current page.
        /// </summary>
        public ICollection<TModel> Items { get; init; } = default!;

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageLength { get; set; }

        /// <summary>
        /// Gets or sets the total number of items available.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages available.
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => Page > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool HasNextPage => Page < PagesCount;
    }
}
