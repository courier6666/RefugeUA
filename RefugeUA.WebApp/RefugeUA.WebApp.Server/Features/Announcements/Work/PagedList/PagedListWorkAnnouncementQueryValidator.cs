using FluentValidation;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.PagedList
{
    public class PagedListWorkAnnouncementQueryValidator : AbstractValidator<PagedListEducationAnnouncementQuery>
    {
        public PagedListWorkAnnouncementQueryValidator(
            IValidator<ListWorkAnnouncementQuery> listWorkAnnounceQueryValidator,
            IValidator<IPagingInfoQuery> pagingInfoQueryValidator)
        {
            Include(listWorkAnnounceQueryValidator);
            Include(pagingInfoQueryValidator);
        }
    }
}
