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

        // GET: Sociologist/Letters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Letters.Include(l => l.TripBooking).ThenInclude(l=>l.SchedulingTripDetail.EducationalBody);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/Letters/Details/5
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


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ Route("Sociologist/Letters/Create")]

        public async Task<IActionResult> Create(int TripBookingId,int TypeLetter,int tripDetailsId)
        {
            Letter letter = new Letter()
            {
                TripBookingId = TripBookingId,
                TypeLetter= TypeLetter
            };
            _context.Add(letter);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = tripDetailsId });


        }

    

        // POST: Sociologist/Letters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/Letters/NoMersalUpdate")]

        public async Task<IActionResult> NoMersalUpdate(int letterId,string NoMersal,int detailId)
        {

           var letter = await _context.Letters.FindAsync(letterId);
            letter.NoMersal = NoMersal;
                _context.Update(letter);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = detailId });

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
