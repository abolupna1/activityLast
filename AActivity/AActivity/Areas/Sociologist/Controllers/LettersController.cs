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
using System.Security.Claims;

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


        private void getControllerName(int typeInt,out string controlleName, out string typeName)
        {
            
            switch (typeInt)
            {
                case 1:
                    controlleName = "LetterTransports";
                    typeName = "تأمين وسيلة نقل";
                    break;

                case 2:
                    controlleName = "LetterFoods";
                    typeName = "تأمين تغذية ";
                    break;

                case 3:
                    controlleName = "LetterDelegations";
                    typeName = "انتداب جماعي";
                    break;

                case 4:
                    controlleName = "LetterAdvancedDelegations";
                    typeName = "سلفة من صندوق الطالب";
                    break;

                default:
                    controlleName = "";
                    typeName = "";
                    break;
            };
         

        }

        public async Task<IActionResult> Index()
        {
            var letters = new List<LettersModelView>();
            var Allletters = await _context.Letters
                .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(n=>n.NotificationLetters).ThenInclude(u=>u.User)

                .ToListAsync();

            foreach (var l in Allletters)
            {
                getControllerName(l.TypeLetter, out string controlleName,out string typeName);
                var letter = new LettersModelView()
                {
                    TripId = l.TripBookingId,
                    LetterId = l.Id,
                    TypeLetter = typeName,
                    ControlleName = controlleName,
                    TripType = l.TripBooking.SchedulingTripDetail.TripType.Name,
                    EducationBody = l.TripBooking.SchedulingTripDetail.EducationalBody.Name,
                    TripDate = l.TripBooking.SchedulingTripDetail.TripDate,
                    NotificationLettersStatus = l.NotificationLetters.FirstOrDefault().Status,
                    NotificationLettersUserId=l.NotificationLetters.FirstOrDefault().UserId
                };
                letters.Add(letter);
            }
            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(letters);

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
