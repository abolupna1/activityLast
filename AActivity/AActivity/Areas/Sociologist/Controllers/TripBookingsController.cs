using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AActivity.Data;
using AActivity.Models;
using AActivity.Areas.Sociologist.ModelViews;
using System.Security.Claims;

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class TripBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

   
        private async Task<List<City>> cityName(string type)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
          
            if (type == "عمرة")
            {
                if (User.IsInRole("Admin"))
                {
                    return await _context.Cities
                         .Where(c => c.LocationName == "مكة المكرمة" ||
                                    c.LocationName == "المدينة المنورة")
                        .OrderByDescending(e => e.Id).ToListAsync();
                }
                var edu = await _context.EducationalBodies.FirstOrDefaultAsync(s => s.UserId == userId);
                return await _context.Cities
                    .Where(c => c.LocationName != edu.City )
                    .Take(1)
                    .OrderBy(e => e.Id).ToListAsync();

            }
            else if (type == "داخلية")
            {
                if (User.IsInRole("Admin"))
                {
                    return await _context.Cities.OrderByDescending(e => e.Id).ToListAsync();
                }
                var edu = await _context.EducationalBodies.FirstOrDefaultAsync(s => s.UserId == userId);
                return await _context.Cities
                    .Where(c => c.LocationName == edu.City).ToListAsync();

            }
            else if (type == "خارجية")
            {               
                return await _context.Cities.Skip(2)
                    .OrderByDescending(e => e.Id).ToListAsync();
            }
            else if (type == "زيارة")
            {
                return await _context.Cities.OrderByDescending(e => e.Id).ToListAsync();

            }
            else
            {
                return await _context.Cities.Take(0).ToListAsync();
            }

        }

        // GET: Sociologist/TripBookings/Create
        [Route("Sociologist/TripBookings/Create/{tripId:int}")]
        public async Task< IActionResult> Create(int tripId)
        {

         
            var trip = await _context.SchedulingTripDetails
                .Include(e=>e.EducationalBody)
                .Include(t=>t.TripType)
                .FirstOrDefaultAsync(i=>i.Id == tripId);
            if (trip == null)
            {
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }
            var apSetting = await _context.AppSettings.FirstOrDefaultAsync();
            int qtDays = 0;
            if (apSetting!=null) { 
            if (trip.TripType.Name == "عمرة")
            {
                ViewData["CityId"] = new SelectList(await cityName("عمرة"), "Id", "LocationName");
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.TripType.Name == "داخلية")
            {
                ViewData["CityId"] = new SelectList(await cityName("داخلية"), "Id", "LocationName");
                qtDays = apSetting.QtyInternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.TripType.Name == "خارجية")
            {
                ViewData["CityId"] = new SelectList(await cityName("خارجية"), "Id", "LocationName");
                qtDays = apSetting.QtyExternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.TripType.Name == "زيارة")
            {
                ViewData["CityId"] = new SelectList(await cityName("زيارة"), "Id", "LocationName");
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            }
            else
            {

                ViewBag.appsettingNull = "انتظار ضبط اعدادات الرحلة الرجاء الاتصال بالمسؤول";
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }

            var tripView = new TripBookingCreateModelView()
            {
                EducationName=trip.EducationalBody.Name,
                EducationCity=trip.EducationalBody.City,
                QtyDaysVisitInternal = apSetting.QtyDaysVisitInternal,
                QtyDaysVisitEternal = apSetting.QtyDaysVisitEternal,
                SchedulingTripDetailId =trip.Id,
                TripTime=trip.TripDate,
                TripType=trip.TripType.Name,
                QtyDays=qtDays
            };
        
            return View(tripView);
        }

        private TripBooking TripBook(TripBookingCreateModelView tripBooking)
        {
            var tripbok = new TripBooking()
            {
                Id = tripBooking.Id,
                SchedulingTripDetailId = tripBooking.SchedulingTripDetailId,
                CityId = tripBooking.CityId,
                TripToDate = tripBooking.TripTime.AddDays(tripBooking.TripQtyDays),
                TripQtyDays=tripBooking.TripQtyDays,
                TripLocationName=tripBooking.TripLocationName,
                TripTypeName=tripBooking.TripTypeName,

            };



            return tripbok;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/TripBookings/Create/{tripId:int}")]
        public async Task<IActionResult> Create(int tripId, TripBookingCreateModelView tripBooking)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(TripBook(tripBooking));
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id= tripId });
            }
            var apSetting = await _context.AppSettings.FirstOrDefaultAsync();
            int qtDays = 0;

            var trip = await _context.SchedulingTripDetails
                .Include(e => e.EducationalBody)
                .Include(t => t.TripType)
                .FirstOrDefaultAsync(i => i.Id == tripId);
            if (trip.TripType.Name == "عمرة")
            {
                ViewData["CityId"] = new SelectList(await cityName("عمرة"), "Id", "LocationName",tripBooking.CityId);
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.TripType.Name == "داخلية")
            {
                ViewData["CityId"] = new SelectList(await cityName("داخلية"), "Id", "LocationName", tripBooking.CityId);
                qtDays = apSetting.QtyInternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.TripType.Name == "خارجية")
            {
                ViewData["CityId"] = new SelectList(await cityName("خارجية"), "Id", "LocationName", tripBooking.CityId);
                qtDays = apSetting.QtyExternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.TripType.Name == "زيارة")
            {
                ViewData["CityId"] = new SelectList(await cityName("زيارة"), "Id", "LocationName", tripBooking.CityId);
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            return View(tripBooking);
        }

        // GET: Sociologist/TripBookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var trip = await _context.TripBookings
                .Include(t=>t.SchedulingTripDetail.TripType)
                .Include(e=>e.SchedulingTripDetail.EducationalBody)
                .FirstOrDefaultAsync(i=>i.Id==id);
            if (trip == null)
            {
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }

            var apSetting = await _context.AppSettings.FirstOrDefaultAsync();
            int qtDays = 0;

            if (trip.SchedulingTripDetail.TripType.Name == "عمرة")
            {
                ViewData["CityId"] = new SelectList(await cityName("عمرة"), "Id", "LocationName",trip.CityId);
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.SchedulingTripDetail.TripType.Name == "داخلية")
            {
                ViewData["CityId"] = new SelectList(await cityName("داخلية"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyInternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.SchedulingTripDetail.TripType.Name == "خارجية")
            {
                ViewData["CityId"] = new SelectList(await cityName("خارجية"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyExternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.SchedulingTripDetail.TripType.Name == "زيارة")
            {
                ViewData["CityId"] = new SelectList(await cityName("زيارة"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }


            var tripView = new TripBookingCreateModelView()
            {
                EducationName = trip.SchedulingTripDetail.EducationalBody.Name,
                EducationCity = trip.SchedulingTripDetail.EducationalBody.City,
                QtyDaysVisitInternal = apSetting.QtyDaysVisitInternal,
                QtyDaysVisitEternal = apSetting.QtyDaysVisitEternal,
                SchedulingTripDetailId = trip.SchedulingTripDetailId,
                TripTime = trip.SchedulingTripDetail.TripDate,
                TripType = trip.SchedulingTripDetail.TripType.Name,
                QtyDays = qtDays,
                TripLocationName=trip.TripLocationName,
                CityId=trip.CityId,
                Id=trip.Id,
                TripQtyDays=trip.TripQtyDays,
                TripToDate=trip.TripToDate,
                TripTypeName=trip.TripTypeName
                
            };

            return View(tripView);

        }

        // POST: Sociologist/TripBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripBookingCreateModelView tripBooking)
        {
            var trip = await _context.TripBookings
            .Include(t => t.SchedulingTripDetail.TripType)
            .Include(e => e.SchedulingTripDetail.EducationalBody)
            .FirstOrDefaultAsync(i => i.Id == id);
            if (trip == null)
            {
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }
            if (ModelState.IsValid)
            {
                var tripbok = await _context.TripBookings.FindAsync(tripBooking.Id);
                tripbok.Id = tripBooking.Id;
                tripbok.SchedulingTripDetailId = tripBooking.SchedulingTripDetailId;
                tripbok.TripLocationName = tripBooking.TripLocationName;
                tripbok.CityId = tripBooking.CityId;
                tripbok.TripQtyDays = tripBooking.TripQtyDays;
                tripbok.TripToDate = tripBooking.TripTime.AddDays(tripBooking.TripQtyDays);
                tripbok.TripTypeName = tripBooking.TripTypeName;
                _context.Update(tripbok);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = trip.SchedulingTripDetailId });
            }
            else
            {

           
        

            var apSetting = await _context.AppSettings.FirstOrDefaultAsync();
            int qtDays = 0;

            if (trip.SchedulingTripDetail.TripType.Name == "عمرة")
            {
                ViewData["CityId"] = new SelectList(await cityName("عمرة"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.SchedulingTripDetail.TripType.Name == "داخلية")
            {
                ViewData["CityId"] = new SelectList(await cityName("داخلية"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyInternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.SchedulingTripDetail.TripType.Name == "خارجية")
            {
                ViewData["CityId"] = new SelectList(await cityName("خارجية"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyExternalDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }
            if (trip.SchedulingTripDetail.TripType.Name == "زيارة")
            {
                ViewData["CityId"] = new SelectList(await cityName("زيارة"), "Id", "LocationName", trip.CityId);
                qtDays = apSetting.QtyOmrahMedinaDaysTrip;
                ViewBag.qtDaysTripToMakkah = apSetting.QtyOmrahMakkahDaysTrip;
            }


            var tripView = new TripBookingCreateModelView()
            {
                EducationName = trip.SchedulingTripDetail.EducationalBody.Name,
                EducationCity = trip.SchedulingTripDetail.EducationalBody.City,
                QtyDaysVisitInternal = apSetting.QtyDaysVisitInternal,
                QtyDaysVisitEternal = apSetting.QtyDaysVisitEternal,
                SchedulingTripDetailId = trip.SchedulingTripDetailId,
                TripTime = trip.SchedulingTripDetail.TripDate,
                TripType = trip.SchedulingTripDetail.TripType.Name,
                QtyDays = qtDays,
                TripLocationName = trip.TripLocationName,
                CityId = trip.CityId,
                Id = trip.Id,
                TripQtyDays = trip.TripQtyDays,
                TripToDate = trip.TripToDate,
                TripTypeName = trip.TripTypeName

            };

            return View(tripView);
            }
        }

        // GET: Sociologist/TripBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }

            var tripBooking = await _context.TripBookings
                .Include(t => t.City)
                .Include(t => t.SchedulingTripDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripBooking == null)
            {
                Response.StatusCode = 404;
                return View("TripBookingsNotFound");
            }

            return View(tripBooking);
        }

        // POST: Sociologist/TripBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripBooking = await _context.TripBookings.FindAsync(id);
            _context.TripBookings.Remove(tripBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsMore", "SchedulingTripDetails", new {  id = tripBooking.SchedulingTripDetailId });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookingConfirmed(int id ,int TripStatus)
        {

            var tripBooking = await _context.TripBookings.FindAsync(id);
            tripBooking.TripStatus = TripStatus;
            _context.TripBookings.Update(tripBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsMore", "SchedulingTripDetails", new { id = tripBooking.SchedulingTripDetailId });

        }

        

        private bool TripBookingExists(int id)
        {
            return _context.TripBookings.Any(e => e.Id == id);
        }
    }
}
