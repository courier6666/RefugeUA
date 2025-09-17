using FluentValidation;

namespace RefugeUA.WebApp.Server.Shared.Dto.ContactInformation
{
    public class ContactInformationDtoValidator : AbstractValidator<ContactInformationDto>
    {
        public ContactInformationDtoValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону є обов’язковим.")
                .Matches(@"^\+?[1-9]\d{10,14}$").WithMessage("Номер телефону має бути у дійсному міжнародному форматі.");

            RuleFor(x => x.Email)
                .MaximumLength(128).WithMessage("Довжина email не може перевищувати 128 символів.").When(x => x.Email != null)
                .EmailAddress().WithMessage("Неправильний формат email.").When(x => x.Email != null);

            RuleFor(x => x.Telegram)
                .MaximumLength(256).WithMessage("Посилання на Telegram не може перевищувати 256 символів.").When(x => x.Telegram != null);

            RuleFor(x => x.Viber)
                .MaximumLength(256).WithMessage("Посилання на Viber не може перевищувати 256 символів.").When(x => x.Viber != null);

            RuleFor(x => x.Facebook)
                .MaximumLength(256).WithMessage("Посилання на Facebook не може перевищувати 256 символів.").When(x => x.Facebook != null);
        }
    }
}
