using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AActivity.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using AActivity.Data;
using AActivity.Areas.Sociologist.Helpers;
using System.Security.Claims;

namespace AActivity.Controllers
{
    [Area("Sociologist")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> getSignutreCount()
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    return Ok(1);
        //}
        public async Task<IActionResult> Index()
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var sd = SignutreOfUserHelper.getUserSignutre(userId, _context);
            ViewBag.Signutre = sd;
            
            var sig = _context.Signatures.FindAsync(sd);
            if (sig.Result != null)
            {
                if (sig.Result.SignatureRole == "مفوض")
                {
                    ViewBag.SignutreName = false;

                }
                else
                {
                    ViewBag.SignutreName = true;

                }


            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
