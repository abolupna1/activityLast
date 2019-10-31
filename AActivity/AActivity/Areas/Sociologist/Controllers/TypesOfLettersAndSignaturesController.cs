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
    public class TypesOfLettersAndSignaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypesOfLettersAndSignaturesController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: /TypesOfLettersAndSignatures/Details/5
        [Route("Sociologist/TypesOfLettersAndSignatures/Details/{userId:int}")]
        public async Task<IActionResult> Details(int userId)
        {

            var signutre = await _context.Signatures
                .Include(t=>t.User)
                .Include(t=>t.JobsSignatorie)
                .Include(t=>t.TypesOfLettersAndSignatures)
                .ThenInclude(f=>f.TypesOfletter)
                .ThenInclude(f=>f.TypesOfLettersAndSignatures)
                .FirstOrDefaultAsync(u=>u.UserId==userId);
           
          

            return View(signutre);
        }

       
    }
}
