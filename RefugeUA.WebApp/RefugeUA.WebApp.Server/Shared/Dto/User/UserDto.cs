namespace RefugeUA.WebApp.Server.Shared.Dto.User
{
    public class UserDto
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string Role { get; set; } = default!;

        public string ProfileImageUrl { get; set; } = default!;
    }
}
