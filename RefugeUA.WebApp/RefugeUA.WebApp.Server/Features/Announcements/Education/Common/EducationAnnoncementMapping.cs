using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Common
{
    public static class EducationAnnouncementMapping
    {
        public readonly static Func<EducationAnnouncement, EducationAnnouncementResult> Func = a => new EducationAnnouncementResult()
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            Fee = a.Fee,
            Duration = a.Duration,
            Language = a.Language,
            EducationType = a.EducationType,
            TargetGroups = new[] { a.TargetGroup },
            InstitutionName = a.InstitutionName,
            IsClosed = a.IsClosed
        };
    }
}
