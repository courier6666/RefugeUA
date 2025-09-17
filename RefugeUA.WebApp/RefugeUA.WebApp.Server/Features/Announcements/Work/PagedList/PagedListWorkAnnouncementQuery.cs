using Microsoft.AspNetCore.Mvc;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.PagedList
{
    public class PagedListEducationAnnouncementQuery : ListEducationAnnouncementQuery, IPagingInfoQuery
    {
        public int Page { get; set; } = 1;

        public int PageLength { get; set; }
    }
}
