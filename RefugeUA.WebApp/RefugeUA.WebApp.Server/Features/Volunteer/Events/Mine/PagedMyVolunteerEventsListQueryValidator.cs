using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.PagedList
{
    public class PagedMyVolunteerEventsListQueryValidator : AbstractValidator<PagedMyVolunteerEventsListQuery>
    {
        public PagedMyVolunteerEventsListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);


        }
    }
}
