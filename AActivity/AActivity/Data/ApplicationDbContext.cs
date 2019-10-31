using System;
using System.Collections.Generic;
using System.Text;
using AActivity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AActivity.Areas.Sociologist.ModelViews;

namespace AActivity.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>,
        AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        >
       
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DelegateToSignutre>()
              .HasKey(t => new {t.Id,t.WonerSignutreId, t.DelegatedToSignutreId });

            builder.Entity<DelegateToSignutre>()
                .HasOne(pt => pt.WonerSignutre)
                .WithMany(p => p.WonerSignutres)
                .HasForeignKey(pt => pt.WonerSignutreId);

            builder.Entity<DelegateToSignutre>()
                .HasOne(pt => pt.DelegatedToSignutre)
                .WithMany(p => p.DelegateToSignutres)
                .HasForeignKey(pt => pt.DelegatedToSignutreId);

            builder.Entity<AppUserRole>(

                userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                    userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                    userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.UserId)
                    .IsRequired();
                }

                );









        }




        public DbSet<EducationalBody> EducationalBodies { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<TripType> TripTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<SchedulingTripHead> SchedulingTripHead { get; set; }

        public DbSet<SchedulingTripDetail> SchedulingTripDetails { get; set; }
        public DbSet<TripBooking> TripBookings { get; set; }
        public DbSet<StudentsParticipatingInTrip> StudentsParticipatingInTrip { get; set; }
        public DbSet<TripDelegate> TripDelegates { get; set; }
        public DbSet<JobsSignatorie> JobsSignatories { get; set; }
        public DbSet<TypesOfletter> TypesOfletters { get; set; }

        
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<TypesOfLettersAndSignature> TypesOfLettersAndSignatures { get; set; }
        public DbSet<DelegateToSignutre> DelegateToSignutres { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<LetterTransport> LetterTransports { get; set; }       
        public DbSet<LetterSignutre> LetterSignutres { get; set; }
        public DbSet<LetterFood> LetterFoods { get; set; }
        public DbSet<LetterAdvancedDelegation> LetterAdvancedDelegations { get; set; }
        public DbSet<LetterSignutreForAdvance> LetterSignutreForAdvances { get; set; }
        public DbSet<TripReport> TripReports { get; set; }
        public DbSet<TripReportImage> TripReportImages { get; set; }
        public DbSet<FinishWork> FinishWorks { get; set; }


        

        public DbSet<NotificationLetter> NotificationLetter { get; set; }





    }
}
