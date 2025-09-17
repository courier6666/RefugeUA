namespace RefugeUA.WebApp.Server.Features.Authentication.Register
{
    public class RegisterCommand
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Role { get; set; } = default!;

        public string District { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string ConfirmPassword { get; set; } = default!;
    }
}
