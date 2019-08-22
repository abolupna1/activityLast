using System;
using System.Collections.Generic;
using System.Text;
using AActivity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<SignutreDelegate> SignutreDelegates { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<TripTransportSignature> TripTransportSignatures { get; set; }
        public DbSet<TripFoodsSignature> TripFoodsSignatures { get; set; }
        public DbSet<TripFood> TripFoods { get; set; }
        
     
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



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

     

    }
}
