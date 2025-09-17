using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.PagedList
{
    public class PagedVolunteerEventsListQueryValidator : AbstractValidator<PagedVolunteerEventsListQuery>
    {
        public PagedVolunteerEventsListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);


        }
    }
}
