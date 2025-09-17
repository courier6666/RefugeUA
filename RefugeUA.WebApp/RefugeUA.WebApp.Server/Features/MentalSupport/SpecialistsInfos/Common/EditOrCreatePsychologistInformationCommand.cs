using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common
{
    public class EditOrCreatePsychologistInformationCommand
    {
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public ContactInformationDto ContactInformation { get; set; } = default!;
    }
}
