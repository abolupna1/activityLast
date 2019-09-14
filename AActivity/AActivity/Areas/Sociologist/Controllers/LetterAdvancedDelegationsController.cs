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

        [Route("Sociologist/LetterAdvancedDelegations/IndexByBokingId/{bokingId:int}")]
        public async Task<IActionResult> IndexByBokingId(int bokingId)
        {
            var boking = await _context.TripBookings
                .Include(e=>e.SchedulingTripDetail.EducationalBody.User)
                .Include(e=>e.TripDelegates)
                .Include(e=>e.Letters)
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
        // GET: Sociologist/LetterAdvancedDelegations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LetterAdvancedDelegations.Include(l => l.Letter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/LetterAdvancedDelegations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letterAdvancedDelegation = await _context.LetterAdvancedDelegations
                .Include(l => l.Letter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letterAdvancedDelegation == null)
            {
                return NotFound();
            }

            return View(letterAdvancedDelegation);
        }

        // GET: Sociologist/LetterAdvancedDelegations/Create
        [Route("Sociologist/LetterAdvancedDelegations/Create/{letterId:int}/{delegateId:int}")]

        public async Task<IActionResult> Create(int letterId,int delegateId)
        {
            var letter = await _context.Letters
                .Include(b => b.TripBooking.SchedulingTripDetail.TripType)
                .Include(b => b.TripBooking.StudentsParticipatingInTrips)
                .Include(b => b.TripBooking.TripDelegates)
                .Include(b => b.TripBooking.SchedulingTripDetail.EducationalBody.User)
                .FirstOrDefaultAsync(l=>l.Id== letterId);
            string empName;
            int empMobile;
            if (delegateId==0)
            {
                empName = letter.TripBooking.SchedulingTripDetail.EducationalBody.User.FullName;
                empMobile =Int32.Parse(letter.TripBooking.SchedulingTripDetail.EducationalBody.User.PhoneNumber);

            }
            else
            {
                empName = letter.TripBooking.TripDelegates.FirstOrDefault(e=>e.Id== delegateId).EmployeeName;
                empMobile =letter.TripBooking.TripDelegates.FirstOrDefault(e => e.Id == delegateId).EmployeeNumber;
            }
            var advanc = new LetterAdvanceCreateModelView()
            {
                Amount = TripType(letter.TripBooking.SchedulingTripDetail.TripType.Name),
                QtyStudents=letter.TripBooking.StudentsParticipatingInTrips.Count(),
                CreditToEMployee= empName,
                EmployeeMobile= empMobile.ToString(),
                LetterId=letter.Id

            };
            return View(advanc);
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

        // POST: Sociologist/LetterAdvancedDelegations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LetterId,Amount,AmountAdditional,QtyStudents,CreditToEMployee,EmployeeMobile,Statatus,Notes")] LetterAdvancedDelegation letterAdvancedDelegation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(letterAdvancedDelegation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LetterId"] = new SelectList(_context.Letters, "Id", "Id", letterAdvancedDelegation.LetterId);
            return View(letterAdvancedDelegation);
        }

        // GET: Sociologist/LetterAdvancedDelegations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letterAdvancedDelegation = await _context.LetterAdvancedDelegations.FindAsync(id);
            if (letterAdvancedDelegation == null)
            {
                return NotFound();
            }
            ViewData["LetterId"] = new SelectList(_context.Letters, "Id", "Id", letterAdvancedDelegation.LetterId);
            return View(letterAdvancedDelegation);
        }

        // POST: Sociologist/LetterAdvancedDelegations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LetterId,Amount,AmountAdditional,QtyStudents,CreditToEMployee,EmployeeMobile,Statatus,Notes")] LetterAdvancedDelegation letterAdvancedDelegation)
        {
            if (id != letterAdvancedDelegation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(letterAdvancedDelegation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LetterAdvancedDelegationExists(letterAdvancedDelegation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LetterId"] = new SelectList(_context.Letters, "Id", "Id", letterAdvancedDelegation.LetterId);
            return View(letterAdvancedDelegation);
        }

        // GET: Sociologist/LetterAdvancedDelegations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letterAdvancedDelegation = await _context.LetterAdvancedDelegations
                .Include(l => l.Letter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letterAdvancedDelegation == null)
            {
                return NotFound();
            }

            return View(letterAdvancedDelegation);
        }

        // POST: Sociologist/LetterAdvancedDelegations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var letterAdvancedDelegation = await _context.LetterAdvancedDelegations.FindAsync(id);
            _context.LetterAdvancedDelegations.Remove(letterAdvancedDelegation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LetterAdvancedDelegationExists(int id)
        {
            return _context.LetterAdvancedDelegations.Any(e => e.Id == id);
        }
    }
}
