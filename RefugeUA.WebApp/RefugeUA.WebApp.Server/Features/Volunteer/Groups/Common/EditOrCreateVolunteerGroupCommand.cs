namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common
{
    public class EditOrCreateVolunteerGroupCommand
    {
        public string Title { get; set; } = default!;

        public string DescriptionContent { get; set; } = default!;
    }
}
