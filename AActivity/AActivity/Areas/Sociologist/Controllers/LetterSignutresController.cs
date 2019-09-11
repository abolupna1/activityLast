using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class LetterSignutresController : Controller
    {
        private readonly ApplicationDbContext _context;
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }

        public LetterSignutresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Sociologist/LetterSignutres/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int WhoHasSignutre,int LetterId,
            int SignatureId,int IsHeOwnerOfSignature,string ControllerName)
        {
            bool isSignutred = await _context.LetterSignutres.AnyAsync(l=>l.LetterId == LetterId && l.WhoHasSignutre == WhoHasSignutre);
            if (!isSignutred)
            {
                bool status = IsHeOwnerOfSignature == 0 ? false : true;
                var sig = new LetterSignutre()
                {
                    WhoHasSignutre = WhoHasSignutre,
                    LetterId = LetterId,
                    IsHeOwnerOfSignature = status,
                    SignatureId = SignatureId
                };

                _context.Add(sig);
                await _context.SaveChangesAsync();
                MessageSuccess = "تم التوقيع بنجاح";
                return RedirectToAction("Details", ControllerName, new { letterId = LetterId });

            }
            else
            {
                MessageError = "يوجد توقيع مسبق لهذا الخطاب";
                return RedirectToAction("Details", ControllerName, new { letterId = LetterId });

            }



        }

        



    }
}
