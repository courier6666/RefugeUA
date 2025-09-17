using FluentValidation;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common
{
    public class EditOrCreateMentalSupportArticleCommandValidator : AbstractValidator<EditOrCreateMentalSupportArticleCommand>
    {
        public EditOrCreateMentalSupportArticleCommandValidator()
        {
            RuleFor(x => x.Title).
                NotEmpty().WithMessage("Заголовок статті не може бути порожнім.").
                MaximumLength(200).WithMessage("Заголовок не може перевищувати 200 символів.");

            RuleFor(x => x.Content).
                NotEmpty().WithMessage("Зміст статті не може бути порожнім.").
                MaximumLength(8192).WithMessage("Зміст не може перевищувати 8192 символів.");
        }
    }
}
