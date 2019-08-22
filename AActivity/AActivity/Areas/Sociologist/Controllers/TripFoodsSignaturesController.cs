using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using AActivity.Areas.Sociologist.Helpers;
using System.Security.Claims;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class TripFoodsSignaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripFoodsSignaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/TripFoodsSignatures
        public async Task<IActionResult> Index()
        {
            var letters = await _context.Letters.Where(l => l.TypeLetter == 2)
                .Where(h=>h.TripBooking.SchedulingTripDetail.SchedulingTripHead.Status == true)
                .Include(t=>t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t=>t.TripBooking.SchedulingTripDetail.SchedulingTripHead)
                .Include(t=>t.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(t=>t.TripBooking.SchedulingTripDetail)
                .Include(s => s.TripFoodsSignatures).OrderByDescending(o=>o.Id)
                
                .ToListAsync();

            ViewData["signutre"] = await _context.Signatures.ToListAsync(); 
            return View( letters);
        }

        // GET: Sociologist///
        [Route("Sociologist/TripFoodsSignatures/Details/{letterId:int}")]

        public async Task<IActionResult> Details(int letterId)
        {
            var letter = await _context.Letters.FindAsync(letterId);
            if (letter == null) { return NotFound(); }
            ViewData["signutre"] = await _context.Signatures.Include(s=>s.SignutreDelegates).ToListAsync();

            var foodSignutres = await _context.Letters.Include(f => f.TripFood)
                 .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(t => t.TripBooking.SchedulingTripDetail)
                .Include(t => t.TripBooking.City)
                .Include(t => t.TripBooking.StudentsParticipatingInTrips)
                .Include(t => t.TripFood)
                  .Include(s => s.TripFoodsSignatures).FirstOrDefaultAsync(i=>i.Id == letterId);

        
            return View(foodSignutres);
        }

    

        // POST: Sociologist/TripFoodsSignatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int LetterId,int WhoHasSignutre)
        {
            var letter = await _context.Letters.FindAsync(LetterId);
            if (letter == null || WhoHasSignutre == null) { return NotFound(); }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            TripFoodsSignature trip = new TripFoodsSignature()
            {

                LetterId = LetterId,
                WhoHasSignutre = WhoHasSignutre,
                SignatureId = SignutreOfUserHelper.getUserSignutre(userId, _context)
            };
            try
            {
               // throw new Exception(";lkjhg");
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { letterId = LetterId });

            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorTitle = "خطأ";
                ViewBag.ErrorMessage = " لم يتم الحفظ الرجاء اعد المحاولة او اتصل بالمسؤول";
                return View("Error");
            }

          

        }

      
    }
}
