using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using Microsoft.AspNetCore.Authorization;
using Core.Flash;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFlasher _f;
        public CitiesController(ApplicationDbContext context, IFlasher f)
        {
            _context = context;
            _f = f;
        }

        // GET: Admin/TripLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cities.ToListAsync());
        }

        // GET: Admin/TripLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("TripLocationsNotFound");
            }

            var tripLocation = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripLocation == null)
            {
                Response.StatusCode = 404;
                return View("TripLocationsNotFound");
            }

            return View(tripLocation);
        }

        // GET: Admin/TripLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TripLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocationName")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                _f.Flash("success", "تم الحفظ بنجاح");
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Admin/TripLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("TripLocationsNotFound");
            }

            var tripLocation = await _context.Cities.FindAsync(id);
            if (tripLocation == null)
            {
                Response.StatusCode = 404;
                return View("TripLocationsNotFound");
            }
            return View(tripLocation);
        }

        // POST: Admin/TripLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocationName")] City city)
        {
            if (id != city.Id)
            {
                Response.StatusCode = 404;
                return View("TripLocationsNotFound");
            }

            if (ModelState.IsValid)
            {
                _context.Update(city);
                _f.Flash("success", "تم التعديل بنجاح");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Admin/TripLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripLocation = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripLocation == null)
            {
                Response.StatusCode = 404;
                return View("TripLocationsNotFound");
            }

            return View(tripLocation);
        }

        // POST: Admin/TripLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripLocation = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(tripLocation);
            await _context.SaveChangesAsync();
            _f.Flash("success", "تم الحذف بنجاح");
            return RedirectToAction(nameof(Index));
        }

        private bool TripLocationExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
