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
    public class LetterDelegationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LetterDelegationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/LetterDelegations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Letters.Include(l => l.TripBooking);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/LetterDelegations/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Sociologist/LetterDelegations/Create
        public IActionResult Create()
        {
            ViewData["TripBookingId"] = new SelectList(_context.TripBookings, "Id", "TripLocationName");
            return View();
        }

        // POST: Sociologist/LetterDelegations/Create
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

        // GET: Sociologist/LetterDelegations/Edit/5
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

        // POST: Sociologist/LetterDelegations/Edit/5
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

        // GET: Sociologist/LetterDelegations/Delete/5
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

        // POST: Sociologist/LetterDelegations/Delete/5
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
