using RefugeUA.WebApp.Server.Shared.Converters;
using System.Text.Json.Serialization;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Common
{
    public class ListEducationAnnouncementQuery
    {
        public string? Prompt { get; set; }

        public bool? IsClosed {  get; set; }

        public string? District { get; set; }

        public string? AnnouncementGroup { get; set; }

        public decimal? FeeLower { get; set; }

        public decimal? FeeUpper { get; set; }

        public bool? IsFreeOnly { get; set; }

        public int? DurationLower { get; set; }

        public int? DurationUpper { get; set; }

        public string[]? EducationTypes { get; set; }

        public string[]? TargetGroups { get; set; }

        public string? Language { get; set; }
    }
}
