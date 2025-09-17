using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.PagedList
{
    public class PagedPsychologistInformationsListQuery : IPagingInfoQuery
    {
        public string? Prompt { get; set; }

        public int Page { get ; set ; }

        public int PageLength { get ; set ; }
    }
}
