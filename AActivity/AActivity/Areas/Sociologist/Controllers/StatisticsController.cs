using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AActivity.Data;
using AActivity.Areas.Sociologist.ModelViews;
using Microsoft.EntityFrameworkCore;
using AActivity.Models;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<ActionResult> StatisticsAll()
        //{
        //    var st = new List<StatisticsModelView>();
        //    var scdualtripall = await _context.SchedulingTripDetails
        //      .Include(b => b.EducationalBody)
        //      .Include(t => t.TripType)
        //      .Include(t => t.TripBookings)
        //      .ThenInclude(l => l.Letters)
        //      .ThenInclude(t => t.LetterTransports)
        //      .Where(d => d.TripBookings.Any(f => f.TripStatus == 1))
        //      .ToListAsync();

        //    foreach (var mo in scdualtripall)
        //    {
        //        var model = new StatisticsModelView()
        //        {
        //            EducationBody = mo.EducationalBody,
        //            letterTransport = await _context.Letters.wh.ToListAsync(),
                    
        //        };
        //    }
        //    return View(st);
        //}
        //// GET: Statistics
        public async Task<ActionResult> Index()
        {
            ViewData["Edus"] = await _context.EducationalBodies.ToListAsync();
            ViewData["LetterTransports"] = await _context.LetterTransports.Include(l=>l.Letter).ToListAsync();
            return View(await Statistics());
        }

        private async Task<IList<SchedulingTripDetail>> Statistics()
        {
            
            var scdualtripall = await _context.SchedulingTripDetails
                .Include(b=>b.EducationalBody)
                .Include(t=>t.TripType)               
                .Include(t=>t.TripBookings)
                .ThenInclude(l=>l.Letters)
                .ThenInclude(t=>t.LetterTransports) 
                .Where(d=>d.TripBookings.Any(f=>f.TripStatus==1))
                .ToListAsync();          
                return scdualtripall;
            }


        }
    }
