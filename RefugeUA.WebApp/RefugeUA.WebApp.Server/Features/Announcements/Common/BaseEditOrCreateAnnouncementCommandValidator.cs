using FluentValidation;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    public class BaseEditOrCreateAnnouncementCommandValidator : AbstractValidator<BaseEditOrCreateAnnouncementCommand>
    {
        public BaseEditOrCreateAnnouncementCommandValidator(RefugeUADbContext refugeUADbContext,
            IValidator<ContactInformationDto> validatorContactInfoDto,
            IValidator<AddressDto> validatorAddressDto)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Заголовок є обов'язковим.")
                .MaximumLength(200)
                .WithMessage("Заголовок не повинен перевищувати 200 символів.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Опис є обов'язковим.")
                .MaximumLength(4096)
                .WithMessage("Опис не повинен перевищувати 4096 символів.");

            RuleFor(x => x.ContactInformation)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Контактна інформація є обов'язковою.")
                .SetValidator(validatorContactInfoDto);

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Адреса є обов'язковою.")
                .SetValidator(validatorAddressDto);
        }
    }
}
