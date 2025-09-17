using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Followers
{
    public class PagedFollowersOfGroupListQuery : IPagingInfoQuery
    {
        public string? Prompt { get; set; }

        public int Page { get; set; }

        public int PageLength { get; set; }
    }
}
