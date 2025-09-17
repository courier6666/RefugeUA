using FluentValidation;

namespace RefugeUA.WebApp.Server.Shared.Dto.Address
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Країна є обов’язковою.")
                .MaximumLength(100)
                .WithMessage("Країна не може перевищувати 100 символів.");

            RuleFor(x => x.Region)
                .NotEmpty()
                .WithMessage("Регіон є обов’язковим.")
                .MaximumLength(100)
                .WithMessage("Регіон не може перевищувати 100 символів.");

            RuleFor(x => x.District)
                .NotEmpty()
                .WithMessage("Район є обов’язковим.")
                .MaximumLength(100)
                .WithMessage("Район не може перевищувати 100 символів.");

            RuleFor(x => x.Settlement)
                .NotEmpty()
                .WithMessage("Населений пункт є обов’язковим.")
                .MaximumLength(100)
                .WithMessage("Населений пункт не може перевищувати 100 символів.");

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("Вулиця є обов’язковою.")
                .MaximumLength(100)
                .WithMessage("Вулиця не може перевищувати 100 символів.");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("Поштовий індекс є обов’язковим.")
                .Matches(@"^\d{5}$")
                .WithMessage("Поштовий індекс має містити 5 цифр.");
        }
    }
}
