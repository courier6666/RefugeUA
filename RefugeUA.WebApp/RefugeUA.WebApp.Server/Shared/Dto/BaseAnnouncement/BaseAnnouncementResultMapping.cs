using RefugeUA.Entities.Abstracts;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement
{
    public static class BaseAnnouncementResultMapping
    {
        public static Func<Announcement, BaseAnnouncementResult> Func =>
            announcement => new BaseAnnouncementResult
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Content = announcement.Content,
                CreatedAt = announcement.CreatedAt,
                IsAccepted = announcement.Accepted,
                NonAcceptenceReason = announcement.NonAcceptenceReason,
                IsClosed = announcement.IsClosed,
                AuthorId = announcement.AuthorId ?? 0,
            };
    }
}
