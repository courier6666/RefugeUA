using FluentValidation;
using RefugeUA.DatabaseAccess;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Common
{
    public class CreateAnnouncementGroupCommandValidator : AbstractValidator<CreateAnnouncementGroupCommand>
    {
        public CreateAnnouncementGroupCommandValidator(RefugeUADbContext dbContext)
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Назва групи не повинна перевищувати 100 символів.");

            RuleFor(x => x.Name)
                .Must(x => !dbContext.AnnouncementGroups.Any(g => g.Name == x))
                .WithMessage("Група оголошень з такою назвою вже існує.");
        }
    }
}
