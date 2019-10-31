﻿// <auto-generated />
using System;
using AActivity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AActivity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191029053949_notificationletter")]
    partial class notificationletter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AActivity.Models.AcademicYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AcademicYears");
                });

            modelBuilder.Entity("AActivity.Models.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArName");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("AActivity.Models.AppSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AmountExternalCreditToTrip");

                    b.Property<float>("AmountInternalCreditToTrip");

                    b.Property<float>("AmountOmrahCreditToTrip");

                    b.Property<float>("AmountVisitCreditToTrip");

                    b.Property<int>("BookingTime");

                    b.Property<int>("QtyCollegesDelegates");

                    b.Property<int>("QtyDaysVisitEternal");

                    b.Property<int>("QtyDaysVisitInternal");

                    b.Property<int>("QtyDeanshipDelegates");

                    b.Property<int>("QtyExternalDaysTrip");

                    b.Property<int>("QtyExtirnalBuses");

                    b.Property<int>("QtyInstitutesDelegates");

                    b.Property<int>("QtyInternalDaysTrip");

                    b.Property<int>("QtyIntirnalBuses");

                    b.Property<int>("QtyOmrahMakkahDaysTrip");

                    b.Property<int>("QtyOmrahMedinaDaysTrip");

                    b.Property<int>("QtyPassengersInOneBus");

                    b.Property<int>("QtyUmrahBuses");

                    b.Property<int>("QtyVisitExtirnalBuses");

                    b.Property<int>("QtyVisitIntirnalBuses");

                    b.Property<string>("Stamp")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AppSettings");
                });

            modelBuilder.Entity("AActivity.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AActivity.Models.AppUserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("AActivity.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocationName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AActivity.Models.DelegateToSignutre", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("WonerSignutreId");

                    b.Property<int>("DelegatedToSignutreId");

                    b.Property<DateTime>("DateDelegate");

                    b.Property<bool>("Status");

                    b.HasKey("Id", "WonerSignutreId", "DelegatedToSignutreId");

                    b.HasIndex("DelegatedToSignutreId");

                    b.HasIndex("WonerSignutreId");

                    b.ToTable("DelegateToSignutres");
                });

            modelBuilder.Entity("AActivity.Models.EducationalBody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("EntityType")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Stamp")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EducationalBodies");
                });

            modelBuilder.Entity("AActivity.Models.FinishWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CashAdvance");

                    b.Property<float>("CashAdvanceAmont");

                    b.Property<DateTime>("DateDelgation");

                    b.Property<string>("DelegationBudy");

                    b.Property<int>("DelegationNumber");

                    b.Property<int>("EndWorkDuration");

                    b.Property<bool>("FoodsBuy");

                    b.Property<string>("JopDegree");

                    b.Property<bool>("LivingBuy");

                    b.Property<bool>("TransportBuy");

                    b.Property<bool>("TransportToToWorkBuy");

                    b.Property<int>("TripBookingId");

                    b.Property<int>("TripDelegateId");

                    b.HasKey("Id");

                    b.HasIndex("TripBookingId");

                    b.HasIndex("TripDelegateId");

                    b.ToTable("FinishWorks");
                });

            modelBuilder.Entity("AActivity.Models.JobsSignatorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("JobsSignatories");
                });

            modelBuilder.Entity("AActivity.Models.Letter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NoMersal");

                    b.Property<int>("TripBookingId");

                    b.Property<int>("TypeLetter");

                    b.HasKey("Id");

                    b.HasIndex("TripBookingId");

                    b.HasIndex("TypeLetter");

                    b.ToTable("Letters");
                });

            modelBuilder.Entity("AActivity.Models.LetterAdvancedDelegation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<float>("AmountAdditional");

                    b.Property<string>("CreditToEMployee");

                    b.Property<string>("EmployeeMobile");

                    b.Property<string>("EmployeeNomber");

                    b.Property<int>("LetterId");

                    b.Property<string>("Notes");

                    b.Property<int>("QtyStudents");

                    b.Property<string>("Statatus");

                    b.HasKey("Id");

                    b.HasIndex("LetterId");

                    b.ToTable("LetterAdvancedDelegations");
                });

            modelBuilder.Entity("AActivity.Models.LetterFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FirstMealDate");

                    b.Property<string>("FirstMealTime");

                    b.Property<DateTime>("LastMealDate");

                    b.Property<string>("LastMealTime");

                    b.Property<int>("LetterId");

                    b.Property<int>("QtyMeals");

                    b.Property<int>("QtyStudents");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LetterId");

                    b.HasIndex("UserId");

                    b.ToTable("LetterFoods");
                });

            modelBuilder.Entity("AActivity.Models.LetterSignutre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsHeOwnerOfSignature");

                    b.Property<int>("LetterId");

                    b.Property<int>("SignatureId");

                    b.Property<int>("WhoHasSignutre");

                    b.HasKey("Id");

                    b.HasIndex("LetterId");

                    b.HasIndex("SignatureId");

                    b.ToTable("LetterSignutres");
                });

            modelBuilder.Entity("AActivity.Models.LetterSignutreForAdvance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsHeOwnerOfSignature");

                    b.Property<int>("LetterAdvancedDelegationId");

                    b.Property<int>("SignatureId");

                    b.Property<int>("WhoHasSignutre");

                    b.HasKey("Id");

                    b.HasIndex("LetterAdvancedDelegationId");

                    b.HasIndex("SignatureId");

                    b.ToTable("LetterSignutreForAdvances");
                });

            modelBuilder.Entity("AActivity.Models.LetterTransport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LetterId");

                    b.Property<int>("QtyBuses");

                    b.Property<int>("QtyStudents");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LetterId");

                    b.HasIndex("UserId");

                    b.ToTable("LetterTransports");
                });

            modelBuilder.Entity("AActivity.Models.NotificationLetter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LetterId");

                    b.Property<int>("SignatureId");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("Time");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LetterId");

                    b.HasIndex("SignatureId");

                    b.HasIndex("UserId");

                    b.ToTable("NotificationLetter");
                });

            modelBuilder.Entity("AActivity.Models.SchedulingTripDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EducationalBodyId");

                    b.Property<string>("Notce");

                    b.Property<int>("SchedulingTripHeadId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("TripDate");

                    b.Property<int>("TripTypeId");

                    b.HasKey("Id");

                    b.HasIndex("EducationalBodyId");

                    b.HasIndex("SchedulingTripHeadId");

                    b.HasIndex("TripTypeId");

                    b.ToTable("SchedulingTripDetails");
                });

            modelBuilder.Entity("AActivity.Models.SchedulingTripHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcademicYear")
                        .IsRequired();

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Semester")
                        .IsRequired();

                    b.Property<bool>("Status");

                    b.Property<DateTime>("ToDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SchedulingTripHead");
                });

            modelBuilder.Entity("AActivity.Models.Signature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Degree");

                    b.Property<int>("JobsSignatorieId");

                    b.Property<string>("SignaturePhoto")
                        .IsRequired();

                    b.Property<bool>("Status");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("JobsSignatorieId");

                    b.HasIndex("UserId");

                    b.ToTable("Signatures");
                });

            modelBuilder.Entity("AActivity.Models.StudentsParticipatingInTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StudentName")
                        .IsRequired();

                    b.Property<int>("StudentNumber");

                    b.Property<int>("TripBookingId");

                    b.HasKey("Id");

                    b.HasIndex("TripBookingId");

                    b.ToTable("StudentsParticipatingInTrip");
                });

            modelBuilder.Entity("AActivity.Models.TripBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<int>("SchedulingTripDetailId");

                    b.Property<string>("TripLocationName")
                        .IsRequired();

                    b.Property<int>("TripQtyDays");

                    b.Property<int>("TripStatus");

                    b.Property<DateTime>("TripToDate");

                    b.Property<string>("TripTypeName");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("SchedulingTripDetailId");

                    b.ToTable("TripBookings");
                });

            modelBuilder.Entity("AActivity.Models.TripDelegate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Confirmed");

                    b.Property<string>("EmployeMobile")
                        .IsRequired();

                    b.Property<string>("EmployeeName")
                        .IsRequired();

                    b.Property<int>("EmployeeNumber");

                    b.Property<bool>("IsFromEducationBody");

                    b.Property<string>("JopName")
                        .IsRequired();

                    b.Property<int>("TripBookingId");

                    b.HasKey("Id");

                    b.HasIndex("TripBookingId");

                    b.ToTable("TripDelegates");
                });

            modelBuilder.Entity("AActivity.Models.TripReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Status");

                    b.Property<string>("TextBody")
                        .IsRequired();

                    b.Property<int>("TripBookingId");

                    b.HasKey("Id");

                    b.HasIndex("TripBookingId");

                    b.ToTable("TripReports");
                });

            modelBuilder.Entity("AActivity.Models.TripReportImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath");

                    b.Property<int>("TripReportId");

                    b.HasKey("Id");

                    b.HasIndex("TripReportId");

                    b.ToTable("TripReportImages");
                });

            modelBuilder.Entity("AActivity.Models.TripType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TripTypes");
                });

            modelBuilder.Entity("AActivity.Models.TypesOfLettersAndSignature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSignatureOwner");

                    b.Property<int>("SignatureId");

                    b.Property<DateTime>("StartAtDate");

                    b.Property<int>("TypesOfletterId");

                    b.Property<int?>("WonerSignatureId");

                    b.HasKey("Id");

                    b.HasIndex("SignatureId");

                    b.HasIndex("TypesOfletterId");

                    b.HasIndex("WonerSignatureId");

                    b.ToTable("TypesOfLettersAndSignatures");
                });

            modelBuilder.Entity("AActivity.Models.TypesOfletter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TypesOfletters");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AActivity.Models.AppUserRole", b =>
                {
                    b.HasOne("AActivity.Models.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.DelegateToSignutre", b =>
                {
                    b.HasOne("AActivity.Models.Signature", "DelegatedToSignutre")
                        .WithMany("DelegateToSignutres")
                        .HasForeignKey("DelegatedToSignutreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.Signature", "WonerSignutre")
                        .WithMany("WonerSignutres")
                        .HasForeignKey("WonerSignutreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.EducationalBody", b =>
                {
                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("EducationalBodies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.FinishWork", b =>
                {
                    b.HasOne("AActivity.Models.TripBooking", "TripBooking")
                        .WithMany("FinishWorks")
                        .HasForeignKey("TripBookingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.TripDelegate", "TripDelegate")
                        .WithMany("FinishWorks")
                        .HasForeignKey("TripDelegateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.Letter", b =>
                {
                    b.HasOne("AActivity.Models.TripBooking", "TripBooking")
                        .WithMany("Letters")
                        .HasForeignKey("TripBookingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.TypesOfletter", "TypesOfletter")
                        .WithMany()
                        .HasForeignKey("TypeLetter")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.LetterAdvancedDelegation", b =>
                {
                    b.HasOne("AActivity.Models.Letter", "Letter")
                        .WithMany("LetterAdvancedDelegations")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.LetterFood", b =>
                {
                    b.HasOne("AActivity.Models.Letter", "Letter")
                        .WithMany("LetterFoods")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("LetterFood")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.LetterSignutre", b =>
                {
                    b.HasOne("AActivity.Models.Letter", "Letter")
                        .WithMany("LetteSignutres")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.Signature", "Signature")
                        .WithMany("LetterSignutres")
                        .HasForeignKey("SignatureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.LetterSignutreForAdvance", b =>
                {
                    b.HasOne("AActivity.Models.LetterAdvancedDelegation", "Letter")
                        .WithMany("LetterSignutreForAdvances")
                        .HasForeignKey("LetterAdvancedDelegationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.Signature", "Signature")
                        .WithMany("LetterSignutreForAdvances")
                        .HasForeignKey("SignatureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.LetterTransport", b =>
                {
                    b.HasOne("AActivity.Models.Letter", "Letter")
                        .WithMany("LetterTransports")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("LetterTransports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.NotificationLetter", b =>
                {
                    b.HasOne("AActivity.Models.Letter", "Letter")
                        .WithMany("NotificationLetters")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.Signature", "Signature")
                        .WithMany("NotificationLetters")
                        .HasForeignKey("SignatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("NotificationLetters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.SchedulingTripDetail", b =>
                {
                    b.HasOne("AActivity.Models.EducationalBody", "EducationalBody")
                        .WithMany("SchedulingTripDetails")
                        .HasForeignKey("EducationalBodyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.SchedulingTripHead", "SchedulingTripHead")
                        .WithMany("SchedulingTripDetails")
                        .HasForeignKey("SchedulingTripHeadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.TripType", "TripType")
                        .WithMany("SchedulingTripDetails")
                        .HasForeignKey("TripTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.SchedulingTripHead", b =>
                {
                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("SchedulingTripHeads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.Signature", b =>
                {
                    b.HasOne("AActivity.Models.JobsSignatorie", "JobsSignatorie")
                        .WithMany("Signatures")
                        .HasForeignKey("JobsSignatorieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.AppUser", "User")
                        .WithMany("Signatures")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.StudentsParticipatingInTrip", b =>
                {
                    b.HasOne("AActivity.Models.TripBooking", "TripBooking")
                        .WithMany("StudentsParticipatingInTrips")
                        .HasForeignKey("TripBookingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.TripBooking", b =>
                {
                    b.HasOne("AActivity.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.SchedulingTripDetail", "SchedulingTripDetail")
                        .WithMany("TripBookings")
                        .HasForeignKey("SchedulingTripDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.TripDelegate", b =>
                {
                    b.HasOne("AActivity.Models.TripBooking", "TripBooking")
                        .WithMany("TripDelegates")
                        .HasForeignKey("TripBookingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.TripReport", b =>
                {
                    b.HasOne("AActivity.Models.TripBooking", "TripBooking")
                        .WithMany("TripReports")
                        .HasForeignKey("TripBookingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.TripReportImage", b =>
                {
                    b.HasOne("AActivity.Models.TripReport", "TripReport")
                        .WithMany("TripReportImages")
                        .HasForeignKey("TripReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AActivity.Models.TypesOfLettersAndSignature", b =>
                {
                    b.HasOne("AActivity.Models.Signature", "Signature")
                        .WithMany("TypesOfLettersAndSignatures")
                        .HasForeignKey("SignatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.TypesOfletter", "TypesOfletter")
                        .WithMany("TypesOfLettersAndSignatures")
                        .HasForeignKey("TypesOfletterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AActivity.Models.TypesOfLettersAndSignature", "WonerSignature")
                        .WithMany()
                        .HasForeignKey("WonerSignatureId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AActivity.Models.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AActivity.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AActivity.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("AActivity.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
