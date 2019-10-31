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

namespace AActivity.Areas.Sociologist.Controllers
{
    [Area("Sociologist")]
    public class FinishWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinishWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

   
        [Route("Sociologist/FinishWorks/IndexByBoking/{bokingId:int}")]
        public async Task<IActionResult> IndexByBoking(int bokingId)
        {
            var Emps = await _context.TripDelegates.Include(e=>e.FinishWorks)
                .Where(d => d.TripBookingId == bokingId && d.Confirmed == true)
                .ToListAsync();
            var deId = await _context.TripBookings.FirstOrDefaultAsync(e=>e.Id== bokingId);
            ViewBag.detailId = deId.SchedulingTripDetailId;
            return View(Emps);
        }
        // GET: Sociologist/FinishWorks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var finish = await _context.FinishWorks.FindAsync(id);
            var emp = await _context.TripDelegates
            .Include(t => t.TripBooking)
            .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
            .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
            .Include(t => t.TripBooking.SchedulingTripDetail)
            .FirstOrDefaultAsync(d => d.Id == finish.TripDelegateId);
            var finishWork = new FinishWorkCreateModelView()
            {
                Id = finish.Id,
                TripDelegateId = finish.TripDelegateId,
                TripBookingId = finish.TripBookingId,
                DelegationBudy = finish.DelegationBudy,
                CashAdvance = finish.CashAdvance,
                CashAdvanceAmont = finish.CashAdvanceAmont,
                DateDelgation = finish.DateDelgation,
                DelegationNumber = finish.DelegationNumber,
                EmployeeName = emp.EmployeeName,
                EmployeeNumber = emp.EmployeeNumber,
                EndWorkDuration = finish.EndWorkDuration,
                FoodsBuy = finish.FoodsBuy,
                FromDateDelgation = emp.TripBooking.SchedulingTripDetail.TripDate,
                JopDegree = finish.JopDegree,
                JopName = emp.JopName,
                LivingBuy = finish.LivingBuy,
                TransportBuy = finish.TransportBuy,
                TransportToToWorkBuy = finish.TransportToToWorkBuy
            };
            var sig = await _context.Signatures
               // .Where(e => e.SignatureRole == "عميد شؤون الطلاب" && e.Status==true)
                .Include(u=>u.User)
                .FirstOrDefaultAsync();
            ViewBag.amaidSignutre = sig != null ? sig.User.FullName : "";
            return View(finishWork);
        }

        // GET: Sociologist/FinishWorks/Create
        [Route("Sociologist/FinishWorks/Create/{tripDelegateId:int}")]
        public async Task<IActionResult> Create(int tripDelegateId)
        {
            var emp = await _context.TripDelegates
                .Include(t => t.TripBooking)
                .Include(t=>t.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(t=>t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t=>t.TripBooking.SchedulingTripDetail)
                .FirstOrDefaultAsync(d=>d.Id== tripDelegateId);
            string bodyName = emp.IsFromEducationBody == false ? "عمادة شؤون الطلاب" : emp.TripBooking.SchedulingTripDetail.EducationalBody.Name;
            int duration = emp.TripBooking.TripQtyDays;
            if (emp.TripBooking.SchedulingTripDetail.TripType.Name == "عمرة")
            {
                duration += 2;
            }
            var finishWork = new FinishWorkCreateModelView()
            {
                EmployeeName=emp.EmployeeName,
                EmployeeNumber=emp.EmployeeNumber,
                JopName=emp.JopName,
                DelegationBudy= bodyName,
                EndWorkDuration= duration,
                FromDateDelgation=emp.TripBooking.SchedulingTripDetail.TripDate,
                TripBookingId=emp.TripBookingId,
                TripDelegateId=emp.Id

            };

            return View(finishWork);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/FinishWorks/Create/{tripDelegateId:int}")]
        public async Task<IActionResult> Create(int tripDelegateId,FinishWorkCreateModelView finishWork)
        {
            if (ModelState.IsValid)
            {
                var finish = new FinishWork()
                {
                    CashAdvance=finishWork.CashAdvance,CashAdvanceAmont=finishWork.CashAdvanceAmont,
                    DateDelgation=finishWork.DateDelgation,DelegationBudy=finishWork.DelegationBudy,
                    DelegationNumber=finishWork.DelegationNumber,FoodsBuy=finishWork.FoodsBuy,
                    JopDegree= finishWork.JopDegree,LivingBuy= finishWork.LivingBuy,
                    TransportBuy= finishWork.TransportBuy,TransportToToWorkBuy=finishWork.TransportToToWorkBuy,
                    TripBookingId=finishWork.TripBookingId,TripDelegateId=finishWork.TripDelegateId,
                    EndWorkDuration = finishWork.EndWorkDuration
                };
                _context.Add(finish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexByBoking),new { bokingId = finish.TripBookingId});
            }

            return View(finishWork);
        }

        [Route("Sociologist/FinishWorks/Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
           
            var finish = await _context.FinishWorks.FindAsync(id);
            var emp = await _context.TripDelegates
            .Include(t => t.TripBooking)
            .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
            .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
            .Include(t => t.TripBooking.SchedulingTripDetail)
            .FirstOrDefaultAsync(d => d.Id == finish.TripDelegateId);
            var finishWork = new FinishWorkCreateModelView()
            {
                Id= finish.Id,TripDelegateId=finish.TripDelegateId,
                TripBookingId=finish.TripBookingId,DelegationBudy=finish.DelegationBudy,
                CashAdvance=finish.CashAdvance,CashAdvanceAmont=finish.CashAdvanceAmont,
                DateDelgation=finish.DateDelgation,DelegationNumber=finish.DelegationNumber,
                EmployeeName= emp.EmployeeName,EmployeeNumber=emp.EmployeeNumber,
                EndWorkDuration=finish.EndWorkDuration,FoodsBuy=finish.FoodsBuy,
                FromDateDelgation=emp.TripBooking.SchedulingTripDetail.TripDate,
                JopDegree=finish.JopDegree,JopName=emp.JopName,
                LivingBuy=finish.LivingBuy,TransportBuy=finish.TransportBuy,
                TransportToToWorkBuy=finish.TransportToToWorkBuy
            };
            return View(finishWork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/FinishWorks/Edit/{id:int}")]

        public async Task<IActionResult> Edit(int id, FinishWorkCreateModelView finishWork)
        {
            if (ModelState.IsValid)
            {
                var finish = new FinishWork()
                {
                    CashAdvance = finishWork.CashAdvance,
                    CashAdvanceAmont = finishWork.CashAdvanceAmont,
                    DateDelgation = finishWork.DateDelgation,
                    DelegationBudy = finishWork.DelegationBudy,
                    DelegationNumber = finishWork.DelegationNumber,
                    FoodsBuy = finishWork.FoodsBuy,
                    JopDegree = finishWork.JopDegree,
                    LivingBuy = finishWork.LivingBuy,
                    TransportBuy = finishWork.TransportBuy,
                    TransportToToWorkBuy = finishWork.TransportToToWorkBuy,
                    TripBookingId = finishWork.TripBookingId,
                    TripDelegateId = finishWork.TripDelegateId,
                    Id=finishWork.Id,EndWorkDuration=finishWork.EndWorkDuration
                    
                };
                _context.Update(finish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexByBoking), new { bokingId = finish.TripBookingId });

            }
            return View(finishWork);
        }

        // GET: Sociologist/FinishWorks/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var finish = await _context.FinishWorks.FindAsync(id);
            var emp = await _context.TripDelegates
            .Include(t => t.TripBooking)
            .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
            .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
            .Include(t => t.TripBooking.SchedulingTripDetail)
            .FirstOrDefaultAsync(d => d.Id == finish.TripDelegateId);
            var finishWork = new FinishWorkCreateModelView()
            {
                Id = finish.Id,
                TripDelegateId = finish.TripDelegateId,
                TripBookingId = finish.TripBookingId,
                DelegationBudy = finish.DelegationBudy,
                CashAdvance = finish.CashAdvance,
                CashAdvanceAmont = finish.CashAdvanceAmont,
                DateDelgation = finish.DateDelgation,
                DelegationNumber = finish.DelegationNumber,
                EmployeeName = emp.EmployeeName,
                EmployeeNumber = emp.EmployeeNumber,
                EndWorkDuration = finish.EndWorkDuration,
                FoodsBuy = finish.FoodsBuy,
                FromDateDelgation = emp.TripBooking.SchedulingTripDetail.TripDate,
                JopDegree = finish.JopDegree,
                JopName = emp.JopName,
                LivingBuy = finish.LivingBuy,
                TransportBuy = finish.TransportBuy,
                TransportToToWorkBuy = finish.TransportToToWorkBuy
            };

            return View(finishWork);
        }

        // POST: Sociologist/FinishWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finishWork = await _context.FinishWorks.FindAsync(id);
            _context.FinishWorks.Remove(finishWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexByBoking), new { bokingId = finishWork.TripBookingId });

        }

        private bool FinishWorkExists(int id)
        {
            return _context.FinishWorks.Any(e => e.Id == id);
        }
    }
}
