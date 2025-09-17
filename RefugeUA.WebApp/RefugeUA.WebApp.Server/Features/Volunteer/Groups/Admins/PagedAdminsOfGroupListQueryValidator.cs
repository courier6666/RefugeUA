using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Admins
{
    public class PagedAdminsOfGroupListQueryValidator : AbstractValidator<PagedAdminsOfGroupListQuery>
    {
        public PagedAdminsOfGroupListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
