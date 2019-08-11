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
using Microsoft.AspNetCore.Authorization;
using Core.Flash;
using AActivity.Areas.Sociologist.ModelViews;
using System.Globalization;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
   
    public class SchedulingTripHeadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IFlasher _f;
        public SchedulingTripHeadsController(ApplicationDbContext context, IFlasher f)
        {
            _context = context;
            _f = f;
        }

        // GET: Sociologist/SchedulingTripHeads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SchedulingTripHead
                .Include(s => s.User).Include(d=>d.SchedulingTripDetails)
                .ThenInclude(e=>e.EducationalBody)
                .OrderByDescending(s=>s.ToDate );
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        public async Task<IActionResult> StatusUpdate(int Id)
        {
            
            var schedulingTripHead = await _context.SchedulingTripHead.FindAsync(Id);
            bool status = schedulingTripHead.Status;
            if (schedulingTripHead != null)
            {
                foreach(var s in await _context.SchedulingTripHead.ToListAsync())
                {
                    s.Status = false;
                    _context.SchedulingTripHead.Update(s);
                }
                await _context.SaveChangesAsync();
            }
            schedulingTripHead.Status = status == true ? false : true;
            _context.SchedulingTripHead.Update(schedulingTripHead);
            await _context.SaveChangesAsync();
            _f.Flash("success", "تم الحفظ بنجاح");
            return RedirectToAction(nameof(Index));
        }

        // GET: Sociologist/SchedulingTripHeads/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }
           

            SchedulingTripDetailsView schedulingTripDetailsView = new SchedulingTripDetailsView()
            {
                EducationalBodies = await _context.EducationalBodies.ToListAsync(),
                SchedulingTripDetails = await _context.SchedulingTripDetails.Where(h => h.SchedulingTripHeadId == id).ToListAsync(),
                SchedulingTripHead = await _context.SchedulingTripHead
                .Include(s => s.User)
                .Include(d => d.SchedulingTripDetails).ThenInclude(e => e.EducationalBody)

              
                .FirstOrDefaultAsync(m => m.Id == id)

        };
            if (schedulingTripDetailsView.SchedulingTripHead == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }

           
            return View(schedulingTripDetailsView);
        }

        // GET: Sociologist/SchedulingTripHeads/Create
        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        public IActionResult Create()
        {

           
            ViewData["AcademicYear"] = new SelectList(_context.AcademicYears, "Name", "Name");
            ViewData["Semester"] = new SelectList(SemesterList(), "Name", "Name");
            ViewData["UserId"] = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }

        // POST: Sociologist/SchedulingTripHeads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        public async Task<IActionResult> Create([Bind("Id,AcademicYear,Semester,FromDate,ToDate,UserId")] SchedulingTripHead schedulingTripHead)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedulingTripHead);
                await _context.SaveChangesAsync();
                _f.Flash("success", "تم الحفظ بنجاح");
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYear"] = new SelectList(_context.AcademicYears, "Name", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", schedulingTripHead.UserId);
            return View(schedulingTripHead);
        }

        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }

            var schedulingTripHead = await _context.SchedulingTripHead.FindAsync(id);
            if (schedulingTripHead == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }
            ViewData["Semester"] = new SelectList(SemesterList(), "Name", "Name",schedulingTripHead.Semester);

            ViewData["AcademicYear"] = new SelectList(_context.AcademicYears, "Name", "Name", schedulingTripHead.AcademicYear);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", schedulingTripHead.UserId);
            return View(schedulingTripHead);
        }

        // POST: Sociologist/SchedulingTripHeads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcademicYear,Semester,FromDate,ToDate,UserId")] SchedulingTripHead schedulingTripHead)
        {
            if (id != schedulingTripHead.Id)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }

            if (ModelState.IsValid)
            {
                _context.Update(schedulingTripHead);
                _f.Flash("success", "تم التعديل بنجاح");
                await _context.SaveChangesAsync();

             
                return RedirectToAction(nameof(Index));
            }
            ViewData["Semester"] = new SelectList(SemesterList(), "Name", "Name", schedulingTripHead.Semester);

            ViewData["AcademicYear"] = new SelectList(_context.AcademicYears, "Name", "Name", schedulingTripHead.AcademicYear);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", schedulingTripHead.UserId);
            return View(schedulingTripHead);
        }

        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }

            var schedulingTripHead = await _context.SchedulingTripHead
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedulingTripHead == null)
            {
                Response.StatusCode = 404;
                return View("SchedulingTripHeadsNotFound");
            }

            return View(schedulingTripHead);
        }

        [Authorize(Roles = "Admin,DirectorOfSocialActivity")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedulingTripHead = await _context.SchedulingTripHead.FindAsync(id);
            _context.SchedulingTripHead.Remove(schedulingTripHead);
            await _context.SaveChangesAsync();
            _f.Flash("success", "تم الحذف بنجاح");
            return RedirectToAction(nameof(Index));
        }

     private IList<SemesterHelper> SemesterList()
        {
            IList<SemesterHelper> semesterHelpers = new List<SemesterHelper>()
            {
                new SemesterHelper(){Name = "الفصل الدراسي الأول" },
                new SemesterHelper(){Name = "الفصل الدراسي الثاني" },
                new SemesterHelper(){Name = "الفصل الدراسي الصيفي" },
            };
            return semesterHelpers;
        }

   

        private bool SchedulingTripHeadExists(int id)
        {
            return _context.SchedulingTripHead.Any(e => e.Id == id);
        }

     
    }
}
