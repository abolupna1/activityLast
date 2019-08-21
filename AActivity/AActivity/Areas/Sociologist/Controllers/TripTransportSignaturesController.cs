using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using System.Globalization;
using System.Security.Claims;
using AActivity.Areas.Sociologist.Helpers;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class TripTransportSignaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripTransportSignaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> countbuses(int bokingId)
        {

            int countbus = 0;
            var students = await _context.StudentsParticipatingInTrip.Where(s => s.TripBookingId == bokingId).CountAsync();
            var boking = await _context.TripBookings.FindAsync(bokingId);
            var EducationalBody = await _context.SchedulingTripDetails.Include(c => c.EducationalBody).FirstOrDefaultAsync(e => e.Id == boking.SchedulingTripDetailId); ;
            var b = EducationalBody.EducationalBody.EntityType;
            int typeEdu = b == "الكليات" ? 1 : 2;
            if (students < 60 && students >= 30)
            {
                countbus = 1 * typeEdu;
                return countbus;
            }
            else if (students >= 60 && students <= 100)
            {
                countbus = 2 * typeEdu;
                return countbus;
            }
            else
            {

                while (students >= 50)
                {
                    countbus++;
                    students -= 50;
                }

                if (students < 50 && students >= 30)
                {
                    countbus++;
                }
                return countbus * typeEdu;
            }


        }

        [HttpGet(), Route("Sociologist/TripTransportSignatures/Details/{bokingId:int}")]
        public async Task<IActionResult> Details(int bokingId)
        {

            var tripTransportSignature = await _context.Letters
           
              .Include(t => t.TripTransportSignatures)
              .ThenInclude(s => s.Signature)
              .Include(t => t.TripBooking)
              .ThenInclude(s=>s.StudentsParticipatingInTrips)
              .Include(t => t.TripBooking)

              .ThenInclude(t => t.SchedulingTripDetail.EducationalBody)
                  .Include(t => t.TripBooking)
              .ThenInclude(u => u.SchedulingTripDetail.EducationalBody.User)
                 .Include(t => t.TripBooking)
              .ThenInclude(u => u.City)
                 .Include(t => t.TripBooking)
              .ThenInclude(t=>t.SchedulingTripDetail.TripType)
                         .Where(l => l.TripBookingId == bokingId && l.TypeLetter == 1).ToListAsync();
            CultureInfo cultureInfo = new CultureInfo("ar-Sa");
            ViewBag.cultureInfo = cultureInfo;
            var bbs = await _context.Signatures.Include(s => s.SignutreDelegates).Include(u => u.User).ToListAsync();

            ViewData["signutre"] = bbs;

            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            ViewData["tripTypes"] = await _context.TripTypes.ToListAsync();
            var Leter = tripTransportSignature.FirstOrDefault();

            ViewBag.countBus = countbuses( bokingId).Result;
            return View(Leter);



        }

        [HttpGet(), Route("Sociologist/TripTransportSignatures/Print/{bokingId:int}")]
        public async Task<IActionResult> Print(int bokingId)
        {

            var tripTransportSignature = await _context.Letters

              .Include(t => t.TripTransportSignatures)
              .ThenInclude(s => s.Signature)
              .Include(t => t.TripBooking)
              .ThenInclude(s => s.StudentsParticipatingInTrips)
              .Include(t => t.TripBooking)

              .ThenInclude(t => t.SchedulingTripDetail.EducationalBody)
                  .Include(t => t.TripBooking)
              .ThenInclude(u => u.SchedulingTripDetail.EducationalBody.User)
                 .Include(t => t.TripBooking)
              .ThenInclude(u => u.City)
                         .Where(l => l.TripBookingId == bokingId && l.TypeLetter == 1).ToListAsync();
            CultureInfo cultureInfo = new CultureInfo("ar-Sa");
            ViewBag.cultureInfo = cultureInfo;
            var bbs = await _context.Signatures.Include(s => s.SignutreDelegates).Include(u => u.User).ToListAsync();

            ViewData["signutre"] = bbs;

            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            ViewData["tripTypes"] = await _context.TripTypes.ToListAsync();
            var Leter = tripTransportSignature.FirstOrDefault();

            ViewBag.countBus = countbuses(bokingId).Result;
            return View(Leter);



        }


        // POST: Sociologist/TripTransportSignatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int LetterId ,int StatusSignature,int bokingId)
        {
            if (LetterId==null || StatusSignature==null)
            {
                return RedirectToAction(nameof(Details), new { bokingId = bokingId });

            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            TripTransportSignature tripTransportSignature = new TripTransportSignature()
            {
                LetterId = LetterId,
                SignatureId = SignutreOfUserHelper.getUserSignutre(userId, _context),
                StatusSignature= StatusSignature
                
            };
        
                _context.Add(tripTransportSignature);
                await _context.SaveChangesAsync();
        
            await _context.SaveChangesAsync();
          

            return RedirectToAction(nameof(Details),new { bokingId= bokingId });
        
           
        }



        // POST: Sociologist/TripTransportSignatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,int bokingId)
        {
            var tripTransportSignature = await _context.TripTransportSignatures.FindAsync(id);
            _context.TripTransportSignatures.Remove(tripTransportSignature);
            await _context.SaveChangesAsync();
       
      

            return RedirectToAction(nameof(Details),new { bokingId = bokingId });
        }

        private bool TripTransportSignatureExists(int id)
        {
            return _context.TripTransportSignatures.Any(e => e.Id == id);
        }
    }
}
