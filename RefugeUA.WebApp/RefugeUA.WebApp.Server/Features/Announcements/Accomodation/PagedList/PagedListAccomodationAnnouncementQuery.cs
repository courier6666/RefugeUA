using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.PagedList
{
    public class PagedListAccomodationAnnouncementQuery : ListAccomodationAnnouncementQuery, IPagingInfoQuery
    {
        public int Page { get; set; }

        public int PageLength { get; set; }
    }
}
