using RefugeUA.Entities.Interfaces;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common
{
    public class PsychologistInformationResult
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public long AuthorId { get; set; }

        public UserDtoWithId Author { get; set; } = default!;

        public ContactInformationDtoWithId Contact { get; set; } = default!;
    }
}
