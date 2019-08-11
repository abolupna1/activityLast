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
using Vereyon.Web;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AcademicYearsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IFlashMessage _f { get; private set; }

        public AcademicYearsController(ApplicationDbContext context, IFlashMessage f)
        {
            _context = context;
            _f = f;
        }

        // GET: Admin/AcademicYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.AcademicYears.ToListAsync());
        }

        // GET: Admin/AcademicYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }

            var academicYear = await _context.AcademicYears
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicYear == null)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }

            return View(academicYear);
        }

        // GET: Admin/AcademicYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AcademicYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicYear);
                await _context.SaveChangesAsync();
                _f.Info( "تم الحفظ بنجاح");
                return RedirectToAction(nameof(Index));
            }
            return View(academicYear);
        }

        // GET: Admin/AcademicYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }

            var academicYear = await _context.AcademicYears.FindAsync(id);
            if (academicYear == null)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }
            return View(academicYear);
        }

        // POST: Admin/AcademicYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AcademicYear academicYear)
        {
            if (id != academicYear.Id)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }

            if (ModelState.IsValid)
            {
              
                    _context.Update(academicYear);
                _f.Info("تم التعديل بنجاح");
                await _context.SaveChangesAsync();
                
            
                return RedirectToAction(nameof(Index));
            }
            return View(academicYear);
        }

        // GET: Admin/AcademicYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }

            var academicYear = await _context.AcademicYears
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicYear == null)
            {
                Response.StatusCode = 404;
                return View("AcademicYearsNotFound");
            }

            return View(academicYear);
        }

        // POST: Admin/AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicYear = await _context.AcademicYears.FindAsync(id);
            _context.AcademicYears.Remove(academicYear);
            await _context.SaveChangesAsync();
            _f.Info("تم الحذف بنجاح");
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicYearExists(int id)
        {
            return _context.AcademicYears.Any(e => e.Id == id);
        }
    }
}
