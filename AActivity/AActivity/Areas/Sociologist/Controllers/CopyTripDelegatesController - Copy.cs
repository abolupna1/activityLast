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
    public class CopyTripDelegatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CopyTripDelegatesController(ApplicationDbContext context)
        {
            
            _context = context;
        }

     
        [HttpGet()]
        public async Task<IActionResult> Index(int bokingId ,string error = null )
        {
            if (!TripBookingExists(bokingId))
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }
            var TripBoking = await _context.TripBookings.Include(s=>s.StudentsParticipatingInTrips).FirstOrDefaultAsync(b => b.Id == bokingId);
          ViewBag.countStudend =  TripBoking.StudentsParticipatingInTrips.Where(t => t.TripBookingId == bokingId).Count();
           var settingForTrip = await _context.AppSettings.FirstOrDefaultAsync();
            ViewData["settingForTrip"] = settingForTrip;
              TempData["error"] = error;
            ViewData["TripBoking"] = TripBoking;
             
            var applicationDbContext = _context.TripDelegates.Where(d=>d.TripBookingId == bokingId).Include(t => t.TripBooking);
            return View(await applicationDbContext.ToListAsync());
        }
      
        public async Task<int> countbuses(int bokingId)
        {

            int countbus=0;
            var students = await _context.StudentsParticipatingInTrip.Where(s => s.TripBookingId == bokingId).CountAsync();
            var boking = await _context.TripBookings.FindAsync(bokingId);
            var EducationalBody = await _context.SchedulingTripDetails.Include(c => c.EducationalBody).FirstOrDefaultAsync(e=>e.Id==boking.SchedulingTripDetailId); ;
            var b= EducationalBody.EducationalBody.EntityType;
            int typeEdu = b == "الكليات" ? 1 : 2;
            if (students < 60 && students >= 30)
            {
                countbus = 1 * typeEdu;
                return countbus;
            }
            else if (students >= 60 && students <= 100)
            {
                 countbus = 2 * typeEdu;
                return countbus;
            }
            else
            {
              
                while (students >= 50)
                {
                    countbus++;
                    students -= 50;
                }

                if (students < 50 && students >= 30)
                {
                    countbus++;
                }
                return countbus * typeEdu;
            }

          
        }

        public async Task<int> countbusesForAmada(int bokingId)
        {

            int countbus = 0;
            var students = await _context.StudentsParticipatingInTrip.Where(s => s.TripBookingId == bokingId).CountAsync();
   
            if (students < 60 && students >= 30)
            {
                countbus = 1 ;
                return countbus;
            }
            else if (students >= 60 && students <= 100)
            {
                countbus = 2 ;
                return countbus;
            }
            else
            {

                while (students >= 50)
                {
                    countbus++;
                    students -= 50;
                }

                if (students < 50 && students >= 30)
                {
                    countbus++;
                }
                return countbus ;
            }


        }

        // GET: Sociologist/TripDelegates/Create

        public async Task<IActionResult> Create(int bokingId )
        {
            if (!TripBookingExists(bokingId))
            {
                Response.StatusCode = 404;
                return View("TripDelegatesNotFound");
            }
            var FromEducationBody = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId && c.IsFromEducationBody == true).CountAsync();
            var FromAmada = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId && c.IsFromEducationBody == false).CountAsync();
            var jha = countbuses(bokingId).Result;
            var amada = countbusesForAmada(bokingId).Result;
            if (User.IsInRole("Admin")) {
                if ((FromEducationBody + FromAmada) >= (jha + amada))
                {
                    string CanNotAdd = "لايمكن اضافة منتدبين آخرين!! ";
                    return RedirectToAction(nameof(Index), new { bokingId = bokingId, error = CanNotAdd });
                }
                ViewData["countbus"] = (jha + amada) - (FromEducationBody + FromAmada);
            }
            else if (User.IsInRole("Supervisor")) {
                if (FromEducationBody  >= jha )
                {
                    string CanNotAdd = "لايمكن اضافة منتدبين آخرين!! ";
                    return RedirectToAction(nameof(Index), new { bokingId = bokingId, error = CanNotAdd });
                }
                var edBody = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId && c.IsFromEducationBody == true).CountAsync();
                ViewData["countbus"] = (jha - edBody) ;
            }
            else if (User.IsInRole("DirectorOfSocialActivity") || User.IsInRole("SocialActivityOfficer")) {
                if (FromAmada >=  amada )
                {
                    string CanNotAdd = "لايمكن اضافة منتدبين آخرين!! ";
                    return RedirectToAction(nameof(Index), new { bokingId = bokingId, error = CanNotAdd });
                }
                var edBody = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId && c.IsFromEducationBody == false).CountAsync();
                ViewData["countbus"] = (amada - edBody);
            
            }
            else { ViewData["countbus"] = 0; }
          
          
            
            ViewData["TripBookingId"] = bokingId;
            return View();
        }
        
        // POST: Sociologist/TripDelegates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bokingId ,List<TripDelegate> tripDelegate)
        {


            if (ModelState.IsValid)
            {
                List<int> ii = tripDelegate.Select(s => s.EmployeeNumber).ToList();
                if (studentsCeck(ii) > 0)
                {
                    TempData["message"] = "رقم الموظف مكرر :" + studentsCeck(ii);
                }
                else if(studentsCeckInDb(tripDelegate, bokingId) > 0)
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

            var check = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId).CountAsync();
            var jha = countbuses(bokingId).Result;
            var amada = countbusesForAmada(bokingId).Result;
            if (User.IsInRole("Admin"))
            {
                if (check > (jha + amada))
                {
                    string CanNotAdd = "لايمكن اضافة منتدبين آخرين!! ";
                    return RedirectToAction(nameof(Index), new { bokingId = bokingId, error = CanNotAdd });
                }
                ViewData["countbus"] = (jha + amada) - check;
            }
            else if (User.IsInRole("Supervisor"))
            {
                if (check > (jha - amada))
                {
                    string CanNotAdd = "لايمكن اضافة منتدبين آخرين!! ";
                    return RedirectToAction(nameof(Index), new { bokingId = bokingId, error = CanNotAdd });
                }
                var edBody = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId && c.IsFromEducationBody == true).CountAsync();
                ViewData["countbus"] = (jha - edBody);
            }
            else if (User.IsInRole("DirectorOfSocialActivity") || User.IsInRole("SocialActivityOfficer"))
            {
                if (check > (jha - amada))
                {
                    string CanNotAdd = "لايمكن اضافة منتدبين آخرين!! ";
                    return RedirectToAction(nameof(Index), new { bokingId = bokingId, error = CanNotAdd });
                }
                var edBody = await _context.TripDelegates.Where(c => c.TripBookingId == bokingId && c.IsFromEducationBody == false).CountAsync();
                ViewData["countbus"] = (amada - edBody);

            }
            else { ViewData["countbus"] = 0; }


            ViewData["TripBookingId"] = bokingId;
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
