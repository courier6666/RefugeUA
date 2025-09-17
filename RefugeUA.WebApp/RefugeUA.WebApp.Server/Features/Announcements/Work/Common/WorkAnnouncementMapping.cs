using RefugeUA.Entities;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Common
{
    public static class WorkAnnouncementMapping
    {
        public static Expression<Func<WorkAnnouncement, WorkAnnouncementResult>> Expression => a => new WorkAnnouncementResult()
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            JobPosition = a.JobPosition,
            CompanyName = a.CompanyName,
            SalaryLower = a.SalaryLower,
            SalaryUpper = a.SalaryUpper,
            RequirementsContent = a.RequirementsContent,
            WorkCategoryId = a.WorkCategoryId,
            IsClosed = a.IsClosed
        };

        public static Func<WorkAnnouncement, WorkAnnouncementResult> Func => a => new WorkAnnouncementResult()
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            JobPosition = a.JobPosition,
            CompanyName = a.CompanyName,
            SalaryLower = a.SalaryLower,
            SalaryUpper = a.SalaryUpper,
            RequirementsContent = a.RequirementsContent,
            WorkCategoryId = a.WorkCategoryId,
            IsClosed = a.IsClosed
        };
    }
}
