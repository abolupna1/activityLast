using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using AActivity.Areas.Admin.ModelViews;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypesOfLettersAndSignaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypesOfLettersAndSignaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: 
  

        [Route("Admin/TypesOfLettersAndSignatures/IndexBysignutre/{signutreId:int}")]
        public async Task<IActionResult> IndexBysignutre(int signutreId)
        {
            var sig = await _context.Signatures.Include(u=>u.User)
                .FirstOrDefaultAsync(s=>s.Id== signutreId);
            ViewBag.Signutre = sig != null ? sig.User.FullName :"";
            ViewBag.SignutreId = sig != null ? sig.Id :0;


            var typelist = await _context.TypesOfletters.Include(t=>t.TypesOfLettersAndSignatures).ToListAsync();
            var model = new List<LettersAndSignaturesModelView>();
            foreach (var t in typelist)
            {
                IsHasSignutreTypeLetter(sig.Id, t.Id, out bool isSelected , out int typeOfLetterAndSignutreId);
                var typeletter = new LettersAndSignaturesModelView()
                {
                    TypesOfletterId=t.Id,
                    TypesOfletterName=t.Name,
                    IsSelected = isSelected,
                    TypeOfLetterAndSignutreId= typeOfLetterAndSignutreId

                };
              
                model.Add(typeletter);
            }

            return View(model);
        }

        private void IsHasSignutreTypeLetter(int sigId,int typeId ,out bool isSelected  , out int typeOfLetterAndSignutreId)
        {
           
            var signutrestypes =  _context.TypesOfLettersAndSignatures
                .Where(s=>s.SignatureId==sigId && s.IsSignatureOwner   && s.TypesOfletterId == typeId )
                .FirstOrDefault();
            if (signutrestypes != null)
            {
                isSelected = true;
                typeOfLetterAndSignutreId = signutrestypes.Id;
            }
            else
            {
                isSelected = false;
                typeOfLetterAndSignutreId = 0;
            }
        }

        [HttpPost]
        [Route("Admin/TypesOfLettersAndSignatures/IndexBysignutre/{signutreId:int}")]
        public async Task<IActionResult> IndexBysignutre(int signutreId,List<LettersAndSignaturesModelView> model )
        {
            if (ModelState.IsValid)
            {
                var typeOfLettersToCreate = new List<TypesOfLettersAndSignature>();
                var typeOfLettersToDelete = new List<TypesOfLettersAndSignature>();

                foreach (var m in model)
                {
                    var typeOfLetter = new TypesOfLettersAndSignature()
                    {
                        SignatureId = signutreId,
                        IsSignatureOwner = true,
                        StartAtDate = DateTime.Now,
                        TypesOfletterId=m.TypesOfletterId,
                        WonerSignatureId=null,
                        Id = m.TypeOfLetterAndSignutreId
                    };
                    if (m.TypeOfLetterAndSignutreId == 0 && m.IsSelected)
                    {
                        typeOfLettersToCreate.Add(typeOfLetter);
                    }
                    else if (m.TypeOfLetterAndSignutreId > 0 && m.IsSelected == false)
                    {
                        typeOfLettersToDelete.Add(typeOfLetter);
                    }
                }
                _context.AddRange(typeOfLettersToCreate);
                _context.RemoveRange(typeOfLettersToDelete);
                _context.RemoveRange(CancelTypesOfLettersAndSignatures(signutreId).Result);
                if (CancelDelegateToSignutre(signutreId).Result!=null)
                {
                    _context.Update(CancelDelegateToSignutre(signutreId).Result);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Signatures");

            }

            var sig = await _context.Signatures.Include(u => u.User)
              .FirstOrDefaultAsync(s => s.Id == signutreId);
            ViewBag.Signutre = sig != null ? sig.User.FullName : "";
            ViewBag.SignutreId = sig != null ? sig.Id : 0;
            return View(model);
        }


        private async Task<DelegateToSignutre> CancelDelegateToSignutre(int wonerSignutreId)
        {
            var delegateToSignutre = await _context.DelegateToSignutres.
                SingleOrDefaultAsync(d =>d.WonerSignutreId == wonerSignutreId && d.Status);
            if (delegateToSignutre!=null)
            {
                delegateToSignutre.Status = false;

            }
            return delegateToSignutre;
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
                foreach (var del in allDelegated.Where(d => d.WonerSignatureId == woner.Id))
                {
                    modelToRemove.Add(del);
                }

            }

            return modelToRemove;
        }


    }
}
