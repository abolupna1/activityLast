using AActivity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Data
{

    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                                            UserManager<AppUser> userManager,
                                            RoleManager<AppRole> roleManager)
        {
            context.Database.EnsureCreated();

            int adminID = 0;

       
            string roleAdmin = "Admin";
            string roleAdminAr = "مدير النظام";

            string roleSupervisor = "Supervisor";
            string roleSupervisorAr = " مشرف جهة تعليمية";

            string roleDirectorOfSocialActivity = "DirectorOfSocialActivity";
            string roleDirectorOfSocialActivityAr = "   مدير النشاط الإجتماعي";

            string roleSocialActivityOfficer = "SocialActivityOfficer";
            string roleSocialActivityOfficerAr = " مسؤول النشاط الإجتماعي";

            string roleActivityOfficer = "ActivityOfficer";
            string roleActivityOfficerAr = " مدير إدارة النشاط";

            string roleDeanForActivity = "DeanForActivity ";
            string roleDeanForActivityAr = "وكيل العمادة لشؤون النشاط";

            string roleAmaid = "Amaid";
            string roleAmaidAr = "عميد شؤون الطلاب";


            string password = "2329472589";

            if (await roleManager.FindByNameAsync(roleAmaid) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleAmaid, roleAmaidAr));
            }

            if (await roleManager.FindByNameAsync(roleDeanForActivity) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleDeanForActivity, roleDeanForActivityAr));
            }

            if (await roleManager.FindByNameAsync(roleAdmin) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleAdmin, roleAdminAr));
            }


            if (await roleManager.FindByNameAsync(roleSupervisor) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleSupervisor, roleSupervisorAr));
            }


            if (await roleManager.FindByNameAsync(roleDirectorOfSocialActivity) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleDirectorOfSocialActivity, roleDirectorOfSocialActivityAr));
            }

            if (await roleManager.FindByNameAsync(roleSocialActivityOfficer) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleSocialActivityOfficer, roleSocialActivityOfficerAr));
            }

            if (await roleManager.FindByNameAsync(roleActivityOfficer) == null)
            {
                var resault = await roleManager.CreateAsync(new AppRole(roleActivityOfficer, roleActivityOfficerAr));
            }



            if (await userManager.FindByNameAsync("2329472589") == null)
            {
                var user = new AppUser
                {
                    UserName = "2329472589",
                    Email = "abolupna1@gmail.com",
                    FullName = "ايدوم ابراهيم"

                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                  
                    await userManager.AddToRoleAsync(user, roleAdmin);

                }
                adminID = user.Id;
            }

           // =========================
          if (!context.TripTypes.Any(t => t.Id > 0))
            {
                IList<TripType> Trips = new List<TripType>()
            {
                new TripType(){Name = "عمرة" },
                new TripType(){Name = "داخلية" },
                new TripType(){Name = "خارجية" },
                new TripType(){Name = "زيارة" },
            };

                foreach (var t in Trips)
                {
                    context.Add(t);
                    await context.SaveChangesAsync();
                }
            }

            //====================================
            if (!context.Cities.Any(t => t.Id > 0))
            {
                IList<City> city = new List<City>()
            {
                new City(){LocationName = "المدينة المنورة" },
                new City(){LocationName = "مكة المكرمة" },

            };

                foreach (var t in city)
                {
                    context.Add(t);
                    await context.SaveChangesAsync();
                }
            }
            // ======================

            //====================================
            if (!context.JobsSignatories.Any(t => t.Id > 0))
            {
                IList<JobsSignatorie> jobs = new List<JobsSignatorie>()
            {
                new JobsSignatorie(){Name="رئيس قسم النشاط الاجتماعي" },
                new JobsSignatorie(){Name="مدير إدارة النشاط" },
                new JobsSignatorie(){Name="وكيل العمادة لشؤون النشاط" },
                new JobsSignatorie(){Name="عميد شؤون الطلاب" },
                new JobsSignatorie(){Name="مفوض" },


            };

                foreach (var t in jobs)
                {
                    context.Add(t);
                    await context.SaveChangesAsync();
                }
            }
            // ======================

            //====================================
            if (!context.TypesOfletters.Any(t => t.Id > 0))
            {
                IList<TypesOfletter> typesletters = new List<TypesOfletter>()
            {
                new TypesOfletter(){Name="النقل" },
                new TypesOfletter(){Name="التغذية" },
                new TypesOfletter(){Name="الإنتداب" },
                new TypesOfletter(){Name="السلف" },
                new TypesOfletter(){Name="الى من يهمه الأمر" },
                new TypesOfletter(){Name="انهاء مهمة" },



            };

                context.AddRange(typesletters);
                await context.SaveChangesAsync();
            }
            // ======================



            ////====================================
            if (context.StudentsParticipatingInTrip.ToList().Count == 0)
            {

                var boking = await context.TripBookings.LastOrDefaultAsync();
                for (int i = 0; i < 92; i++)
                {
                    StudentsParticipatingInTrip student = new StudentsParticipatingInTrip()
                    {
                        StudentName = "student" + i + "",
                        StudentNumber = 111111 + i,
                        TripBookingId = 1,
                    };

                    context.Add(student);
                    await context.SaveChangesAsync();

                }


            }
            //// ======================


        }
    }

}
