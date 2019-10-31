using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using ReflectionIT.Mvc.Paging;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class StudentsParticipatingInTripsController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public StudentsParticipatingInTripsController(ApplicationDbContext context)
        {
            _context = context;
         
    }
        //[HttpPost]
   
        [HttpGet()]
        [Route("Sociologist/StudentsParticipatingInTrips/Index/{bokingId:int}")]
        public async Task<IActionResult> Index(int bokingId)
        {
            if (!TripBooking(bokingId))
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }
            var TripBoking= await _context.TripBookings.FirstOrDefaultAsync(b => b.Id == bokingId);
         
            ViewData["TripBoking"] = TripBoking;
            var items = _context.StudentsParticipatingInTrip.Where(t => t.TripBookingId == bokingId).AsNoTracking().OrderBy(p=>p.Id);

            QtyStudens(bokingId, out int Required, out int Registered, out int Remainder);

            ViewData["Required"] = Required;
            ViewData["Registered"] = Registered;
            ViewData["Remainder"] = Remainder;
            return View(await items.ToListAsync());
        }

        public IActionResult Adapter(int bokingId, int count)
        {
         
            return RedirectToAction(nameof(Create),new { bokingId= bokingId, count = count });
        }

        [HttpGet(),Route("Sociologist/StudentsParticipatingInTrips/Create/{bokingId:int}/{count:int}")]
        public IActionResult Create(int bokingId,int count)
        {

            if (!TripBooking(bokingId))
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }


            QtyStudens(bokingId, out int Required, out int Registered, out int Remainder);

            ViewData["Required"] = Required;
            ViewData["Registered"] = Registered;
            ViewData["Remainder"] = Remainder;
            if(Remainder > count)
            {
                ViewData["StudentCount"] = count;

            }
            else
            {
                ViewData["StudentCount"] = Remainder;

            }
            ViewData["TripBookingId"] = bokingId;
            return View();
        }

    

        // POST: Sociologist/StudentsParticipatingInTrips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ Route("Sociologist/StudentsParticipatingInTrips/Create/{bokingId:int}/{count:int}")]



        public async Task<IActionResult> Create(int bokingId, int count,IList<StudentsParticipatingInTrip> studentsParticipatingInTrip)
        {
            if (!TripBooking(bokingId))
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }
  
            if (ModelState.IsValid)
            {
               
                List<int> ii = studentsParticipatingInTrip.Select(s=>s.StudentNumber).ToList();
              if (studentsCeck(ii) > 0)
                {
                    ViewData["StudentCount"] = count;
                    ViewData["TripBookingId"] = bokingId;
                    TempData["message"] = "رقم الطالب مكرر :"+ studentsCeck(ii);
                    return View(studentsParticipatingInTrip);
                }
                foreach (var student in studentsParticipatingInTrip)
                {
                  
                    var status = await _context.StudentsParticipatingInTrip.Where(s => s.StudentNumber == student.StudentNumber && s.TripBookingId == student.TripBookingId).ToListAsync();
                    if (status.Count() > 0)
                    {
                        ViewData["StudentCount"] = count;
                        ViewData["TripBookingId"] = bokingId;
                        TempData["message"] = " رقم الطالب ("+ student.StudentNumber + ") موجود مسبقا في نفس الرحلة !! ";
                        return View(studentsParticipatingInTrip);
                    }
                }
                _context.AddRange(studentsParticipatingInTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { bokingId = bokingId });
            }
            ViewData["StudentCount"] = count;
            ViewData["TripBookingId"] = bokingId;
            return View(studentsParticipatingInTrip);
        }

        // GET: Sociologist/StudentsParticipatingInTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }

          

            var studentsParticipatingInTrip = await _context.StudentsParticipatingInTrip.FindAsync(id);
            if (studentsParticipatingInTrip == null)
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }
            ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName", studentsParticipatingInTrip.TripBookingId);
            return View(studentsParticipatingInTrip);
        }

        // POST: Sociologist/StudentsParticipatingInTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentNumber,StudentName,TripBookingId")] StudentsParticipatingInTrip studentsParticipatingInTrip)
        {

            if (id != studentsParticipatingInTrip.Id)
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }

            
           
           
                if (ModelState.IsValid)
                {
                if (StudentsParticipatingInTripExists( id, studentsParticipatingInTrip.StudentNumber, studentsParticipatingInTrip.TripBookingId))
                {
                    ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName", studentsParticipatingInTrip.TripBookingId);
                    TempData["message"] = " رقم الطالب (" + studentsParticipatingInTrip.StudentNumber + ") موجود مسبقا في نفس الرحلة !! ";
                    return View(studentsParticipatingInTrip);
                }

                _context.Update(studentsParticipatingInTrip);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { bokingId = studentsParticipatingInTrip.TripBookingId });

                }

            

          
            
         
            return View(studentsParticipatingInTrip);
        }

        // GET: Sociologist/StudentsParticipatingInTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }

            var studentsParticipatingInTrip = await _context.StudentsParticipatingInTrip
                .Include(s => s.TripBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentsParticipatingInTrip == null)
            {
                Response.StatusCode = 404;
                return View("StudentsNotFound");
            }

            return View(studentsParticipatingInTrip);
        }

        // POST: Sociologist/StudentsParticipatingInTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentsParticipatingInTrip = await _context.StudentsParticipatingInTrip.FindAsync(id);
            _context.StudentsParticipatingInTrip.Remove(studentsParticipatingInTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) ,new { bokingId = studentsParticipatingInTrip.TripBookingId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMulti(IEnumerable<int> checkForDelete)
        {
            var studentsParticipatingInTrip = await _context.StudentsParticipatingInTrip
                .Where(e => checkForDelete.Contains(e.Id)).ToListAsync();
            int BookingId = studentsParticipatingInTrip.FirstOrDefault().TripBookingId;
            _context.StudentsParticipatingInTrip.RemoveRange(studentsParticipatingInTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { bokingId = BookingId });
        }
       

        private bool StudentsParticipatingInTripExists(int id)
        {
            return _context.StudentsParticipatingInTrip.Any(e => e.Id == id);
        }

        private bool StudentsParticipatingInTripExists(int id, int studentnumber, int bokingId)
        {
            return _context.StudentsParticipatingInTrip.Any(e => e.Id != id && e.TripBookingId == bokingId && e.StudentNumber == studentnumber);
        }

        private int studentcheck(int id ,int studentnumber , int bokingId)
        {
            var status =  _context.StudentsParticipatingInTrip.FindAsync(id);
            if (status.Result.StudentNumber != studentnumber)
            {
                var studentcheck =  _context.StudentsParticipatingInTrip.Where(b => b.TripBookingId == bokingId && b.StudentNumber == studentnumber).ToListAsync();
                return studentcheck.Result.Count();
            }
            return 0;
        }

   

    private bool TripBooking(int id)
        {
            return _context.TripBookings.Any(e => e.Id == id);
        }

        private int studentsCeck(List<int> studentsNumber)
        {
            for (int i = 0; i < studentsNumber.Count(); i++)
            {
                for (int j = i + 1; j < studentsNumber.Count(); j++)
                {
                    if (studentsNumber[i] == studentsNumber[j] && i != j)
                    {
                        return studentsNumber[i];
                    }
                }
            }
            return 0;
        }



        private void QtyStudens(int bokingId, out int Required, out int Registered, out int Remainder)
        {
            var booking = _context.TripBookings
                   .Include(s => s.StudentsParticipatingInTrips)
                   .Include(e => e.SchedulingTripDetail.EducationalBody)
                   .FirstOrDefaultAsync(i => i.Id == bokingId);

            var boking = booking.Result;
            var appSettings = _context.AppSettings.FirstOrDefaultAsync();
            var appSetting = appSettings.Result;

            if (boking.TripTypeName == "عمرة")
            {

                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    Required = appSetting.QtyUmrahBuses * 46;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
                else
                {
                    Required = appSetting.QtyUmrahBuses * 47;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
            }
            else if (boking.TripTypeName == "داخلية")
            {
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    Required = appSetting.QtyIntirnalBuses * 47;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
                else
                {
                    Required = appSetting.QtyIntirnalBuses * 48;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
            }
            else if (boking.TripTypeName == "خارجية")
            {
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    Required = appSetting.QtyExtirnalBuses * 46;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
                else
                {
                    Required = appSetting.QtyExtirnalBuses * 47;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
            }
            else if (boking.TripTypeName == "زيارة داخلية")
            {
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    Required = appSetting.QtyVisitIntirnalBuses * 47;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
                else
                {
                    Required = appSetting.QtyVisitIntirnalBuses * 48;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
            }
            else if (boking.TripTypeName == "زيارة خارجية")
            {
                if (boking.SchedulingTripDetail.EducationalBody.EntityType == "المعاهد والدور")
                {
                    Required = appSetting.QtyExtirnalBuses * 46;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
                else
                {
                    Required = appSetting.QtyExtirnalBuses * 47;
                    Registered = boking.StudentsParticipatingInTrips.Count();
                    Remainder = Required - Registered;
                }
            }
            else
            {
                Required = 0;
                Registered = 0;
                Remainder = 0;
            }

        }



    }
}
