using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common
{
    public class PagedVolunteerGroupsListQueryValidator : AbstractValidator<PagedVolunteerGroupsListQuery>
    {
        public PagedVolunteerGroupsListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
