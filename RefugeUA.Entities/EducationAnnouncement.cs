using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;

namespace RefugeUA.Entities
{
    public class EducationAnnouncement : Announcement
    {
        public string EducationType { get; set; } = default!;

        public string InstitutionName { get; set; } = default!;

        public string TargetGroup { get; set; } = default!;

        public bool IsFree { get; set; }

        public decimal? Fee { get; set; }

        public int Duration { get; set; } = default!;

        public string Language { get; set; } = default!;
    }
}
