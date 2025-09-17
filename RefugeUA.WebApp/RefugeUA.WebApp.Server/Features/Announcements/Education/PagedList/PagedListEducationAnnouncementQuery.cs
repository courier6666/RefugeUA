using Microsoft.AspNetCore.Mvc;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.PagedList
{
    public class PagedListEducationAnnouncementQuery : ListWorkAnnouncementQuery, IPagingInfoQuery
    {
        public int Page { get; set; } = 1;

        public int PageLength { get; set; }
    }
}
