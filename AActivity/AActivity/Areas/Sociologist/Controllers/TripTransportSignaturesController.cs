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
                         .Where(l => l.TripBookingId == bokingId && l.TypeLetter == 1).ToListAsync();
            CultureInfo cultureInfo = new CultureInfo("ar-Sa");
            ViewBag.cultureInfo = cultureInfo;
            var bbs = await _context.Signatures.Include(s=>s.SignutreDelegates).Include(u=>u.User).ToListAsync();
            
              ViewData["signutre"] = bbs;
            if (tripTransportSignature.Count() == 0)
            {
                Letter letter = new Letter()
                {
                    TripBookingId = bokingId,
                    TypeLetter = 1,
                };
                _context.Add(letter);
                await _context.SaveChangesAsync();

                tripTransportSignature = await _context.Letters
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


            }
            ViewBag.userId =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            ViewData["tripTypes"] = await _context.TripTypes.ToListAsync();
            var Leter = tripTransportSignature.FirstOrDefault();
            
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
