using FluentValidation;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public class VolunteerEventScheduleItemDtoWithIdValidator : AbstractValidator<VolunteerEventScheduleItemDtoWithId>
    {
        public VolunteerEventScheduleItemDtoWithIdValidator()
        {
            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Час початку є обов’язковим.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Опис є обов’язковим.")
                .MaximumLength(400).WithMessage("Опис не може перевищувати 400 символів.");
        }
    }
}
