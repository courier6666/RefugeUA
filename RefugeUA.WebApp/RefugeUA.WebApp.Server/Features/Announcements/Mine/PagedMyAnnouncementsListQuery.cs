using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Mine
{
    public class PagedMyAnnouncementsListQuery : IPagingInfoQuery
    {
        public string? Prompt { get; set; }

        public int Page { get; set; }

        public int PageLength { get; set; }
    }
}
