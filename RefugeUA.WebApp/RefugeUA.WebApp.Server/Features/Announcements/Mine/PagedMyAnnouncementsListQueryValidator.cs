using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Mine
{
    public class PagedMyAnnouncementsListQueryValidator : AbstractValidator<PagedMyAnnouncementsListQuery>
    {
        public PagedMyAnnouncementsListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
