using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.PagedList
{
    public class PagedPsychologistInformationsListQueryValidator : AbstractValidator<PagedPsychologistInformationsListQuery>
    {
        public PagedPsychologistInformationsListQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
