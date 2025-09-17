using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Common
{
    public class EditOrCreateWorkAnnouncementCommand : BaseEditOrCreateAnnouncementCommand
    {
        public string JobPosition { get; set; } = default!;

        public string CompanyName { get; set; } = default!;

        public decimal? SalaryLower { get; set; }

        public decimal? SalaryUpper { get; set; }

        public string RequirementsContent { get; set; } = default!;

        public long WorkCategoryId { get; set; }
    }
}
