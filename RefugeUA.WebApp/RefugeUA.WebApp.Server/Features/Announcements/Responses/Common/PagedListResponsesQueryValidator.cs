using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Responses.Common
{
    public class PagedListResponsesQueryValidator : AbstractValidator<PagedListResponsesQuery>
    {
        public PagedListResponsesQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
