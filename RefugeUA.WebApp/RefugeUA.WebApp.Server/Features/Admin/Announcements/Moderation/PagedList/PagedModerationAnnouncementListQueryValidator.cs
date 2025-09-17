using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Admin.Announcements.Moderation.PagedList
{
    public class PagedModerationAnnouncementListQueryValidator : AbstractValidator<PagedModerationAnnouncementListQuery>
    {
        public PagedModerationAnnouncementListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
