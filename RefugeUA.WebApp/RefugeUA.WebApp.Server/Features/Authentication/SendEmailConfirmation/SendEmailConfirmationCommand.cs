namespace RefugeUA.WebApp.Server.Features.Authentication.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
