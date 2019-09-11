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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AppSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _ihostingEnvironment;

        public AppSettingsController(ApplicationDbContext context, IHostingEnvironment ihostingEnvironment)
        {
            _context = context;
            _ihostingEnvironment = ihostingEnvironment;
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
        public async Task<IActionResult> Create(AppSettingCreateModelView appSetting)
        {
            if (ModelState.IsValid)
            {
                string uniqFileName = null;
                if (appSetting.StampFile != null && appSetting.StampFile.Length>0)
                {
                    if (IsFileValidate(appSetting.StampFile.FileName))
                    {
                        string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/stamps");
                        uniqFileName = Guid.NewGuid().ToString() + "_" + appSetting.StampFile.FileName;
                        string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            appSetting.StampFile.CopyTo(fileStream);
                        }


                    }
                    else
                    {
                        ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";

                        return View(appSetting);
                    }
                }
                var settengSave = new AppSetting()
                {
                    AmountExternalCreditToTrip=appSetting.AmountExternalCreditToTrip,
                    Stamp= uniqFileName,BookingTime= appSetting.BookingTime,
                    AmountInternalCreditToTrip= appSetting.AmountInternalCreditToTrip,
                    AmountOmrahCreditToTrip= appSetting.AmountOmrahCreditToTrip,
                    AmountVisitCreditToTrip= appSetting.AmountVisitCreditToTrip,
                    QtyCollegesDelegates= appSetting.QtyCollegesDelegates,
                    QtyDeanshipDelegates= appSetting.QtyDeanshipDelegates,
                    QtyExternalDaysTrip= appSetting.QtyExternalDaysTrip,
                    QtyInstitutesDelegates = appSetting.QtyInstitutesDelegates,
                    QtyInternalDaysTrip= appSetting.QtyInternalDaysTrip,
                    QtyOmrahDaysTrip= appSetting.QtyOmrahDaysTrip,
                    QtyPassengersInOneBus= appSetting.QtyPassengersInOneBus,
                    
                };
                _context.Add(settengSave);
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
            var appSettingEdit = new AppSettingCreateModelView()
            {
                AmountExternalCreditToTrip = appSetting.AmountExternalCreditToTrip,
                Stamp = appSetting.Stamp,
                Id =appSetting.Id,
                BookingTime = appSetting.BookingTime,
                AmountInternalCreditToTrip = appSetting.AmountInternalCreditToTrip,
                AmountOmrahCreditToTrip = appSetting.AmountOmrahCreditToTrip,
                AmountVisitCreditToTrip = appSetting.AmountVisitCreditToTrip,
                QtyCollegesDelegates = appSetting.QtyCollegesDelegates,
                QtyDeanshipDelegates = appSetting.QtyDeanshipDelegates,
                QtyExternalDaysTrip = appSetting.QtyExternalDaysTrip,
                QtyInstitutesDelegates = appSetting.QtyInstitutesDelegates,
                QtyInternalDaysTrip = appSetting.QtyInternalDaysTrip,
                QtyOmrahDaysTrip = appSetting.QtyOmrahDaysTrip,
                QtyPassengersInOneBus = appSetting.QtyPassengersInOneBus,
            };
            return View(appSettingEdit);
        }

        // POST: Admin/AppSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppSettingCreateModelView appSetting)
        {
            if (id != appSetting.Id)
            {
                Response.StatusCode = 404;
                return View("AppSettingsNotFound");
            }

            if (ModelState.IsValid)
            {
                string filenameForEdit = null;
                string uniqFileName = null;
                if (appSetting.StampFile != null && appSetting.StampFile.Length > 0)
                {
                    string filePathForDelete = Path.Combine(_ihostingEnvironment.WebRootPath,
              "img/stamps", appSetting.Stamp);
                    System.IO.File.Delete(filePathForDelete);
                    if (IsFileValidate(appSetting.StampFile.FileName))
                    {
                        string uplouadsFolder = Path.Combine(_ihostingEnvironment.WebRootPath, "img/stamps");
                        uniqFileName = Guid.NewGuid().ToString() + "_" + appSetting.StampFile.FileName;
                        string filePath = Path.Combine(uplouadsFolder, uniqFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            appSetting.StampFile.CopyTo(fileStream);
                        }


                    }
                    else
                    {
                        ViewBag.msg = "الصور المسموح بها يجب ان تكون بمتداد : " + "png , jpeg , jpg , gif , bmp ";

                        return View(appSetting);
                    }
                }
                filenameForEdit = uniqFileName == null ? appSetting.Stamp : uniqFileName;
                var settengUpdate = new AppSetting()
                {
                    AmountExternalCreditToTrip = appSetting.AmountExternalCreditToTrip,
                    Stamp = filenameForEdit,Id=appSetting.Id,
                    BookingTime = appSetting.BookingTime,
                    AmountInternalCreditToTrip = appSetting.AmountInternalCreditToTrip,
                    AmountOmrahCreditToTrip = appSetting.AmountOmrahCreditToTrip,
                    AmountVisitCreditToTrip = appSetting.AmountVisitCreditToTrip,
                    QtyCollegesDelegates = appSetting.QtyCollegesDelegates,
                    QtyDeanshipDelegates = appSetting.QtyDeanshipDelegates,
                    QtyExternalDaysTrip = appSetting.QtyExternalDaysTrip,
                    QtyInstitutesDelegates = appSetting.QtyInstitutesDelegates,
                    QtyInternalDaysTrip = appSetting.QtyInternalDaysTrip,
                    QtyOmrahDaysTrip = appSetting.QtyOmrahDaysTrip,
                    QtyPassengersInOneBus = appSetting.QtyPassengersInOneBus,

                };
                _context.Update(settengUpdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appSetting);
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

        private bool AppSettingExists(int id)
        {
            return _context.AppSettings.Any(e => e.Id == id);
        }
    }
}
