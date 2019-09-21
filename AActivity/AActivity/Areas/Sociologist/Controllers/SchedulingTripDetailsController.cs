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
using System.Globalization;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class SchedulingTripDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulingTripDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/SchedulingTripDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchedulingTripDetails.Include(s => s.EducationalBody).Include(s => s.SchedulingTripHead).Include(s => s.TripType);
            return View(await applicationDbContext.Where(t=>t.SchedulingTripHead.Status==true).ToListAsync());
        }

        public IActionResult CreateTripsCount(int id,int EducationalBodyId,int tripCount)
        {
            return RedirectToAction(nameof(Create), new { id = id, EducationalBodyId = EducationalBodyId, tripCount = tripCount });
        }

        // GET: 
        [Route("Sociologist/SchedulingTripDetails/DetailsMore/{id:int}")]
        public async Task<IActionResult> DetailsMore(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }

            var schedulingTripDetail = await _context.SchedulingTripDetails
                    .Include(s => s.EducationalBody)
                    .Include(s => s.SchedulingTripHead)
                    .Include(s => s.TripBookings)
                    .ThenInclude(c => c.City)
                    .Include(st => st.TripBookings)
                    .ThenInclude(stu => stu.StudentsParticipatingInTrips)
                      .Include(st => st.TripBookings)
                    .ThenInclude(td => td.TripDelegates)
                    .Include(l => l.TripBookings)
                    .Include(l => l.TripBookings)
                    .ThenInclude(r=>r.TripReports)
                    .ThenInclude(i=>i.TripReportImages)
                    .Include(l => l.TripBookings)
                    .ThenInclude(r => r.TripReports)
                    .ThenInclude(i => i.TripReportsNotes)
                    .Include(s => s.TripBookings).ThenInclude(l=>l.Letters)

                  .Include(t=>t.TripType)
                 

                    //.Include(s => s.TripType)
                    .FirstOrDefaultAsync(e=>e.Id==id);
            if (schedulingTripDetail == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }


            return View(schedulingTripDetail);
        }

        // GET: Sociologist/SchedulingTripDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }

            var schedulingTripDetail = await _context.SchedulingTripDetails
                .Include(s => s.EducationalBody)
                .Include(s => s.SchedulingTripHead)
             
                .Include(s => s.TripType)
                .Where(m => m.EducationalBodyId == id).ToListAsync();
            if (schedulingTripDetail == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }

            ViewData["EducationalBodies"] = await _context.EducationalBodies.FindAsync(id);
            ViewData["SchedulingTripHeadId"] = await _context.SchedulingTripHead.FindAsync(schedulingTripDetail.FirstOrDefault().SchedulingTripHeadId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes.Where(t => t.Id != 4), "Id", "Name");
            ViewData["tripCount"] = schedulingTripDetail.Count();

            return View(schedulingTripDetail);
        }

       [HttpGet]
       [Route("Sociologist/SchedulingTripDetails/Create/{id:int}/{EducationalBodyId:int}/{tripCount:int}")]
        public async Task<IActionResult> Create(int id, int EducationalBodyId, int tripCount)
        {
            var tripHead = await _context.SchedulingTripHead.FindAsync(id);
            var eduBody = await _context.EducationalBodies.FindAsync(EducationalBodyId);
         
            ViewData["EducationalBodies"] = eduBody;
            ViewData["SchedulingTripHeadId"] =tripHead;
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes.Where(t=>t.Id != 4), "Id", "Name");
            ViewData["tripCount"] = tripCount;
        
            return View();
        }

        // POST: Sociologist/SchedulingTripDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/SchedulingTripDetails/Create/{id:int}/{EducationalBodyId:int}/{tripCount:int}")]
        public async Task<IActionResult> Create(int id ,int EducationalBodyId,int tripCount , IList<SchedulingTripDetail> schedulingTripDetail)
        {
            if (ModelState.IsValid)
            {
                foreach (var detail in schedulingTripDetail)
                {
                  
                    _context.Add(detail);
                    await _context.SaveChangesAsync();
                }
               
                return RedirectToAction(nameof(Details), "SchedulingTripHeads", new {id=id });
            }
            ViewData["EducationalBodies"] = await _context.EducationalBodies.FindAsync(EducationalBodyId);
            ViewData["SchedulingTripHeadId"] = await _context.SchedulingTripHead.FindAsync(id);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes.Where(t => t.Id != 4), "Id", "Name");
            ViewData["tripCount"] = tripCount;
            return View(schedulingTripDetail);
        }

        // GET: Sociologist/SchedulingTripDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }

            var schedulingTripDetail = await _context.SchedulingTripDetails
                .Where(e=>e.EducationalBodyId == id).ToListAsync();
            if (schedulingTripDetail == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }
            ViewData["EducationalBodies"] = await _context.EducationalBodies.FindAsync(id);
            ViewData["SchedulingTripHeadId"] = await _context.SchedulingTripHead.FindAsync(schedulingTripDetail.FirstOrDefault().SchedulingTripHeadId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes.Where(t => t.Id != 4), "Id", "Name");
            ViewData["tripCount"] = schedulingTripDetail.Count();
            return View(schedulingTripDetail);
        }

        // POST: Sociologist/SchedulingTripDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,IList<SchedulingTripDetail> schedulingTripDetail)
        {
          

            if (ModelState.IsValid)
            {
                _context.UpdateRange(schedulingTripDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var schedulingTripDetails = await _context.SchedulingTripDetails
                     .Where(e => e.EducationalBodyId == id).ToListAsync();
            if (schedulingTripDetail == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }
            ViewData["EducationalBodies"] = await _context.EducationalBodies.FindAsync(id);
            ViewData["SchedulingTripHeadId"] = await _context.SchedulingTripHead.FindAsync(schedulingTripDetails.FirstOrDefault().SchedulingTripHeadId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes.Where(t => t.Id != 4), "Id", "Name");
            ViewData["tripCount"] = schedulingTripDetails.Count();
            return View(schedulingTripDetail);
        }

        // GET: Sociologist/SchedulingTripDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }

            var schedulingTripDetail = await _context.SchedulingTripDetails
                .Include(s => s.EducationalBody)
                .Include(s => s.SchedulingTripHead)
                .Include(s => s.TripType)
                .Where(m => m.EducationalBodyId == id).ToListAsync();
            if (schedulingTripDetail == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripDetailsNotFound");
            }

            ViewData["EducationalBodies"] = await _context.EducationalBodies.FindAsync(id);
            ViewData["SchedulingTripHeadId"] = await _context.SchedulingTripHead.FindAsync(schedulingTripDetail.FirstOrDefault().SchedulingTripHeadId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes.Where(t => t.Id != 4), "Id", "Name");
            ViewData["tripCount"] = schedulingTripDetail.Count();

            return View(schedulingTripDetail);
        }

        // POST: Sociologist/SchedulingTripDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id ,IEnumerable<int> checkForDelete)
        {
           
            var schedulingTripDetail = await _context.SchedulingTripDetails
                .Where(e => checkForDelete.Contains(e.Id)).ToListAsync();
            _context.SchedulingTripDetails.RemoveRange(schedulingTripDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "SchedulingTripHeads",new { id=id});
        }

        private bool SchedulingTripDetailExists(int id)
        {
            return _context.SchedulingTripDetails.Any(e => e.Id == id);
        }

    
    }
}
