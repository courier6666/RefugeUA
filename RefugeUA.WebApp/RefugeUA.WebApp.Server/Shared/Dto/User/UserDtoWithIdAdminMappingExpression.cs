using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Shared.Dto.User
{
    public static class UserDtoWithIdAdminMappingExpression
    {
        public static Expression<Func<AppUser, UserDtoWithIdAdmin>> AppUserToDtoWithIdAdmin =>
            user => new UserDtoWithIdAdmin
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                CreatedAt = user.CreatedAt,
                IsAccepted = user.IsAccepted,
                IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
                IsEmailConfirmed = user.EmailConfirmed,
                DateOfBirth = user.DateOfBirth,
                District = user.District,
                ProfileImageUrl = user.ProfileImageUrl,
                Role = user.UserRoles.Select(x => x.Role)
                    .FirstOrDefault()!.Name!,
            };
    }
}
