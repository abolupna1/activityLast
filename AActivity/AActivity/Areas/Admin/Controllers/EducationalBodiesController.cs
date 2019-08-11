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
using AActivity.Areas.Admin.Helpers;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EducationalBodiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IFlasher _f;
        public EducationalBodiesController(ApplicationDbContext context, IFlasher f)
        {
            _context = context;
            _f = f;
        }

        // GET: Admin/EducationalBodies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EducationalBodies.Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/EducationalBodies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }

            var educationalBody = await _context.EducationalBodies
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalBody == null)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }
            
            return View(educationalBody);
        }

        // GET: Admin/EducationalBodies/Create
        public IActionResult Create()
        {
           var type = new List<EntityTypeForEducation>();
         
            var Institutes = new EntityTypeForEducation(){ Name = "المعاهد والدور"};
            type.Add(Institutes);
            var Colleges = new EntityTypeForEducation(){ Name = "الكليات"};
            type.Add(Colleges);

            ViewData["UserId"] = new SelectList(_context.Users.Include(u=>u.UserRoles).Where(r=>r.UserRoles.Any(a=>a.Role.Name == "Supervisor")), "Id", "FullName");
            ViewData["type"] = new SelectList(type, "Name", "Name");
       
            return View();
        }

        // POST: Admin/EducationalBodies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId,EntityType")] EducationalBody educationalBody)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationalBody);
                await _context.SaveChangesAsync();
                _f.Flash("success", "تم الحفظ بنجاح");
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users.Include(u => u.UserRoles).Where(r => r.UserRoles.Any(a => a.Role.Name == "Supervisor")), "Id", "FullName", educationalBody.UserId);
            return View(educationalBody);
        }

        // GET: Admin/EducationalBodies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }

            var educationalBody = await _context.EducationalBodies.FindAsync(id);
            if (educationalBody == null)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }
            var type = new List<EntityTypeForEducation>();

            var Institutes = new EntityTypeForEducation() { Name = "المعاهد والدور" };
            type.Add(Institutes);
            var Colleges = new EntityTypeForEducation() { Name = "الكليات" };
            type.Add(Colleges);
            ViewData["type"] = new SelectList(type, "Name", "Name", educationalBody.EntityType);

            ViewData["UserId"] = new SelectList(_context.Users.Include(u => u.UserRoles).Where(r => r.UserRoles.Any(a => a.Role.Name == "Supervisor")), "Id", "FullName", educationalBody.UserId);
            return View(educationalBody);
        }

        // POST: Admin/EducationalBodies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,EntityType")] EducationalBody educationalBody)
        {
            if (id != educationalBody.Id)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }

            if (ModelState.IsValid)
            {
                _context.Update(educationalBody);
                _f.Flash("success", "تم التعديل بنجاح");
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            var type = new List<EntityTypeForEducation>();

            var Institutes = new EntityTypeForEducation() { Name = "المعاهد والدور" };
            type.Add(Institutes);
            var Colleges = new EntityTypeForEducation() { Name = "الكليات" };
            type.Add(Colleges);
            ViewData["type"] = new SelectList(type, "Name", "Name", educationalBody.EntityType);

            ViewData["UserId"] = new SelectList(_context.Users.Include(u => u.UserRoles).Where(r => r.UserRoles.Any(a => a.Role.Name == "Supervisor")), "Id", "FullName", educationalBody.UserId);
            return View(educationalBody);
        }

        // GET: Admin/EducationalBodies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }

            var educationalBody = await _context.EducationalBodies
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalBody == null)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }

            return View(educationalBody);
        }

        // POST: Admin/EducationalBodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationalBody = await _context.EducationalBodies.FindAsync(id);
            _context.EducationalBodies.Remove(educationalBody);
            await _context.SaveChangesAsync();
            _f.Flash("success", "تم الحذف بنجاح");
            return RedirectToAction(nameof(Index));
        }

        private bool EducationalBodyExists(int id)
        {
            return _context.EducationalBodies.Any(e => e.Id == id);
        }
    }
}
