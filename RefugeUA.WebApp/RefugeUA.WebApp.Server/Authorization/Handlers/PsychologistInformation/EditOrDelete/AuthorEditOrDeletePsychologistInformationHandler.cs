using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.PsychologistInformation.EditOrDelete
{
    public class AuthorEditOrDeletePsychologistInformationHandler : AuthorizationHandler<EditOrDeletePsychologistInformationRequirement, PsychologistInformationResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeletePsychologistInformationRequirement requirement, PsychologistInformationResult resource)
        {
            if (context.User.GetId() == resource.AuthorId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
