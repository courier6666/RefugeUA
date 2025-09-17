using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Followers
{
    public class PagedFollowersOfGroupListQueryValidator : AbstractValidator<PagedFollowersOfGroupListQuery>
    {
        public PagedFollowersOfGroupListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
