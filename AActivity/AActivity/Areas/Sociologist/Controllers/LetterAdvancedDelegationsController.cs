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
    public class LetterAdvancedDelegationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LetterAdvancedDelegationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        
        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.Id == id);
        }

        [Route("Sociologist/LetterAdvancedDelegations/Print/{LetterAdvancedId:int}")]
        public async Task<IActionResult> Print(int LetterAdvancedId)
        {
            
            return View(await getForPrint( LetterAdvancedId));
        }


        [Route("Sociologist/LetterAdvancedDelegations/PrintAll/{LetterId:int}")]
        public async Task<IActionResult> PrintAll(int LetterId)
        {
            var model = new List<PrintLetterAdvancedDelegation>();
            var letterAdvance = await _context.LetterAdvancedDelegations.Where(l => l.LetterId == LetterId).ToListAsync();
            foreach (var ad in letterAdvance)
            {
                model.Add(await getForPrint(ad.Id));  
            }
            return View(model);
        }

        private async Task<PrintLetterAdvancedDelegation> getForPrint(int LetterAdvancedId)
        {
            var adv = await _context.LetterAdvancedDelegations
                .Include(l => l.Letter)
                .Include(l => l.LetterSignutreForAdvances)
                .Include(l => l.Letter.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(l => l.Letter.TripBooking.SchedulingTripDetail)
                .Include(l => l.Letter.TripBooking.SchedulingTripDetail.TripType)
                .Include(l => l.Letter.TripBooking.City)
                .Include(l => l.Letter.TripBooking)
                .FirstOrDefaultAsync(i=>i.Id == LetterAdvancedId);
            var print = new PrintLetterAdvancedDelegation
            {
                AdvanceForEmp=adv.CreditToEMployee,
                Amount=adv.Amount + adv.AmountAdditional,
                EduName=adv.Letter.TripBooking.SchedulingTripDetail.EducationalBody.Name,
                EmpMobile=adv.EmployeeMobile,
                QtyStudent=adv.QtyStudents,
                TripDate=adv.Letter.TripBooking.SchedulingTripDetail.TripDate,
                TripTo=adv.Letter.TripBooking.City.LocationName,
                TripType=adv.Letter.TripBooking.SchedulingTripDetail.TripType.Name,
                WhoHasSignutre=adv.LetterSignutreForAdvances.ToList(),
                Signatures=await _context.Signatures.Include(u=>u.User).ToListAsync()
            };

            return print;
        }

        [Route("Sociologist/LetterAdvancedDelegations/Details/{letterId:int}")]
        public async Task<IActionResult> Details(int letterId)
        {

            if (!LetterExists(letterId))
            {
                Response.StatusCode = 404;
                return View("LetterFoodsNotFound");
            }
            var letter = await _context.Letters
                .Include(e => e.TripBooking)
                .Include(e => e.TripBooking.SchedulingTripDetail)
                .Include(e => e.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(e => e.TripBooking.SchedulingTripDetail.TripType)
                .Include(a=>a.LetterAdvancedDelegations)
                .ThenInclude(l=>l.LetterSignutreForAdvances)
                .FirstOrDefaultAsync(l => l.Id == letterId);
           // ViewBag.signutre = await _context.Signatures.Include(d => d.SignutreDelegates).ToListAsync();
            return View(letter);
        }
        [Route("Sociologist/LetterAdvancedDelegations/IndexByBokingId/{bokingId:int}")]
        public async Task<IActionResult> IndexByBokingId(int bokingId)
        {
            var boking = await _context.TripBookings
                .Include(e=>e.SchedulingTripDetail.EducationalBody.User)
                .Include(e=>e.TripDelegates)
                .Include(e=>e.Letters).ThenInclude(t=>t.LetterAdvancedDelegations)
                .FirstOrDefaultAsync(b=>b.Id == bokingId);
            if (boking == null)
            {
                return NotFound();
            }
            var checkletter = await _context.Letters.AnyAsync(b=>b.TripBookingId== bokingId && b.TypeLetter ==4);
            if (!checkletter)
            {
                var letter = new Letter() { TripBookingId = bokingId, TypeLetter = 4 };
                _context.Letters.Add(letter);
                await _context.SaveChangesAsync();
            }
      
            return View(boking);
        }

     
        [Route("Sociologist/LetterAdvancedDelegations/Create/{letterId:int}/{delegateId:int}")]
        public async Task<IActionResult> Create(int letterId,int delegateId)
        {
           
            return View(getDataForCreate( letterId,  delegateId).Result);
        }

        private float TripType(string tripType)
        {
            float amount;
            var setting = _context.AppSettings.FirstOrDefaultAsync().Result;
            switch (tripType)
            {
                case "عمرة":
                    amount = setting.AmountOmrahCreditToTrip;
                    break;
                case "داخلية":
                    amount = setting.AmountInternalCreditToTrip;
                    break;
                case "خارجية":
                    amount = setting.AmountExternalCreditToTrip;
                    break;
                case "زيارة":
                    amount = setting.AmountVisitCreditToTrip;
                    break;
                default:
                    amount = 0;
                    break;
            }
            return amount;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/LetterAdvancedDelegations/Create/{letterId:int}/{delegateId:int}")]
        public async Task<IActionResult> Create(int letterId, int delegateId,
            LetterAdvanceCreateModelView letter)
        {
            if (ModelState.IsValid)
            {
                var advanced = new LetterAdvancedDelegation()
                {
                    Amount =letter.Amount,
                    AmountAdditional=letter.AmountAdditional,
                    CreditToEMployee=letter.CreditToEMployee,
                    EmployeeMobile=letter.EmployeeMobile,
                    LetterId=letter.LetterId,
                    QtyStudents=letter.QtyStudents,
                    EmployeeNomber=letter.EmployeeNomber
                };
                _context.Add(advanced);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexByBokingId),new { bokingId=letter.bokingId });
            }

            return View(getDataForCreate( letterId,  delegateId).Result);
       
        }

        private async Task<LetterAdvanceCreateModelView> getDataForCreate(int letterId, int delegateId)
        {
            var letter = await _context.Letters
               .Include(b => b.TripBooking)
               .Include(b => b.TripBooking.SchedulingTripDetail.TripType)
               .Include(b => b.TripBooking.StudentsParticipatingInTrips)
               .Include(b => b.TripBooking.TripDelegates)
               .Include(b => b.TripBooking.SchedulingTripDetail.EducationalBody.User)
               .Include(b => b.TripBooking.SchedulingTripDetail.EducationalBody)
               .Include(b => b.TripBooking.SchedulingTripDetail.TripType)
               .FirstOrDefaultAsync(l => l.Id == letterId);
            string empName;
            string empMobile;
            string EmployeeNomber;
            if (delegateId == 0)
            {
                empName = letter.TripBooking.SchedulingTripDetail.EducationalBody.User.FullName;
                empMobile =letter.TripBooking.SchedulingTripDetail.EducationalBody.User.PhoneNumber;
                EmployeeNomber= letter.TripBooking.SchedulingTripDetail.EducationalBody.User.UserName;
            }
            else
            {
                empName = letter.TripBooking.TripDelegates.FirstOrDefault(e => e.Id == delegateId).EmployeeName;
                empMobile = letter.TripBooking.TripDelegates.FirstOrDefault(e => e.Id == delegateId).EmployeMobile;
                EmployeeNomber = letter.TripBooking.TripDelegates.FirstOrDefault(e => e.Id == delegateId).EmployeeNumber.ToString();
            }
            var advanc = new LetterAdvanceCreateModelView()
            {
                Amount = TripType(letter.TripBooking.SchedulingTripDetail.TripType.Name),
                QtyStudents = letter.TripBooking.StudentsParticipatingInTrips.Count(),
                CreditToEMployee = empName,
                EmployeeMobile = empMobile.ToString(),
                LetterId = letter.Id,
                TripType = letter.TripBooking.SchedulingTripDetail.TripType.Name,
                EducationName = letter.TripBooking.SchedulingTripDetail.EducationalBody.Name,
                TripLocationName = letter.TripBooking.TripLocationName,
                TripDate = letter.TripBooking.SchedulingTripDetail.TripDate,
                bokingId=letter.TripBookingId,
                EmployeeNomber= EmployeeNomber

            };
            return advanc;
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/LetterAdvancedDelegations/Delete")]
        public async Task<IActionResult> Delete(int letterId,string EmployeeNumber)
        {

            var letterAdvancedDelegation = await _context.LetterAdvancedDelegations
                .Include(l=>l.Letter)
                .FirstOrDefaultAsync(d=>d.LetterId== letterId && d.EmployeeNomber == EmployeeNumber);
            _context.LetterAdvancedDelegations.Remove(letterAdvancedDelegation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexByBokingId),new { bokingId = letterAdvancedDelegation.Letter.TripBookingId });
        }

        private bool LetterAdvancedDelegationExists(int id)
        {
            return _context.LetterAdvancedDelegations.Any(e => e.Id == id);
        }
    }
}
