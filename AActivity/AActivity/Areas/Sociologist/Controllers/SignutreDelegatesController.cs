using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using System.Security.Claims;
using AActivity.Areas.Sociologist.Helpers;
using System.Globalization;
using AActivity.Areas.Admin.Helpers;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class SignutreDelegatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SignutreDelegatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sociologist/SignutreDelegates
        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var sd = SignutreOfUserHelper.getUserSignutre(userId, _context);
            if (sd==0) ViewData["notSignutre"] = "لايوجد لك توقيع !!";
            var applicationDbContext = _context.SignutreDelegates.Where(e=>e.SignatureId==sd ).Include(s => s.Signature).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/SignutreDelegates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SignutreDelegatesNotFound");
            }

            var signutreDelegate = await _context.SignutreDelegates
                .Include(s => s.Signature)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signutreDelegate == null)
            {
                return NotFound();
            }

            return View(signutreDelegate);
        }

        // GET: Sociologist/SignutreDelegates/Create
        public IActionResult Create()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var signutre = _context.Signatures.FindAsync(SignutreOfUserHelper.getUserSignutre(userId, _context));
            ViewData["SignatureId"] = signutre.Result.Id;
           var cv = _context.Signatures.Where(u => u.UserId != userId).Include(u => u.User).Select(u => new { Id = u.UserId, Name = u.User.FullName }).ToList();
            ViewData["UserId"] = new SelectList(cv, "Id", "Name");
            ViewData["Status"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name");
            
            return View();
        }

        // POST: Sociologist/SignutreDelegates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SignatureId,UserId,StartAtDate,EndAtDate,Status")] SignutreDelegate signutreDelegate)
        {
            if (ModelState.IsValid)
            {
                if (!SignutreOfUserHelper.getUserHasSignutre(signutreDelegate.UserId, signutreDelegate.SignatureId, _context))
                {
                    signutreDelegate.TimeStamp = DateTime.Now;
                    _context.Add(signutreDelegate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["SignutreExist"] = "يوجد تفويض مسبقا";
                }
               
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var signutre = _context.Signatures.FindAsync(SignutreOfUserHelper.getUserSignutre(userId, _context));
            ViewData["SignatureId"] = signutre.Result.Id;
            var cv = _context.Signatures.Where(u => u.UserId != userId).Include(u => u.User).Select(u => new { Id = u.UserId, Name = u.User.FullName }).ToList();
            ViewData["UserId"] = new SelectList(cv, "Id", "Name");
            ViewData["Status"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name");

            return View(signutreDelegate);
        }

        // GET: Sociologist/SignutreDelegates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SignutreDelegatesNotFound");
            }

            var signutreDelegate = await _context.SignutreDelegates.FindAsync(id);
            if (signutreDelegate == null)
            {
                Response.StatusCode = 404;
                return View("SignutreDelegatesNotFound");
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var signutre = _context.Signatures.FindAsync(SignutreOfUserHelper.getUserSignutre(userId, _context));
            ViewData["SignatureId"] = signutre.Result.Id;
            var cv = _context.Signatures.Where(u => u.UserId != userId && u.UserId== signutreDelegate.UserId).Include(u => u.User).Select(u => new { Id = u.UserId, Name = u.User.FullName }).ToList();
            ViewData["UserId"] = new SelectList(cv, "Id", "Name", signutreDelegate.UserId);
            ViewData["Status"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name", signutreDelegate.Status);

            return View(signutreDelegate);
        }

        // POST: Sociologist/SignutreDelegates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SignatureId,UserId,StartAtDate,EndAtDate,Status,TimeStamp")] SignutreDelegate signutreDelegate)
        {
            if (id != signutreDelegate.Id)
            {
                Response.StatusCode = 404;
                return View("SignutreDelegatesNotFound");
            }

            if (ModelState.IsValid)
            {
                if (SignutreOfUserHelper.getUserHasToEditSignutre(id,signutreDelegate.UserId,signutreDelegate.SignatureId,_context))
                {
                    _context.Update(signutreDelegate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
          
            }
         
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var signutre = _context.Signatures.FindAsync(SignutreOfUserHelper.getUserSignutre(userId, _context));
            ViewData["SignatureId"] = signutre.Result.Id;
            var cv = _context.Signatures.Where(u => u.UserId != userId).Include(u => u.User).Select(u => new { Id = u.UserId, Name = u.User.FullName }).ToList();
            ViewData["UserId"] = new SelectList(cv, "Id", "Name", signutreDelegate.UserId);
            ViewData["Status"] = new SelectList(ValidUnValidHelpr.ValidUnValidHelprList(), "Value", "Name", signutreDelegate.Status);

            return View(signutreDelegate);
        }

        // GET: Sociologist/SignutreDelegates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("SignutreDelegatesNotFound");
            }

            var signutreDelegate = await _context.SignutreDelegates
                .Include(s => s.Signature)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signutreDelegate == null)
            {
                Response.StatusCode = 404;
                return View("SignutreDelegatesNotFound");
            }

            return View(signutreDelegate);
        }

        // POST: Sociologist/SignutreDelegates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signutreDelegate = await _context.SignutreDelegates.FindAsync(id);
            _context.SignutreDelegates.Remove(signutreDelegate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignutreDelegateExists(int id)
        {
            return _context.SignutreDelegates.Any(e => e.Id == id);
        }
    }
}
