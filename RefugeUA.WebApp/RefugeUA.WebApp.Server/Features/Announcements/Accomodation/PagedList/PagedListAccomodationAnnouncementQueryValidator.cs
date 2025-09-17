using FluentValidation;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.PagedList
{
    public class PagedListAccomodationAnnouncementQueryValidator : AbstractValidator<PagedListAccomodationAnnouncementQuery>
    {
        public PagedListAccomodationAnnouncementQueryValidator(
            IValidator<ListAccomodationAnnouncementQuery> listAccomodationAnnounceQueryValidator,
            IValidator<IPagingInfoQuery> pagingInfoQueryValidator)
        {
            Include(listAccomodationAnnounceQueryValidator);
            Include(pagingInfoQueryValidator);
        }
    }
}
