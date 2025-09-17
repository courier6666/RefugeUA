using Microsoft.AspNetCore.Mvc;

namespace RefugeUA.WebApp.Server.Features.Profile.EditMine
{
    public class EditMyProfileCommand
    {
        [FromForm]
        public string FirstName { get; set; } = default!;

        [FromForm]
        public string LastName { get; set; } = default!;

        [FromForm]
        public DateTime DateOfBirth { get; set; }

        [FromForm]
        public string PhoneNumber { get; set; } = default!;

        [FromForm]
        public string? District { get; set; }

        [FromForm]
        public IFormFile? ProfilePicture { get; set; }
    }
}
