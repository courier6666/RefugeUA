using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Shared.Dto.Address;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public class EditCreateVolunteerEventCommandValidator : AbstractValidator<EditCreateVolunteerEventCommand>
    {
        public EditCreateVolunteerEventCommandValidator(RefugeUADbContext dbContext, IHttpContextAccessor httpContextAccessor,
            IValidator<VolunteerEventScheduleItemDtoWithId> validatorScheduleItems,
            IValidator<AddressDto> validatorAddress)
        {
            var user = httpContextAccessor.HttpContext!.User;

            RuleFor(x => x.VolunteerGroupId).
                Must(x => dbContext.VolunteerGroups.Any(g => g.Id == x)).
                When(x => x.VolunteerGroupId != null).WithMessage("Група з таким ідентифікатором має існувати.");

            if (!user.IsInRole(Roles.Admin) && !user.IsInRole(Roles.CommunityAdmin))
            {
                var userId = user?.GetId() ?? 0;
                RuleFor(x => x.VolunteerGroupId)
                    .MustAsync(async (groupId, cancellation) =>
                        await dbContext.VolunteerGroups
                            .Where(g => g.Id == groupId)
                            .AnyAsync(g =>
                                g.Followers.Any(f => f.Id == userId) ||
                                g.Admins.Any(a => a.Id == userId), cancellation))
                    .When(x => x.VolunteerGroupId != null)
                    .WithMessage("Користувач не є адміністратором або учасником волонтерської групи.");
            }

            RuleForEach(x => x.ScheduleItems).
                Cascade(CascadeMode.Stop).
                NotNull().WithMessage("Елемент розкладу не може бути пустим.").
                Must((command, item) =>
                {
                    return item.StartTime >= command.StartTime && item.StartTime <= command.EndTime;
                }).WithMessage("Час початку елемента розкладу має бути між початком та кінцем події.").
                SetValidator(validatorScheduleItems);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Заголовок є обов’язковим.")
                .MaximumLength(200).WithMessage("Заголовок не може перевищувати 200 символів.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Вміст є обов’язковим.")
                .MaximumLength(4096).WithMessage("Вміст не може перевищувати 4096 символів.");

            RuleFor(x => x.StartTime)
                .NotNull().WithMessage("Час початку є обов’язковим.");

            RuleFor(x => x.EndTime)
                .NotNull().WithMessage("Час завершення є обов’язковим.");

            RuleFor(x => x.OnlineConferenceLink)
                .MaximumLength(500).WithMessage("Посилання на онлайн-конференцію не може перевищувати 500 символів.")
                .When(x => !string.IsNullOrWhiteSpace(x.OnlineConferenceLink));

            RuleFor(x => x.DonationLink)
                .MaximumLength(500).WithMessage("Посилання на пожертву не може перевищувати 500 символів.")
                .When(x => !string.IsNullOrWhiteSpace(x.DonationLink));

            RuleFor(x => x.Address).
                Cascade(CascadeMode.Stop).
                SetValidator(validatorAddress!).When(x => x.Address != null);

            RuleFor(x => x)
                .Must(x => !x.StartTime.HasValue || x.StartTime <= x.EndTime)
                .WithMessage("Час початку має бути раніше або дорівнювати часу завершення.")
                .When(x => x.StartTime.HasValue && x.EndTime.HasValue);
        }
    }
}
