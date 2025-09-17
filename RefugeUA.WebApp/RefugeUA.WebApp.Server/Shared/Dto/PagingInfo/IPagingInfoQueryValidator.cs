using FluentValidation;

namespace RefugeUA.WebApp.Server.Shared.Dto.PagingInfo
{
    public class PagingInfoQueryValidator : AbstractValidator<IPagingInfoQuery>
    {
        public PagingInfoQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Номер сторінки має бути більшим за 0.");

            RuleFor(x => x.PageLength)
                .GreaterThan(0)
                .WithMessage("Розмір сторінки має бути більшим за 0.");
        }
    }
}
