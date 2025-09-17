using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.MentalSupportArticle.EditOrDelete
{
    public class AdminEditOrDeleteArticleHandler : AuthorizationHandler<EditOrDeleteMentalSupportArticleRequirement, MentalSupportArticleResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteMentalSupportArticleRequirement requirement, MentalSupportArticleResult resource)
        {
            return AuthorizeAdminHandlerMethod.Authorize(context, requirement);
        }
    }
}
