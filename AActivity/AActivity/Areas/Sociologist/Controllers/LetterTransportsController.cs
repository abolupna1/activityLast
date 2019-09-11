using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using AActivity.Areas.Sociologist.ModelViews;
using System.Globalization;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class LetterTransportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LetterTransportsController(ApplicationDbContext context)
        {
            _context = context;
        }



       
        [Route("Sociologist/LetterTransports/Details/{letterId:int}")]
        public async Task<IActionResult> Details(int letterId)
        {

            if (!LetterExists(letterId))
            {
                Response.StatusCode = 404;
                return View("LetterTransportsNotFound");
            }
            var letter = await _context.Letters
                .Include(t => t.LetterTransports).ThenInclude(b => b.User)
                .Include(b => b.TripBooking)
                .Include(b => b.LetteSignutres)
                .Include(t=>t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t=>t.TripBooking.StudentsParticipatingInTrips)
                .Include(t=>t.TripBooking.SchedulingTripDetail.EducationalBody)

                .FirstOrDefaultAsync(l=>l.Id== letterId);
            ViewBag.signutre = await _context.Signatures.Include(d=>d.SignutreDelegates).ToListAsync();
            return View(letter);
        }
        public async Task<int> countbuses(int bokingId)
        {

            int countbus = 0;
            var students = await _context.StudentsParticipatingInTrip.Where(s => s.TripBookingId == bokingId).CountAsync();
            var boking = await _context.TripBookings.FindAsync(bokingId);
            var EducationalBody = await _context.SchedulingTripDetails.Include(c => c.EducationalBody).FirstOrDefaultAsync(e => e.Id == boking.SchedulingTripDetailId); ;
            var b = EducationalBody.EducationalBody.EntityType;
            int typeEdu = b == "الكليات" ? 1 : 2;
            if (students < 60 && students >= 30)
            {
                countbus = 1 * typeEdu;
                return countbus;
            }
            else if (students >= 60 && students <= 98)
            {
                countbus = 2 * typeEdu;
                return countbus;
            }
            else
            {

                while (students >= 48)
                {
                    countbus++;
                    students -= 48;
                }

                if (students < 48 && students >= 30)
                {
                    countbus++;
                }
                return countbus * typeEdu;
            }


        }
        // GET: Sociologist/LetterTransports/Create
        [Route("Sociologist/LetterTransports/Create/{bokingId:int}")]
        public async Task<IActionResult> Create(int bokingId)
        {
            var boking = await _context.TripBookings
                .Include(u => u.StudentsParticipatingInTrips)
                .Include(t => t.SchedulingTripDetail.TripType)
                .Include(u => u.SchedulingTripDetail.EducationalBody.User)
                .Include(e => e.SchedulingTripDetail.EducationalBody)
                .FirstOrDefaultAsync(b => b.Id == bokingId);
            if (boking == null)
            {
                Response.StatusCode = 404;
                return View("LetterTransportsNotFound");
            }

            var transport = new LetterTRansportCreateModelView()
            {
                BokingId = bokingId,
                QtyStudents= boking.StudentsParticipatingInTrips.Count(),
                UserId= boking.SchedulingTripDetail.EducationalBody.User.Id,
                FullName = boking.SchedulingTripDetail.EducationalBody.User.FullName,
                Mobile= boking.SchedulingTripDetail.EducationalBody.User.PhoneNumber,
                TripType=boking.SchedulingTripDetail.TripType.Name,
                EducationBody= boking.SchedulingTripDetail.EducationalBody.Name,
               QtyBuses= countbuses(bokingId).Result

            };
            return View(transport);
        }

        // POST: Sociologist/LetterTransports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/LetterTransports/Create/{bokingId:int}")]
        public async Task<IActionResult> Create(int bokingId ,LetterTRansportCreateModelView trans)
        {
            var boking = await _context.TripBookings
            .Include(u => u.StudentsParticipatingInTrips)
            .Include(t => t.SchedulingTripDetail.TripType)
            .Include(u => u.SchedulingTripDetail.EducationalBody.User)
            .Include(e => e.SchedulingTripDetail.EducationalBody)
            .FirstOrDefaultAsync(b => b.Id == bokingId);
            if (boking == null)
            {
                Response.StatusCode = 404;
                return View("LetterTransportsNotFound");
            }
            if (ModelState.IsValid)
            {
                var letter = new Letter() { TripBookingId=trans.BokingId,TypeLetter=1};
                _context.Letters.Add(letter);
                await _context.SaveChangesAsync();
                var letterTrans = new LetterTransport() { LetterId = letter.Id, UserId = trans.UserId, QtyStudents = trans.QtyStudents ,QtyBuses=trans.QtyBuses};
                _context.LetterTransports.Add(letterTrans);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexByBoking", "Letters", new { bokingId = bokingId });
            }
        

            var transport = new LetterTRansportCreateModelView()
            {
                BokingId = bokingId,
                QtyStudents = boking.StudentsParticipatingInTrips.Count(),
                UserId = boking.SchedulingTripDetail.EducationalBody.User.Id,
                FullName = boking.SchedulingTripDetail.EducationalBody.User.FullName,
                Mobile = boking.SchedulingTripDetail.EducationalBody.User.PhoneNumber,
                TripType = boking.SchedulingTripDetail.TripType.Name,
                EducationBody = boking.SchedulingTripDetail.EducationalBody.Name,
                QtyBuses = countbuses(bokingId).Result

            };
            return View(trans);
        }

        [Route("Sociologist/LetterTransports/Print/{LetterId:int}")]
        public async Task<IActionResult> Print(int LetterId)
        {
            if (!LetterExists(LetterId))
            {
                Response.StatusCode = 404;
                return View("LetterTransportsNotFound");
            }
            var letter = await _context.Letters
                .Include(t => t.LetterTransports).ThenInclude(b => b.User)
                .Include(t => t.LetterTransports).ThenInclude(b => b.User.Signatures)
                .Include(b => b.TripBooking)
                .Include(b => b.TripBooking.City)
                .Include(b => b.LetteSignutres)
                .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.StudentsParticipatingInTrips)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)

                .FirstOrDefaultAsync(l => l.Id == LetterId);
            var letterView = new PrintLetterTransportModelView()
            {
                TripType=letter.TripBooking.SchedulingTripDetail.TripType.Name,
                EducationBody=letter.TripBooking.SchedulingTripDetail.EducationalBody.Name,
                TripSupervisor=letter.LetterTransports.FirstOrDefault().User.FullName,
                TripSupervisorMobile= letter.LetterTransports.FirstOrDefault().User.PhoneNumber,
                QtyStudents=letter.TripBooking.StudentsParticipatingInTrips.Count(),
                TripDate=letter.TripBooking.SchedulingTripDetail.TripDate,
                TripToCity=letter.TripBooking.City.LocationName,
                WhoHasSignutre=letter.LetteSignutres,
                QtyBuses=letter.LetterTransports.FirstOrDefault().QtyBuses,
                cultureInfo = new CultureInfo("ar-Sa"),
                Signatures=await _context.Signatures.Include(u=>u.User).Where(s=>s.Status == true).ToListAsync()


        };
           
            return View(letterView);
        }
      

        // POST: Sociologist/LetterTransports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var letterTransport = await _context.LetterTransports.FindAsync(id);
            _context.LetterTransports.Remove(letterTransport);
            await _context.SaveChangesAsync();
            return RedirectToAction();
        }

 

        private bool LetterTransportExists(int id)
        {
            return _context.LetterTransports.Any(e => e.Id == id);
        }

        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.Id == id);
        }
    }
}
