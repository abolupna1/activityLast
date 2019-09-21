using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class LettersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LettersController(ApplicationDbContext context)
        {
            _context = context;
        }


      

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Letters.Where(m=>m.NoMersal == 0)
                .Include(l=>l.LetterTransports);
            return View(await applicationDbContext.OrderByDescending(i=>i.Id).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/Letters/NoMersalUpdate")]
        public async Task<IActionResult> NoMersalUpdate(int letterId, int NoMersal)
        {

            var letter = await _context.Letters.FindAsync(letterId);
            letter.NoMersal = NoMersal;
            _context.Update(letter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexByBoking),new { bokingId = letter.TripBookingId});

        }

        [Route("Sociologist/Letters/IndexByBoking/{bokingId:int}")]
        public async Task<IActionResult> IndexByBoking(int bokingId)
        {
            var boking = await _context.TripBookings.FindAsync(bokingId);
    
            var Letters = await _context.Letters
                .Where(b => b.TripBookingId == bokingId)
               .Include(t => t.LetterTransports)
               .Include(t => t.LetterAdvancedDelegations).ThenInclude(s=>s.LetterSignutreForAdvances)
               .Include(t => t.TripBooking.TripDelegates)
               .Include(t => t.LetterTransports).ThenInclude(u=>u.User)
               .Include(b=>b.TripBooking.StudentsParticipatingInTrips)
               .Include(t => t.LetteSignutres)
               .Include(a => a.LetterAdvancedDelegations)
               //.Include(b => b.LetterAdvancedEducations)
               .Include(f => f.LetterFoods)
               .Include(f => f.TripBooking.SchedulingTripDetail.TripType)
               .Include(f => f.TripBooking.SchedulingTripDetail.EducationalBody)
                .ToListAsync();
            ViewBag.bokingId = bokingId;
            ViewBag.detailId = boking.SchedulingTripDetailId;
            return View(Letters);
        }

    

        // GET: Sociologist/Letters/Create
        public IActionResult Create()
        {
            ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName");
            return View();
        }

        // POST: Sociologist/Letters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TripBookingId,NoMersal,TypeLetter")] Letter letter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(letter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName", letter.TripBookingId);
            return View(letter);
        }

        // GET: Sociologist/Letters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letter = await _context.Letters.FindAsync(id);
            if (letter == null)
            {
                return NotFound();
            }
            ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName", letter.TripBookingId);
            return View(letter);
        }

        // POST: Sociologist/Letters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TripBookingId,NoMersal,TypeLetter")] Letter letter)
        {
            if (id != letter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(letter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LetterExists(letter.Id))
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
            ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName", letter.TripBookingId);
            return View(letter);
        }

        // GET: Sociologist/Letters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var letter = await _context.Letters
                .Include(l => l.TripBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letter == null)
            {
                return NotFound();
            }

            return View(letter);
        }

        // POST: Sociologist/Letters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var letter = await _context.Letters.FindAsync(id);
            _context.Letters.Remove(letter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.Id == id);
        }
    }
}
