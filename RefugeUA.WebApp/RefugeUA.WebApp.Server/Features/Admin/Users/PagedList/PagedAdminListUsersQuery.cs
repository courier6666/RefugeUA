using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Admin.Users.PagedList
{
    public class PagedAdminListUsersQuery : IPagingInfoQuery
    {
        public string? Prompt { get; set; }

        public int Page { get; set; }
        
        public int PageLength { get; set; }
    }
}
