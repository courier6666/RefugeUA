using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Authorization.Handlers.Announcements.EditOrDelete;
using RefugeUA.WebApp.Server.Authorization.Handlers.MentalSupportArticle.EditOrDelete;
using RefugeUA.WebApp.Server.Authorization.Handlers.PsychologistInformation.EditOrDelete;
using RefugeUA.WebApp.Server.Authorization.Handlers.User;
using RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerEvents.EditOrDelete;
using RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerGroups.EditOrDelete;
using RefugeUA.WebApp.Server.Authorization.Requierements;

namespace RefugeUA.WebApp.Server.Extensions.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddRefugeUaAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.EditDeleteAnnouncementPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser().AddRequirements(new EditOrDeleteAnnouncementRequirement());
                });

                options.AddPolicy(Policies.EditUserPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser().AddRequirements(new EditUserRequirement());
                });

                options.AddPolicy(Policies.EditDeleteVolunteerEventPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser().AddRequirements(new EditOrDeleteVolunteerEventRequirement());
                });

                options.AddPolicy(Policies.EditDeleteVolunteerGroupPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser().AddRequirements(new EditOrDeleteVolunteerGroupRequirement());
                });

                options.AddPolicy(Policies.EditDeletePsychologistInformationPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser().AddRequirements(new EditOrDeletePsychologistInformationRequirement());
                });

                options.AddPolicy(Policies.EditDeleteMentalSupportArticlePolicy, policy =>
                {
                    policy.RequireAuthenticatedUser().AddRequirements(new EditOrDeleteMentalSupportArticleRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, AdminEditOrDeleteAnnouncementHandler>();
            services.AddSingleton<IAuthorizationHandler, OwnerEditOrDeleteAnnouncementHandler>();
            services.AddSingleton<IAuthorizationHandler, CommunityAdminEditOrDeleteAnnouncementHandler>();

            services.AddScoped<IAuthorizationHandler, CommunityAdminEditUserHandler>();
            services.AddScoped<IAuthorizationHandler, AdminEditUserHandler>();

            services.AddSingleton<IAuthorizationHandler, AdminEditOrDeleteVolunteerEventHandler>();
            services.AddSingleton<IAuthorizationHandler, AuthorEditOrDeleteVolunteerEventHandler>();
            services.AddSingleton<IAuthorizationHandler, CommunityAdminEditOrDeleteVolunteerEventHandler>();
            services.AddScoped<IAuthorizationHandler, GroupAdminEditOrDeleteVolunteerEventHandler>();

            services.AddScoped<IAuthorizationHandler, AdminEditOrDeleteVolunteerGroupHandler>();
            services.AddScoped<IAuthorizationHandler, GroupAdminEditOrDeleteVolunteerGroupHandler>();

            services.AddSingleton<IAuthorizationHandler, AdminEditOrDeletePsychologistInformationHandler>();
            services.AddSingleton<IAuthorizationHandler, AuthorEditOrDeletePsychologistInformationHandler>();

            services.AddSingleton<IAuthorizationHandler, AdminEditOrDeleteArticleHandler>();
            services.AddSingleton<IAuthorizationHandler, AuthorEditOrDeleteArticleHandler>();
        }
    }
}
