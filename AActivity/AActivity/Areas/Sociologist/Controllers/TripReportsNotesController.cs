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
    public class TripReportsNotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripReportsNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/TripReportsNotes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TripReportsNotes.Include(t => t.TripReport).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/TripReportsNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripReportsNote = await _context.TripReportsNotes
                .Include(t => t.TripReport)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripReportsNote == null)
            {
                return NotFound();
            }

            return View(tripReportsNote);
        }

        // GET: Sociologist/TripReportsNotes/Create
        public IActionResult Create()
        {
            ViewData["TripReportId"] = new SelectList(_context.TripReports, "Id", "TextBody");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Sociologist/TripReportsNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TripReportId,Note,DateNotes,UserId")] TripReportsNote tripReportsNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripReportsNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripReportId"] = new SelectList(_context.TripReports, "Id", "TextBody", tripReportsNote.TripReportId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", tripReportsNote.UserId);
            return View(tripReportsNote);
        }

        // GET: Sociologist/TripReportsNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripReportsNote = await _context.TripReportsNotes.FindAsync(id);
            if (tripReportsNote == null)
            {
                return NotFound();
            }
            ViewData["TripReportId"] = new SelectList(_context.TripReports, "Id", "TextBody", tripReportsNote.TripReportId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", tripReportsNote.UserId);
            return View(tripReportsNote);
        }

        // POST: Sociologist/TripReportsNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TripReportId,Note,DateNotes,UserId")] TripReportsNote tripReportsNote)
        {
            if (id != tripReportsNote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripReportsNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripReportsNoteExists(tripReportsNote.Id))
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
            ViewData["TripReportId"] = new SelectList(_context.TripReports, "Id", "TextBody", tripReportsNote.TripReportId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", tripReportsNote.UserId);
            return View(tripReportsNote);
        }

        // GET: Sociologist/TripReportsNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripReportsNote = await _context.TripReportsNotes
                .Include(t => t.TripReport)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripReportsNote == null)
            {
                return NotFound();
            }

            return View(tripReportsNote);
        }

        // POST: Sociologist/TripReportsNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripReportsNote = await _context.TripReportsNotes.FindAsync(id);
            _context.TripReportsNotes.Remove(tripReportsNote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripReportsNoteExists(int id)
        {
            return _context.TripReportsNotes.Any(e => e.Id == id);
        }
    }
}
