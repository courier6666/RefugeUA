using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Participated
{
    public class PagedParticipatedEventsListQueryValidator : AbstractValidator<PagedParticipatedEventsListQuery>
    {
        public PagedParticipatedEventsListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
