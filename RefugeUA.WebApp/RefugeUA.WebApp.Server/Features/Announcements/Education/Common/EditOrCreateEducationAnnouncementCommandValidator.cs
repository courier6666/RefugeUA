using FluentValidation;
using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Common
{
    public class EditOrCreateEducationAnnouncementCommandValidator : AbstractValidator<EditOrCreateEducationAnnouncementCommand>
    {
        public EditOrCreateEducationAnnouncementCommandValidator(IValidator<BaseEditOrCreateAnnouncementCommand> baseValidator)
        {
            Include(baseValidator);
            RuleFor(x => x.EducationType)
                .NotEmpty()
                .WithMessage("Тип освіти є обов'язковим.")
                .MaximumLength(100)
                .WithMessage("Тип освіти не повинен перевищувати 100 символів.");

            RuleFor(x => x.InstitutionName)
                .NotEmpty()
                .WithMessage("Назва закладу є обов'язковою.")
                .MaximumLength(200)
                .WithMessage("Назва закладу не повинна перевищувати 200 символів.");

            RuleFor(x => x.Fee)
                .Must(fee => fee == null || fee >= 0)
                .WithMessage("Вартість має бути додатним числом або null.");

            RuleFor(x => x.TargetGroups).NotNull()
                .WithMessage("Цільова група є обов'язковою.")
                .Must(targetGroups => targetGroups.Length > 0)
                .WithMessage("Має бути вказана хоча б одна цільова група.");

            RuleForEach(x => x.TargetGroups)
                .NotEmpty()
                .WithMessage("Цільова група є обов'язковою.")
                .MaximumLength(100)
                .WithMessage("Цільова група не повинна перевищувати 100 символів.");

            RuleFor(x => x.Language)
                .NotEmpty()
                .WithMessage("Мова є обов'язковою.")
                .MaximumLength(50)
                .WithMessage("Мова не повинна перевищувати 50 символів.");

            RuleFor(x => x.Duration)
                .NotEmpty()
                .WithMessage("Тривалість є обов'язковою.")
                .Must(duration => duration >= 0)
                .WithMessage("Тривалість має бути додатним числом.")
                .Must(duration => duration <= 2191)
                .WithMessage("Тривалість не повинна перевищувати шість років.");
        }
    }
}
