using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    public class AnnouncementResponseDtoValidator : AbstractValidator<AnnouncementResponseDto>
    {
        public AnnouncementResponseDtoValidator(IValidator<ContactInformationDto> validator)
        {
            RuleFor(x => x.ContactInformation)
                .NotNull().WithMessage("Контактна інформація є обов'язковою.")
                .SetValidator(validator)
                .When(x => x.ContactInformation != null);
        }
    }
}
