using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Participants
{
    public class VolunteerEventParticipantsQueryValidator : AbstractValidator<VolunteerEventParticipantsQuery>
    {
        public VolunteerEventParticipantsQueryValidator(IValidator<IPagingInfoQuery> validator)
        {
            Include(validator);
        }
    }
}
