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

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class LetterDelegationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LetterDelegationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/LetterDelegations
        [Route("Sociologist/LetterDelegations/Create/{bokingId:int}")]
        public async Task<IActionResult> Create(int bokingId)
        {
            var boking = await _context.TripBookings
                .Include(d=>d.TripDelegates)
                .Include(e=>e.SchedulingTripDetail.EducationalBody)
                .Include(t=>t.SchedulingTripDetail.TripType)
                .Include(c=>c.City)
                .FirstOrDefaultAsync(b => b.Id == bokingId);
            int duration = boking.TripQtyDays;
            if (boking.SchedulingTripDetail.TripType.Name == "عمرة")
            {
                duration += 2;
            }
            var delgations = await _context.TripDelegates.Where(e => e.TripBookingId == bokingId).ToListAsync();
            var model = new List<LetterDelegationCreateModelView>();
            foreach (var del in delgations)
            {
                var emps = new LetterDelegationCreateModelView()
                {
                    EducationBody = boking.SchedulingTripDetail.EducationalBody.Name,
                    Duration = duration,
                    TripType = boking.SchedulingTripDetail.TripType.Name,
                    FromCity = boking.SchedulingTripDetail.EducationalBody.City,
                    ToCityName = boking.City.LocationName,
                    TripDate = boking.SchedulingTripDetail.TripDate,
                    BokingId = boking.Id,
                    EmployeeName=del.EmployeeName,EmployeeNumber=del.EmployeeNumber,
                    JopName=del.JopName,Confirmed=del.Confirmed,
                    Id=del.Id
                };
                model.Add(emps);
            }
        

            return View(model);
        }

        [Route("Sociologist/LetterDelegations/Create/{bokingId:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bokingId, List<LetterDelegationCreateModelView> employes)
        {
            if (ModelState.IsValid)
            {
                
                var letter = new Letter() { TripBookingId = employes[0].BokingId, TypeLetter = 3 };
                _context.Letters.Add(letter);
                await _context.SaveChangesAsync();

                var modelToSave = new List<TripDelegate>();

                foreach (var del in employes)
                {

                    var delForupdate = await _context.TripDelegates.FindAsync(del.Id);
                    if (delForupdate != null)
                    {
                        delForupdate.Confirmed = del.Confirmed;
                        modelToSave.Add(delForupdate);
                    }

                };
                _context.UpdateRange(modelToSave);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexByBoking", "Letters", new { bokingId = bokingId });
            }



            var boking = await _context.TripBookings
             .Include(d => d.TripDelegates)
             .Include(e => e.SchedulingTripDetail.EducationalBody)
             .Include(t => t.SchedulingTripDetail.TripType)
             .Include(c => c.City)
             .FirstOrDefaultAsync(b => b.Id == bokingId);
            int duration = boking.TripQtyDays;
            if (boking.SchedulingTripDetail.TripType.Name == "عمرة")
            {
                duration += 2;
            }
            var delgations = await _context.TripDelegates.Where(e => e.TripBookingId == bokingId).ToListAsync();
            var model = new List<LetterDelegationCreateModelView>();
            foreach (var del in delgations)
            {
                var emps = new LetterDelegationCreateModelView()
                {
                    EducationBody = boking.SchedulingTripDetail.EducationalBody.Name,
                    Duration = duration,
                    TripType = boking.SchedulingTripDetail.TripType.Name,
                    FromCity = boking.SchedulingTripDetail.EducationalBody.City,
                    ToCityName = boking.City.LocationName,
                    TripDate = boking.SchedulingTripDetail.TripDate,
                    BokingId = boking.Id,
                    EmployeeName = del.EmployeeName,
                    EmployeeNumber = del.EmployeeNumber,
                    JopName = del.JopName,
                    Confirmed = del.Confirmed,
                    Id = del.Id
                };
                model.Add(emps);
            }


            return View(model);
        }
        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.Id == id);
        }


        [Route("Sociologist/LetterDelegations/Details/{letterId:int}")]
        public async Task<IActionResult> Details(int letterId)
        {

            if (!LetterExists(letterId))
            {
                Response.StatusCode = 404;
                return View("LetterFoodsNotFound");
            }
            var letter = await _context.Letters
                .Include(b => b.TripBooking)
                .Include(b => b.TripBooking.TripDelegates)
                .Include(b => b.TripBooking.City)
                .Include(b => b.LetteSignutres)
                .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.StudentsParticipatingInTrips)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
                .FirstOrDefaultAsync(l => l.Id == letterId);
          //  ViewBag.signutre = await _context.Signatures.Include(d => d.SignutreDelegates).ToListAsync();
            return View(letter);
        }


        [Route("Sociologist/LetterDelegations/Print/{letterId:int}")]

        public async Task<IActionResult> Print(int letterId)
        {
            if (!LetterExists(letterId))
            {
                Response.StatusCode = 404;
                return View("LetterFoodsNotFound");
            }
            var letter = await _context.Letters
                .Include(b => b.TripBooking)
                .Include(b => b.TripBooking.TripDelegates)
                .Include(b => b.TripBooking.City)
                .Include(b => b.LetteSignutres)
                .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.StudentsParticipatingInTrips)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
                .FirstOrDefaultAsync(l => l.Id == letterId);
            int duration = letter.TripBooking.TripQtyDays;
            string tripType = letter.TripBooking.SchedulingTripDetail.TripType.Name;
            string day = "يوم";
            if (tripType == "عمرة")
            {
                duration += 2;
            }
            if (duration == 2 ) { day = "يومين"; }else if (duration > 2) { day = "أيام"; }
            var print = new PrintLetterDelegationsModelView()
            {
                Employees=await _context.TripDelegates.Where(e=>e.Confirmed == true).ToListAsync(),
                FromCity=letter.TripBooking.SchedulingTripDetail.EducationalBody.City,
                ToCityName=letter.TripBooking.City.LocationName,
                Duration= duration,
                TripType= tripType,
                Day= day,
                EducationBody =letter.TripBooking.SchedulingTripDetail.EducationalBody.Name,
                WhoHasSignutre =letter.LetteSignutres,
                TripDate=letter.TripBooking.SchedulingTripDetail.TripDate,
               // Signatures=await _context.Signatures.Include(d=>d.SignutreDelegates).Include(u=>u.User).ToListAsync()
            };
            return View(print);

        }

    }
}
