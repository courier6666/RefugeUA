using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.MentalSupportArticle.EditOrDelete
{
    public class AuthorEditOrDeleteArticleHandler : AuthorizationHandler<EditOrDeleteMentalSupportArticleRequirement, MentalSupportArticleResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteMentalSupportArticleRequirement requirement, MentalSupportArticleResult resource)
        {
            if(context.User.GetId() == resource.AuthorId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
