using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Requierements;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.PsychologistInformation.EditOrDelete
{
    public class AdminEditOrDeletePsychologistInformationHandler : AuthorizationHandler<EditOrDeletePsychologistInformationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeletePsychologistInformationRequirement requirement)
        {
            return AuthorizeAdminHandlerMethod.Authorize(context, requirement);
        }
    }
}
