using FluentValidation;
using RefugeUA.WebApp.Server.Features;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Create;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Create;
using RefugeUA.WebApp.Server.Features.Authentication.Login;
using RefugeUA.WebApp.Server.Features.Authentication.Register;
using RefugeUA.WebApp.Server.Features.Authentication.SendEmailConfirmation;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Extensions
{
    public static class AddValidatorsExtension
    {
        public static void AddValidators(this IServiceCollection services)
        {
            var assembly = typeof(AddValidatorsExtension).Assembly;

            var abstractValidatorType = typeof(AbstractValidator<>);
            var validatorTypes = assembly.GetTypes().
                Where(t => !t.IsAbstract &&
                t.IsClass &&
                (t.BaseType?.IsGenericType ?? false) &&
                t.BaseType.GetGenericTypeDefinition() == abstractValidatorType);

            foreach(var validatorType in validatorTypes)
            {
                var validatorGenericArgument = validatorType.BaseType!.GetGenericArguments()[0];
                var validatorInterface = typeof(IValidator<>).MakeGenericType(validatorGenericArgument);
                services.AddScoped(validatorInterface, validatorType);
            }
        }
    }
}
