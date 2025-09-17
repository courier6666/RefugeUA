using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Common
{
    public class EducationAnnouncementResult : BaseAnnouncementResult
    {
        public string EducationType { get; set; } = default!;

        public string InstitutionName { get; set; } = default!;

        public string[] TargetGroups { get; set; } = default!;

        public decimal? Fee { get; set; }

        public int Duration { get; set; }

        public string Language { get; set; } = default!;
    }
}
