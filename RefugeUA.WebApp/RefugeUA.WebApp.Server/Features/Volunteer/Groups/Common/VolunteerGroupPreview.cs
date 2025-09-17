namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common
{
    public class VolunteerGroupPreview
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;

        public int? FollowersCount { get; set; }

        public int? AdministratorsCount { get; set; }

        public int? VolunteerEventsCount { get; set; }
    }
}
