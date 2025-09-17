using FluentValidation;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Common
{
    public class EditOrCreateWorkAnnouncementCommandValidator : AbstractValidator<EditOrCreateWorkAnnouncementCommand>
    {
        public EditOrCreateWorkAnnouncementCommandValidator(
            RefugeUADbContext refugeUADbContext,
            IValidator<BaseEditOrCreateAnnouncementCommand> validatorBaseAnnouncementCommand)
        {
            Include(validatorBaseAnnouncementCommand);

            RuleFor(x => x.WorkCategoryId).MustAsync(
                async (id, cancellation) =>
                {
                    var workCategory = await refugeUADbContext.WorkCategories.FindAsync(id);
                    return workCategory != null;
                })
                .WithMessage("Категорія роботи не існує.");

            RuleFor(x => x.JobPosition)
                .NotEmpty()
                .WithMessage("Назва посади є обов’язковою.")
                .MaximumLength(200)
                .WithMessage("Назва посади не повинна перевищувати 100 символів.");

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Назва компанії є обов’язковою.")
                .MaximumLength(400)
                .WithMessage("Назва компанії не повинна перевищувати 100 символів.");

            RuleFor(x => x.SalaryLower)
                .GreaterThan(8000).When(x => x.SalaryLower.HasValue)
                .WithMessage("Нижня межа зарплати повинна бути більшою за 8000.")
                .LessThan(500000).When(x => x.SalaryLower.HasValue)
                .WithMessage("Нижня межа зарплати повинна бути меншою за 500 000.")
                .LessThan(x => x.SalaryUpper)
                    .When(x => x.SalaryUpper.HasValue)
                    .WithMessage("Нижня межа зарплати повинна бути меншою за верхню межу зарплати.");

            RuleFor(x => x.SalaryUpper)
                .GreaterThan(8000).When(x => x.SalaryLower.HasValue)
                .WithMessage("Нижня межа зарплати повинна бути більшою за 8000.")
                .LessThan(500000).When(x => x.SalaryLower.HasValue)
                .WithMessage("Нижня межа зарплати повинна бути меншою за 500 000.")
                .WithMessage("Верхня межа зарплати повинна бути більшою за 0.")
                .GreaterThan(x => x.SalaryLower)
                    .When(x => x.SalaryLower.HasValue)
                    .WithMessage("Верхня межа зарплати повинна бути більшою за нижню межу зарплати.");

            RuleFor(x => x.RequirementsContent)
                .NotEmpty().WithMessage("Опис вимог є обов’язковим.")
                .MaximumLength(1024).WithMessage("Опис вимог не повинен перевищувати 1024 символи.");
        }
    }
}
