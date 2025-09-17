namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common
{
    public class VolunteerGroupResult
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;

        public string DescriptionContent { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public int? FollowersCount { get; set; }

        public int? AdministratorsCount { get; set; }

        public int? VolunteerEventsCount { get; set; }
    }
}
