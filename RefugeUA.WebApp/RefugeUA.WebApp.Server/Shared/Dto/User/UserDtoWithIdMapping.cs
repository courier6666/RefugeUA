using RefugeUA.DatabaseAccess.Identity;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Shared.Dto.User
{
    public static class UserDtoWithIdMapping
    {
        public static Expression<Func<AppUser, UserDtoWithId>> Expression =>
            user => new UserDtoWithId
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                CreatedAt = user.CreatedAt,
                Role = user.UserRoles.Select(x => x.Role)
                    .FirstOrDefault()!.Name!,
            };
    }
}
