using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.PagedList
{
    public class PagedArticlesListQueryValidator : AbstractValidator<PagedArticlesListQuery>
    {
        public PagedArticlesListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);   
        }
    }
}
