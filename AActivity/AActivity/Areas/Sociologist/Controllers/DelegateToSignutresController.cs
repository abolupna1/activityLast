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
using AActivity.Areas.Sociologist.ModelViews;
using AActivity.Areas.Admin.ModelViews;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    [Route("Sociologist/DelegateToSignutres/")]

    public class DelegateToSignutresController : Controller
    {
        private readonly ApplicationDbContext _context;
      

        public DelegateToSignutresController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: Sociologist/DelegateToSignutres
        [Route("IndexByWonerSignutre/{signutreId:int}")]
        public async Task<IActionResult> IndexByWonerSignutre(int signutreId)
        {
            var applicationDbContext = _context.DelegateToSignutres
                .Where(s=>s.WonerSignutreId==signutreId)
                .Include(d => d.DelegatedToSignutre).ThenInclude(d => d.User)
                .Include(d => d.WonerSignutre).ThenInclude(d=>d.User);

            ViewBag.signutreId = signutreId;
            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sociologist/DelegateToSignutres/Details/5
        [Route("Details/{userId:int}")]
        public async Task<IActionResult> Details(int userId)
        {
            var signutre = await _context.Signatures
              .Include(t => t.User)
              .Include(t => t.JobsSignatorie)
              .Include(t => t.DelegateToSignutres).ThenInclude(f=>f.WonerSignutre.User)
              .Include(t => t.DelegateToSignutres).ThenInclude(f=>f.WonerSignutre.JobsSignatorie)

              .Include(t => t.WonerSignutres).ThenInclude(v=>v.DelegatedToSignutre.User)
              .Include(t => t.WonerSignutres).ThenInclude(v=>v.DelegatedToSignutre.JobsSignatorie)
              .Include(t => t.TypesOfLettersAndSignatures)
              .ThenInclude(f => f.TypesOfletter)
              .ThenInclude(f => f.TypesOfLettersAndSignatures)
              .FirstOrDefaultAsync(u => u.UserId == userId);

            if (signutre == null)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            var typelist = await _context.TypesOfletters.Include(t => t.TypesOfLettersAndSignatures).ToListAsync();
            var model = new List<DelegateToSignutreModelView>();
            foreach (var t in typelist)
            {
                IsHasSignutreTypeLetter(signutre.Id, t.Id, out bool isSelected, out int TypesOfLettersAndSignaturesId);
                if (isSelected)
                {
                    var m = new DelegateToSignutreModelView()
                    {
                        TypesOfletterName=t.Name
                    };
                    model.Add(m);
                }
               
            }
            ViewData["TypesOfletters"] = model;
            return View(signutre);
        }


        private void IsHasSignutreTypeLetter(int sigId, int typeId, 
            out bool isSelected ,out int TypesOfLettersAndSignaturesId)
        {

            var signutrestypes = _context.TypesOfLettersAndSignatures
                .Where(s => s.SignatureId == sigId && s.TypesOfletterId == typeId)
                .FirstOrDefault();
            if (signutrestypes != null)
            {
                isSelected = true;
                TypesOfLettersAndSignaturesId = signutrestypes.Id;
            }
            else
            {
                isSelected = false;
                TypesOfLettersAndSignaturesId = 0;

            }
        }
        [Route("Create/{signutreId:int}")]
        public async Task<IActionResult> Create(int signutreId)
        {
            var sig = await _context.Signatures.FindAsync(signutreId);
            if (sig == null)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            var sinutres = await _context.Signatures.Where(s => s.Id != signutreId).Select(s=>new {Id=s.Id , Name =s.User.FullName}).ToListAsync();
         
            ViewData["DelegatedToSignutreId"] = new SelectList(sinutres, "Id", "Name");
            ViewBag.signutreId = signutreId;
            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }

        // POST: Sociologist/DelegateToSignutres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create/{signutreId:int}")]
        public async Task<IActionResult> Create(int signutreId,
            [Bind("Id,WonerSignutreId,DelegatedToSignutreId,DateDelegate,Status")] DelegateToSignutre delegateToSignutre)
        {
            if (ModelState.IsValid)
            {
                var ds= await _context.DelegateToSignutres.ToListAsync();
                delegateToSignutre.Id = ds.Max(i=>i.Id)+ 1;
                delegateToSignutre.Status = true;

                 var delegates = await _context.DelegateToSignutres
                    .Where(s => s.Status && s.WonerSignutreId == delegateToSignutre.WonerSignutreId)
                    .ToListAsync();
                var model = new List<DelegateToSignutre>();
                foreach (var del in delegates)
                {
                    del.Status = false;
                    model.Add(del);
                }


                
               
                _context.RemoveRange(CancelTypesOfLettersAndSignatures(signutreId).Result);
                _context.AddRange(AddTypesOfLettersAndSignatures(signutreId, delegateToSignutre.DelegatedToSignutreId).Result);
                _context.UpdateRange(model);
                _context.Add(delegateToSignutre);
                await _context.SaveChangesAsync();
              int userId=  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
             
                

                return RedirectToAction(nameof(Details),new { userId = userId });
            }
            var sinutres = await _context.Signatures.Where(s => s.Id != signutreId).Select(s => new { Id = s.Id, Name = s.User.FullName }).ToListAsync();
            ViewData["DelegatedToSignutreId"] = new SelectList(sinutres, "Id", "Name", delegateToSignutre.DelegatedToSignutreId);
            ViewBag.signutreId = signutreId;
            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(delegateToSignutre);
        }


        private async Task<List<TypesOfLettersAndSignature>> CancelTypesOfLettersAndSignatures(int signutreId)
        {
            var delegated = await _context.TypesOfLettersAndSignatures
               .Where(s => s.SignatureId == signutreId)
               .ToListAsync();
            var allDelegated = await _context.TypesOfLettersAndSignatures.Where(d => d.WonerSignatureId != null).ToListAsync();
            var modelToRemove = new List<TypesOfLettersAndSignature>();
            foreach (var woner in delegated)
            {
                foreach (var del in allDelegated.Where(d=>d.WonerSignatureId == woner.Id))
                {
                    modelToRemove.Add(del);
                }
           
            }
           
            return modelToRemove;
        }

        private async Task<List<TypesOfLettersAndSignature>> AddTypesOfLettersAndSignatures(int signutreId,int delegatednew)
        {
            var delegated = await _context.TypesOfLettersAndSignatures
              .Where(s => s.SignatureId == signutreId)
              .ToListAsync();
            var modelToAdd = new List<TypesOfLettersAndSignature>();

            foreach (var woner in delegated)
            {
                var added = new TypesOfLettersAndSignature()
                {
                    SignatureId= delegatednew,
                    IsSignatureOwner=false,
                    StartAtDate=DateTime.Now,
                    TypesOfletterId=woner.TypesOfletterId,
                    WonerSignatureId=woner.Id,
                    
                };
                modelToAdd.Add(added);

            }
          

            return modelToAdd;
        }


        [ Route("DelegateCancled/{id:int}")]
        public async Task<IActionResult> DelegateCancled(int id)
        {
         

            var delegateToSignutre = await _context.DelegateToSignutres
                .Include(d => d.DelegatedToSignutre).ThenInclude(f=>f.User)
                .Include(d => d.WonerSignutre).ThenInclude(g=>g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delegateToSignutre == null)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }

            ViewBag.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(delegateToSignutre);
        }
        [Route("DeleteConfirmed/{id:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delegateToSignutre = await _context.DelegateToSignutres.SingleOrDefaultAsync(d=>d.Id==id);
            if (delegateToSignutre==null)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            delegateToSignutre.Status = false;
            _context.DelegateToSignutres.Update(delegateToSignutre);
            _context.RemoveRange(CancelTypesOfLettersAndSignatures(delegateToSignutre.WonerSignutreId).Result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexByWonerSignutre),new { signutreId= delegateToSignutre.WonerSignutreId });
        }

        private bool DelegateToSignutreExists(int id)
        {
            return _context.DelegateToSignutres.Any(e => e.WonerSignutreId == id);
        }
    }
}
