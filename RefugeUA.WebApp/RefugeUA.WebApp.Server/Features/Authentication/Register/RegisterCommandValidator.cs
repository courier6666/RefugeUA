using FluentValidation;
using Microsoft.AspNetCore.Identity;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;

namespace RefugeUA.WebApp.Server.Features.Authentication.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ім'я є обов'язковим.")
                .MaximumLength(100).WithMessage("Ім'я не може бути довшим за 100 символів.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Прізвище є обов'язковим.")
                .MaximumLength(100).WithMessage("Прізвище не може бути довшим за 100 символів.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Електронна пошта є обов'язковою.")
                .EmailAddress().WithMessage("Невірний формат електронної пошти.")
                .MaximumLength(128).WithMessage("Електронна пошта не може бути довшою за 128 символів.")
                .MustAsync(async (email, cancellation) =>
                {
                    var user = await userManager.FindByEmailAsync(email);
                    return user == null;
                }).WithMessage("Ця електронна пошта вже використовується.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону є обов'язковим.")
                .Matches(@"^\+?[1-9]\d{10,14}$").WithMessage("Номер телефону має бути у міжнародному форматі.")
                .Must(x => userManager.Users.FirstOrDefault(u => u.PhoneNumber == x) == null).WithMessage("Користувач з таким номером телефону вже існує.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата народження є обов'язковою.")
                .LessThan(DateTime.Now).WithMessage("Дата народження повинна бути в минулому.")
                .LessThan(DateTime.Now.AddYears(-16)).WithMessage("Користувачу має бути щонайменше 16 років.")
                .GreaterThan(new DateTime(1920, 1, 1)).WithMessage("Дата народження не може бути раніше 1920-01-01.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль є обов'язковим.")
                .MinimumLength(8).WithMessage("Пароль має містити щонайменше 8 символів.")
                .MaximumLength(16).WithMessage("Пароль не може перевищувати 16 символів.")
                .Matches(@"[A-Z]").WithMessage("Пароль має містити хоча б одну велику літеру.")
                .Matches(@"[a-z]").WithMessage("Пароль має містити хоча б одну малу літеру.")
                .Matches(@"[0-9]").WithMessage("Пароль має містити хоча б одну цифру.")
                .Matches(@"[\!\?\*\._]").WithMessage("Пароль має містити хоча б один спеціальний символ (!?*._).");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Підтвердження пароля є обов'язковим.")
                .Equal(x => x.Password).WithMessage("Паролі не співпадають.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Роль користувача є обов'язковою.")
                .MustAsync(async (role, cancellation) =>
                {
                    if (role == Roles.Admin)
                    {
                        return false;
                    }
                    var roleExists = await roleManager.RoleExistsAsync(role);
                    return roleExists;
                }).WithMessage("Такої ролі не існує.");

            RuleFor(x => x.District)
                .Must(x => !string.IsNullOrWhiteSpace(x)).When(x => x.Role == Roles.CommunityAdmin).WithMessage("Громада є обов'язковою для ролі адміністратора громади.")
                .MaximumLength(100).WithMessage("Назва громади не може бути довшою за 100 символів.");

        }
    }
}
