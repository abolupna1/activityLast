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
using AActivity.Areas.Admin.ModelViews;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EducationalBodiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IFlasher _f;
        private readonly IHostingEnvironment _ihostingEnvironment;
        public EducationalBodiesController(ApplicationDbContext context, IFlasher f, 
            IHostingEnvironment ihostingEnvironment)
        {
            _context = context;
            _ihostingEnvironment = ihostingEnvironment;
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

            ViewData["UserId"] = new SelectList(_context.Users.Include(u=>u.UserRoles)
                .Where(r=>r.UserRoles.Any(a=>a.Role.Name == "Supervisor")), "Id", "FullName");
            ViewData["type"] = new SelectList(type, "Name", "Name");
       
            return View();
        }

        // POST: Admin/EducationalBodies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EducationBodyCreateViewModel bodyEdu)
        {
            if (ModelState.IsValid)
            {
                string uniqFileName = null;
                if (bodyEdu.StampFile != null && bodyEdu.StampFile.Length > 0)
                {
                    if (IsFileValidate(bodyEdu.StampFile.FileName))
                    {
                        string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/stamps");
                        uniqFileName = Guid.NewGuid().ToString() + "_" + bodyEdu.StampFile.FileName;
                        string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            bodyEdu.StampFile.CopyTo(fileStream);
                        }


                    }
                    else
                    {
                        ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";

                        return View(bodyEdu);
                    }
                }
                var educationalBody = new EducationalBody()
                {
                    Name= bodyEdu.Name,City= bodyEdu.City,
                    EntityType= bodyEdu.EntityType,UserId= bodyEdu.UserId,Stamp=uniqFileName
                };
                _context.Add(educationalBody);
                await _context.SaveChangesAsync();
                _f.Flash("success", "تم الحفظ بنجاح");
                return RedirectToAction(nameof(Index));
            }
            var type = new List<EntityTypeForEducation>();

            var Institutes = new EntityTypeForEducation() { Name = "المعاهد والدور" };
            type.Add(Institutes);
            var Colleges = new EntityTypeForEducation() { Name = "الكليات" };
            type.Add(Colleges);

            ViewData["type"] = new SelectList(type, "Name", "Name",bodyEdu.EntityType);
            ViewData["UserId"] = new SelectList(_context.Users.Include(u => u.UserRoles).Where(r => r.UserRoles.Any(a => a.Role.Name == "Supervisor")), "Id", "FullName", bodyEdu.UserId);
            return View(bodyEdu);
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

            var budyEdu = new EducationBodyEditViewModel()
            {
                    City= educationalBody.City,EntityType= educationalBody.EntityType,
                    Id= educationalBody.Id,Name= educationalBody.Name,Stamp= educationalBody.Stamp,
                    UserId= educationalBody.UserId

            };

            var type = new List<EntityTypeForEducation>();

            var Institutes = new EntityTypeForEducation() { Name = "المعاهد والدور" };
            type.Add(Institutes);
            var Colleges = new EntityTypeForEducation() { Name = "الكليات" };
            type.Add(Colleges);
            ViewData["type"] = new SelectList(type, "Name", "Name", educationalBody.EntityType);

            ViewData["UserId"] = new SelectList(_context.Users.Include(u => u.UserRoles).Where(r => r.UserRoles.Any(a => a.Role.Name == "Supervisor")), "Id", "FullName", educationalBody.UserId);


            return View(budyEdu);
        }

        // POST: Admin/EducationalBodies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EducationBodyEditViewModel bodyEdu)
        {
            if (id != bodyEdu.Id)
            {
                Response.StatusCode = 404;
                return View("EducationalNotFound");
            }

            if (ModelState.IsValid)
            {
                string uniqFileName = null;
                string filenameForEdit = null;
                if (bodyEdu.StampFile != null && bodyEdu.StampFile.Length > 0)
                {
                    if (bodyEdu.Stamp != null)
                    {
                        string filePathForDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
                         "img/stamps", bodyEdu.Stamp);
                        System.IO.File.Delete(filePathForDelete);
                    }
                    if (IsFileValidate(bodyEdu.StampFile.FileName))
                    {
                        string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/stamps");
                        uniqFileName = Guid.NewGuid().ToString() + "_" + bodyEdu.StampFile.FileName;
                        string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            bodyEdu.StampFile.CopyTo(fileStream);
                        }


                    }
                    else
                    {
                        ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";

                        return View(bodyEdu);
                    }
                }
                filenameForEdit = uniqFileName == null ? bodyEdu.Stamp : uniqFileName;
                var eduEdit = new EducationalBody()
                {
                    City=bodyEdu.City,Stamp= filenameForEdit,
                    UserId=bodyEdu.UserId,EntityType=bodyEdu.EntityType,
                    Id=bodyEdu.Id,Name=bodyEdu.Name
                };
                _context.Update(eduEdit);
                _f.Flash("success", "تم التعديل بنجاح");
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            var type = new List<EntityTypeForEducation>();

            var Institutes = new EntityTypeForEducation() { Name = "المعاهد والدور" };
            type.Add(Institutes);
            var Colleges = new EntityTypeForEducation() { Name = "الكليات" };
            type.Add(Colleges);
            ViewData["type"] = new SelectList(type, "Name", "Name", bodyEdu.EntityType);

            ViewData["UserId"] = new SelectList(_context.Users.Include(u => u.UserRoles).Where(r => r.UserRoles.Any(a => a.Role.Name == "Supervisor")), "Id", "FullName", bodyEdu.UserId);
            return View(bodyEdu);
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
            if (educationalBody.Stamp != null)
            {
                string filePathForDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
                 "img/stamps", educationalBody.Stamp);
                System.IO.File.Delete(filePathForDelete);
            }
            await _context.SaveChangesAsync();
            _f.Flash("success", "تم الحذف بنجاح");
            return RedirectToAction(nameof(Index));
        }

        private bool EducationalBodyExists(int id)
        {
            return _context.EducationalBodies.Any(e => e.Id == id);
        }

        private bool IsFileValidate(string filename)
        {
            string extintion = Path.GetExtension(filename);
            if (extintion.Contains(".png")) { return true; }
            if (extintion.Contains(".PNG")) { return true; }
            if (extintion.Contains(".jpeg")) { return true; }
            if (extintion.Contains(".jpg")) { return true; }
            if (extintion.Contains(".gif")) { return true; }
            if (extintion.Contains(".bmp")) { return true; }
            return false;
        }
    }
}
