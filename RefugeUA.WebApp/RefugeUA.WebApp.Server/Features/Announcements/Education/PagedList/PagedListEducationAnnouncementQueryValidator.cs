using FluentValidation;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.PagedList
{
    public class PagedListEducationAnnouncementQueryValidator : AbstractValidator<PagedListEducationAnnouncementQuery>
    {
        public PagedListEducationAnnouncementQueryValidator(
            IValidator<ListEducationAnnouncementQuery> listEducationAnnounceQueryValidator,
            IValidator<IPagingInfoQuery> pagingInfoQueryValidator)
        {
            Include(listEducationAnnounceQueryValidator);
            Include(pagingInfoQueryValidator);
        }
    }
}
