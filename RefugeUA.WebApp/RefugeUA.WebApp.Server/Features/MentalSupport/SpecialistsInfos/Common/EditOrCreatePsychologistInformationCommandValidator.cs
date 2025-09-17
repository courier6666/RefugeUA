using FluentValidation;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common
{
    public class EditOrCreatePsychologistInformationCommandValidator : AbstractValidator<EditOrCreatePsychologistInformationCommand>
    {
        public EditOrCreatePsychologistInformationCommandValidator(IValidator<ContactInformationDto> validatorContact)
        {
            RuleFor(x => x.Title).
                NotEmpty().WithMessage("Заголовок інформації психолога не може бути порожнім.").
                MaximumLength(200).WithMessage("Заголовок не може перевищувати 200 символів.");

            RuleFor(x => x.Description).
                NotEmpty().WithMessage("Опис інформації психолога не може бути порожнім.").
                MaximumLength(1024).WithMessage("Опис не може перевищувати 1024 символи.");

            RuleFor(x => x.ContactInformation).
                Cascade(CascadeMode.Stop).
                SetValidator(validatorContact);
        }
    }
}
