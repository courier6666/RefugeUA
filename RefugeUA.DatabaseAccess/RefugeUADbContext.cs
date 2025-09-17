using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess
{
    public class RefugeUADbContext : IdentityDbContext<AppUser,
        AppRole,
        long,
        IdentityUserClaim<long>,
        AppUserRole,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>>
    {
        public RefugeUADbContext(DbContextOptions<RefugeUADbContext> dbContextOptions) :
            base (dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(RefugeUADbContext))!);
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<AccomodationAnnouncement> AccomodationAnnouncements { get; set; }

        public DbSet<EducationAnnouncement> EducationAnnouncements { get; set; }

        public DbSet<WorkAnnouncement> WorkAnnouncements { get; set; }

        public DbSet<WorkCategory> WorkCategories { get; set; }

        public DbSet<AnnouncementGroup> AnnouncementGroups { get; set; }

        public DbSet<AnnouncementResponse> AnnouncementResponses { get; set; }

        public DbSet<ContactInformation> ContactInformation { get; set; }

        public DbSet<MentalSupportArticle> MentalSupportArticles { get; set; }

        public DbSet<PsychologistInformation> PsychologistInformation { get; set; }

        public DbSet<VolunteerEventScheduleItem> VolunteerEventScheduleItems { get; set; }

        public DbSet<VolunteerEvent> VolunteerEvents { get; set; }

        public DbSet<VolunteerGroup> VolunteerGroups { get; set; }
    }
}
