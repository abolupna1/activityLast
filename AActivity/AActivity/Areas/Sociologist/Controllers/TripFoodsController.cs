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
    public class TripFoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripFoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/TripFoods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TripFoods.Include(t => t.Letter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/TripFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripFood = await _context.TripFoods
                .Include(t => t.Letter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripFood == null)
            {
                return NotFound();
            }

            return View(tripFood);
        }

        // GET: Sociologist/TripFoods/Create/bokingId
        [Route("Sociologist/TripFoods/Create/{bokingId:int}")]

        public async Task<IActionResult> Create(int bokingId)
        {
            var boking = await _context.TripBookings.FindAsync(bokingId);
            if (boking == null) { return NotFound(); }
            ViewData["bokingId"] = bokingId;

            return View();
        }

        // POST: Sociologist/TripFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Route("Sociologist/TripFoods/Create/{bokingId:int}")]

        public async Task<IActionResult> Create(int bokingId ,TripFoodCreateView tripFood)
        {
            if (ModelState.IsValid)
            {
                Letter letter = new Letter()
                {
                    TripBookingId = tripFood.TripBookingId,
                    TypeLetter = tripFood.TypeLetter
                };
                _context.Add(letter);
                await _context.SaveChangesAsync();
                TripFood food = new TripFood()
                {
                    LetterId = letter.Id,
                    FirstMeal = tripFood.FirstMeal,
                    FirstMealText = tripFood.FirstMealText,
                    LastMeal = tripFood.LastMeal,
                    LastMealText = tripFood.LastMealText,
                    QtyMeals = tripFood.QtyMeals
                };
                _context.Add(food);
                await _context.SaveChangesAsync();
                var boking = await _context.TripBookings.FindAsync(bokingId);
                return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = boking.SchedulingTripDetailId });
            }
            ViewData["bokingId"] = bokingId;

            return View(tripFood);
        }

        // GET: Sociologist/TripFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripFood = await _context.TripFoods.FindAsync(id);
            if (tripFood == null)
            {
                return NotFound();
            }
            ViewData["LetterId"] = new SelectList(_context.Letters, "Id", "Id", tripFood.LetterId);
            return View(tripFood);
        }

        // POST: Sociologist/TripFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LetterId,QtyMeals,FirstMeal,LastMeal,FirstMealText,LastMealText")] TripFood tripFood)
        {
            if (id != tripFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripFoodExists(tripFood.Id))
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
            ViewData["LetterId"] = new SelectList(_context.Letters, "Id", "Id", tripFood.LetterId);
            return View(tripFood);
        }

        // GET: Sociologist/TripFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripFood = await _context.TripFoods
                .Include(t => t.Letter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripFood == null)
            {
                return NotFound();
            }

            return View(tripFood);
        }

        // POST: Sociologist/TripFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripFood = await _context.TripFoods.FindAsync(id);
            _context.TripFoods.Remove(tripFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripFoodExists(int id)
        {
            return _context.TripFoods.Any(e => e.Id == id);
        }
    }
}
