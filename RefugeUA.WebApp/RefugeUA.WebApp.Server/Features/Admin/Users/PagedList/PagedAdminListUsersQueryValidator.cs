using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Admin.Users.PagedList
{
    public class PagedAdminListUsersQueryValidator : AbstractValidator<PagedAdminListUsersQuery>
    {
        public PagedAdminListUsersQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
