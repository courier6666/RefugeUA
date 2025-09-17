using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class WorkAnnouncement : Announcement
    {
        public string JobPosition { get; set; } = default!;

        public string CompanyName { get; set; } = default!;

        public decimal? SalaryLower { get; set; }

        public decimal? SalaryUpper { get; set; }

        public string RequirementsContent { get; set; } = default!;

        public long WorkCategoryId { get; set; }

        public WorkCategory WorkCategory { get; set; } = default!;
    }
}
