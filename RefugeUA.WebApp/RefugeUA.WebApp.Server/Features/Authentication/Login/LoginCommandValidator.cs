using FluentValidation;

namespace RefugeUA.WebApp.Server.Features.Authentication.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Електронна пошта є обов'язковою.")
                .EmailAddress()
                .WithMessage("Невірний формат електронної пошти.");

        }
    }
}
