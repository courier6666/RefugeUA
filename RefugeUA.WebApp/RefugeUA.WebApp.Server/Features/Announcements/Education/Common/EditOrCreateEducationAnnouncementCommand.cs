using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Shared.Converters;
using System.Text.Json.Serialization;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Common
{
    public class EditOrCreateEducationAnnouncementCommand : BaseEditOrCreateAnnouncementCommand
    {
        public string EducationType { get; set; } = default!;

        public string InstitutionName { get; set; } = default!;

        public string[] TargetGroups { get; set; } = default!;

        public decimal? Fee { get; set; }
        
        public int Duration { get; set; }

        public string Language { get; set; } = default!;
    }
}
