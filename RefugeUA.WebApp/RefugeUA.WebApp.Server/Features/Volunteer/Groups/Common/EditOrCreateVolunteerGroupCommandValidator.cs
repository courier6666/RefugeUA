using FluentValidation;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common
{
    public class EditOrCreateVolunteerGroupCommandValidator : AbstractValidator<EditOrCreateVolunteerGroupCommand>
    {
        public EditOrCreateVolunteerGroupCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Назва є обов’язковою.")
                .MaximumLength(200).WithMessage("Назва не може перевищувати 200 символів.");

            RuleFor(c => c.DescriptionContent)
                .NotEmpty().WithMessage("Опис є обов’язковим.")
                .MaximumLength(4096).WithMessage("Опис не може перевищувати 4096 символів.");
        }
    }
}
