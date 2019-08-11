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
using AActivity.Areas.Admin.ModelViews;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AppSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AppSettings
        public async Task<IActionResult> Index()
        {

            return View(await _context.AppSettings.Take(1).ToListAsync());
        }

    
        // GET: Admin/AppSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingTime,QtyDeanshipDelegates,QtyInstitutesDelegates,QtyCollegesDelegates,QtyPassengersInOneBus,QtyOmrahDaysTrip,QtyInternalDaysTrip,QtyExternalDaysTrip,AmountExternalCreditToTrip,AmountInternalCreditToTrip,AmountOmrahCreditToTrip")] AppSetting appSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appSetting);
        }

        // GET: Admin/AppSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("AppSettingsNotFound");
            }

            var appSetting = await _context.AppSettings.FindAsync(id);
            if (appSetting == null)
            {
                Response.StatusCode = 404;
                return View("AppSettingsNotFound");
            }
            return View(appSetting);
        }

        // POST: Admin/AppSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingTime,QtyDeanshipDelegates,QtyInstitutesDelegates,QtyCollegesDelegates,QtyPassengersInOneBus,QtyOmrahDaysTrip,QtyInternalDaysTrip,QtyExternalDaysTrip,AmountExternalCreditToTrip,AmountInternalCreditToTrip,AmountOmrahCreditToTrip")] AppSetting appSetting)
        {
            if (id != appSetting.Id)
            {
                Response.StatusCode = 404;
                return View("AppSettingsNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppSettingExists(appSetting.Id))
                    {
                        Response.StatusCode = 404;
                        return View("AppSettingsNotFound");
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return View("AppSettingsNotFound");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appSetting);
        }


 

        private bool AppSettingExists(int id)
        {
            return _context.AppSettings.Any(e => e.Id == id);
        }
    }
}
