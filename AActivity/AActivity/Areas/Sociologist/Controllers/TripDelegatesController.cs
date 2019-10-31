using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using System.Security.Claims;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class TripDelegatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripDelegatesController(ApplicationDbContext context)
        {
            
            _context = context;
        }

        private void booking(int bokingId, out int scdualDetailId, out List<TripDelegate> employees,
            out int qtyStudentInBus, out int qtyStudentRegisterd,
             out int Required, out int Registered, out int Remainder,
            out int RequiredAmada, out int RequiredEducation,
            out int RegisteredAmadd, out int RegisteredEducation,
            out int RemainderAmada, out int RemainderEducation)
        {
            var boking = _context.TripBookings
                .Include(e=>e.SchedulingTripDetail.EducationalBody)
                .Include(e=>e.TripDelegates)
                .Include(e=>e.StudentsParticipatingInTrips)
                .FirstOrDefault(i=>i.Id== bokingId);
            employees = boking.TripDelegates.ToList();
            var appSetting = _context.AppSettings.FirstOrDefault();
            scdualDetailId = boking.SchedulingTripDetailId;
            qtyStudentRegisterd = boking.StudentsParticipatingInTrips.Count();
            if (boking.TripTypeName =="عمرة")
            {
              
                RequiredAmada = appSetting.QtyDeanshipDelegates * appSetting.QtyUmrahBuses;
                RegisteredAmadd = boking.TripDelegates.Where(a => a.IsFromEducationBody == false).Count();
                RemainderAmada = RequiredAmada - RegisteredAmadd;
                
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    qtyStudentInBus = 46;
                    RequiredEducation = appSetting.QtyInstitutesDelegates * appSetting.QtyUmrahBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
                else
                {
                    qtyStudentInBus = 47;
                    RequiredEducation = appSetting.QtyCollegesDelegates * appSetting.QtyUmrahBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
            }
            else if (boking.TripTypeName == "داخلية")
            {
                RequiredAmada = 0 ;
                RegisteredAmadd = 0;
                RemainderAmada = 0;
                qtyStudentInBus = 47;
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                   
                    RequiredEducation = appSetting.QtyInstitutesDelegates * appSetting.QtyIntirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
                else
                {
                    RequiredEducation = appSetting.QtyCollegesDelegates * appSetting.QtyIntirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
            }
            else if (boking.TripTypeName == "خارجية")
            {
                RequiredAmada = appSetting.QtyDeanshipDelegates * appSetting.QtyExtirnalBuses;
                RegisteredAmadd = boking.TripDelegates.Where(a => a.IsFromEducationBody == false).Count();
                RemainderAmada = RequiredAmada - RegisteredAmadd;
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    qtyStudentInBus = 46;
                    RequiredEducation = appSetting.QtyInstitutesDelegates * appSetting.QtyExtirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
                else
                {
                    qtyStudentInBus = 47;
                    RequiredEducation = appSetting.QtyCollegesDelegates * appSetting.QtyExtirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
            }
            else if (boking.TripTypeName == "زيارة داخلية")
            {
                RequiredAmada = 0;
                RegisteredAmadd = 0;
                RemainderAmada =0;
                qtyStudentInBus = 47;
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    RequiredEducation = appSetting.QtyInstitutesDelegates * appSetting.QtyVisitIntirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
                else
                {
                    RequiredEducation = appSetting.QtyCollegesDelegates * appSetting.QtyVisitIntirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
            }
            else if (boking.TripTypeName == "زيارة خارجية")
            {
                RequiredAmada = appSetting.QtyDeanshipDelegates * appSetting.QtyVisitExtirnalBuses;
                RegisteredAmadd = boking.TripDelegates.Where(a => a.IsFromEducationBody == false).Count();
                RemainderAmada = RequiredAmada - RegisteredAmadd;
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    qtyStudentInBus = 46;
                    RequiredEducation = appSetting.QtyInstitutesDelegates * appSetting.QtyVisitExtirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
                else
                {
                    qtyStudentInBus = 47;
                    RequiredEducation = appSetting.QtyCollegesDelegates * appSetting.QtyVisitExtirnalBuses;
                    RegisteredEducation = boking.TripDelegates.Where(a => a.IsFromEducationBody == true).Count();
                    RemainderEducation = RequiredEducation - RegisteredEducation;

                    Required = RequiredAmada + RequiredEducation;
                    Registered = boking.TripDelegates.Count();
                    Remainder = RemainderAmada + RemainderEducation;
                }
            }
            else
            {
                Required = 0; Registered = 0; Remainder = 0;
                RequiredAmada = 0; RegisteredAmadd = 0; RemainderAmada = 0;
                RequiredEducation = 0; RegisteredEducation = 0; RemainderEducation = 0;
                qtyStudentInBus = 0;

            }
        }

        [HttpGet()]
        [Route("Sociologist/TripDelegates/Index/{bokingId:int}")]
        public async Task<IActionResult> Index(int bokingId)
        {
            if (!TripBookingExists(bokingId))
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }

            booking(bokingId, out int scdualDetailId, out List<TripDelegate> employees,
              out int qtyStudentInBus, out int qtyStudentRegisterd,
             out int Required, out int Registered, out int Remainder,
            out int RequiredAmada, out int RequiredEducation,
            out int RegisteredAmadd, out int RegisteredEducation,
            out int RemainderAmada, out int RemainderEducation) ;

            int buses = countBuses(qtyStudentInBus, qtyStudentRegisterd);
            ViewBag.TripBookingId = bokingId;
            ViewBag.scdualDetailId = scdualDetailId;
            ViewBag.Remainder = Remainder <= buses ? Remainder : buses;
            ViewBag.RemainderAmada = RemainderAmada <= buses ? RemainderAmada : buses;
            ViewBag.RemainderEducation = RemainderEducation <= buses ? RemainderEducation : buses;
            return View( employees);
        }
        public IActionResult Adapter(int bokingId, int count)
        {

            return RedirectToAction(nameof(Create), new { bokingId = bokingId, count = count });
        }

        [HttpGet(), Route("Sociologist/TripDelegates/Create/{bokingId:int}/{count:int}")]

        public async Task<IActionResult> Create(int bokingId ,int count)
        {
            if (!TripBookingExists(bokingId))
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }
            ViewBag.TripBookingId = bokingId;
            countEmps( bokingId, count, out int countEmployees);
            ViewBag.embloyees = countEmployees;
            return View();
        }
        
        private int countBuses(int qtyStudentInBus, int qtyStudentRegisterd)
        {
            int bus = 0;
            int minBus =  _context.AppSettings.FirstOrDefault().QtyPassengersInOneBus;
          
            while (qtyStudentRegisterd >= qtyStudentInBus)
            {
                bus++;
                qtyStudentRegisterd -= qtyStudentInBus;
            }

            if (qtyStudentRegisterd < qtyStudentInBus && qtyStudentRegisterd >= minBus)
            {
                bus++;
            }
            return bus;
        }
        private void countEmps(int bokingId,int count, out int countEmployees)
        {
            booking(bokingId,   out int scdualDetailId, out List<TripDelegate> employees,
                                out int qtyStudentInBus, out int qtyStudentRegisterd,
                                out int Required, out int Registered, out int Remainder,
                                out int RequiredAmada, out int RequiredEducation,
                                out int RegisteredAmadd, out int RegisteredEducation,
                                out int RemainderAmada, out int RemainderEducation);
           int buses =  countBuses( qtyStudentInBus,  qtyStudentRegisterd);
            if (User.IsInRole("Admin"))
            {
                if(Remainder > count)
                {
                    countEmployees = count <= buses ? count : buses;
                }
                else
                {
                    countEmployees = Remainder <= buses ? Remainder : buses;
                }

            }
            else if (User.IsInRole("DirectorOfSocialActivity") || User.IsInRole("SocialActivityOfficer"))
            {
                if (RemainderAmada > count)
                {
                    countEmployees = count <= buses ? count : buses;
                }
                else
                {
                    countEmployees = RemainderAmada <= buses ? RemainderAmada : buses;
                }

            }
            else if (User.IsInRole("Supervisor"))
            {
                if (RemainderEducation > count)
                {
                    countEmployees = count <= buses ? count : buses;
                }
                else
                {
                    countEmployees = RemainderEducation >= buses ? RemainderEducation : buses;
                }

            }
            else
            {
                countEmployees = 0;
            }
          
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost(), Route("Sociologist/TripDelegates/Create/{bokingId:int}/{count:int}")]
        public async Task<IActionResult> Create(int bokingId ,int count, List<TripDelegate> tripDelegate)
        {
            // tripDelegate.ForEach(e=>e.IsFromEducationBody ==)
            booking(bokingId, out int scdualDetailId, out List<TripDelegate> employees,
            out int qtyStudentInBus, out int qtyStudentRegisterd,
            out int Required, out int Registered, out int Remainder,
            out int RequiredAmada, out int RequiredEducation,
            out int RegisteredAmadd, out int RegisteredEducation,
            out int RemainderAmada, out int RemainderEducation);
            int amada = tripDelegate.Where(e => e.IsFromEducationBody == false).Count();
            int education = tripDelegate.Where(e => e.IsFromEducationBody == true).Count();
            if (RemainderAmada >= amada && education >= RemainderEducation)
            {

          


                if (ModelState.IsValid)
                {
                    List<int> ii = tripDelegate.Select(s => s.EmployeeNumber).ToList();
                    if (studentsCeck(ii) > 0)
                    {
                        TempData["message"] = "رقم الموظف مكرر :" + studentsCeck(ii);
                    }
                    else if (studentsCeckInDb(tripDelegate, bokingId) > 0)
                    {
                        TempData["message"] = " الموظف موجود في قائمة المنتدبين مسبقا :" + studentsCeckInDb(tripDelegate, bokingId);
                    }
                    else
                    {
                        _context.AddRangeAsync(tripDelegate);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), new { bokingId = tripDelegate.FirstOrDefault().TripBookingId });
                    }

                }
            }
            else
            {
                ViewBag.TripBookingId = bokingId;
                countEmps(bokingId, count, out int countEmployees);
                ViewBag.embloyees = countEmployees;
                ViewBag.amada = RemainderAmada;
                ViewBag.education = RemainderEducation;
                return View(tripDelegate);
            }

            return View(tripDelegate);
        }

        // GET: Sociologist/TripDelegates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }

            var tripDelegate = await _context.TripDelegates.FindAsync(id);
            if (tripDelegate == null)
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }

            ViewData["TripBookingId"] = tripDelegate.TripBookingId;
            return View(tripDelegate);
        }

        // POST: Sociologist/TripDelegates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeNumber,EmployeeName,EmployeMobile,JopName,TripBookingId")] TripDelegate tripDelegate)
        {
          

            if (id != tripDelegate.Id)
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }

            if (ModelState.IsValid)
            {
               
                if (empDelegateExist(id,  tripDelegate.EmployeeNumber , tripDelegate.TripBookingId))
                {
                    TempData["message"] = " رقم الموظف (" + tripDelegate.EmployeeNumber + ") موجود مسبقا في نفس الرحلة !! ";
                    ViewData["TripBookingId"] = tripDelegate.TripBookingId;
                    return View(tripDelegate);
                }
                _context.Update(tripDelegate);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { bokingId = tripDelegate.TripBookingId });
            }
            ViewData["TripBookingId"] = tripDelegate.TripBookingId;
            return View(tripDelegate);
        }

        // GET: Sociologist/TripDelegates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }

            var tripDelegate = await _context.TripDelegates
                .Include(t => t.TripBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripDelegate == null)
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }

            return View(tripDelegate);
        }

        // POST: Sociologist/TripDelegates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripDelegate = await _context.TripDelegates.FindAsync(id);
            _context.TripDelegates.Remove(tripDelegate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { bokingId = tripDelegate.TripBookingId});
        }

        private bool TripDelegateExists(int id)
        {
            return _context.TripDelegates.Any(e => e.Id == id);
        }

        private bool TripBookingExists(int id)
        {
            return _context.TripBookings.Any(e => e.Id == id);
        }

        private bool empDelegateExist(int id, int EmployeeNumber, int TripBookingId)
        {
            return _context.TripDelegates.Any(e => e.Id != id && e.TripBookingId == TripBookingId && e.EmployeeNumber == EmployeeNumber);
        }
       

        private int studentsCeckInDb(List<TripDelegate> td ,int bokingId)
        {
           var tripD= _context.TripDelegates.Where(d => d.TripBookingId == bokingId).ToList();
            foreach (var i in td)
            {
                foreach (var j in tripD)
                {
                   if (j.EmployeeNumber == i.EmployeeNumber) { return i.EmployeeNumber; }

                }
            }
            return 0;
        }

        private int studentsCeck(List<int> EmpNumber)
        {
            for (int i = 0; i < EmpNumber.Count(); i++)
            {
                for (int j = i + 1; j < EmpNumber.Count(); j++)
                {
                    if (EmpNumber[i] == EmpNumber[j] && i != j)
                    {

                        return EmpNumber[i];
                    }
                }
            }
            return 0;
        }
    }
}
