using FluentValidation;

namespace RefugeUA.WebApp.Server.Features.Authentication.SendEmailConfirmation
{
    public class SendEmailConfirmationCommandValidator : AbstractValidator<SendEmailConfirmationCommand>
    {
        public SendEmailConfirmationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
        }
    }
}
