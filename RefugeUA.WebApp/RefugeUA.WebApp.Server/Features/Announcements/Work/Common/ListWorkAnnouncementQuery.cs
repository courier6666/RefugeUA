using Microsoft.AspNetCore.Mvc;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Common
{
    public class ListWorkAnnouncementQuery
    {
        public string? District { get; set; }

        public string? AnnouncementGroup { get; set; }

        public string? Prompt { get; set; }
        
        public decimal? SalaryLower { get; set; }
        
        public decimal? SalaryUpper { get; set; }
        
        public bool? SalaryNotSet { get; set; }
        
        public int[]? JobCategories { get; set; }

        public bool? IsClosed { get; set; }
    }
}
