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
    public class LetterSignutreForAdvancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        public LetterSignutreForAdvancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Sociologist/LetterSignutreForAdvances/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int WhoHasSignutre, int LetterId,
            int LetterAdvancedDelegationId,
           int SignatureId, int IsHeOwnerOfSignature, string ControllerName)
        {
            bool isSignutred = await _context.LetterSignutreForAdvances.AnyAsync(l => l.LetterAdvancedDelegationId == LetterAdvancedDelegationId && l.WhoHasSignutre == WhoHasSignutre);
            if (!isSignutred)
            {
                bool status = IsHeOwnerOfSignature == 0 ? false : true;
                var sig = new LetterSignutreForAdvance()
                {
                    WhoHasSignutre = WhoHasSignutre,
                    LetterAdvancedDelegationId = LetterAdvancedDelegationId,
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
