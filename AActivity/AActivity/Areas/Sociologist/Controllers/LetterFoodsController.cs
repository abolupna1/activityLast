﻿using System;
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
    public class LetterFoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LetterFoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [Route("Sociologist/LetterFoods/Create/{bokingId:int}")]
        public async Task<IActionResult> Create(int bokingId)
        {
            var boking = await _context.TripBookings
                .Include(u => u.StudentsParticipatingInTrips)
                .Include(t => t.SchedulingTripDetail.TripType)
                .Include(u => u.SchedulingTripDetail.EducationalBody.User)
                .Include(e => e.SchedulingTripDetail.EducationalBody)
                .FirstOrDefaultAsync(b => b.Id == bokingId);
            if (boking==null)
            {
                Response.StatusCode = 404;
                return View("LetterFoodsNotFound");
            }
            var foods = new LetterFoodCreateViewModel()
            {
                EducationBody=boking.SchedulingTripDetail.EducationalBody.Name,
                EducationBodySupervisor= boking.SchedulingTripDetail.EducationalBody.User.FullName,
                EducationBodySupervisorMobile= boking.SchedulingTripDetail.EducationalBody.User.PhoneNumber,
                QtyStudents=boking.StudentsParticipatingInTrips.Count(),
                TripDate=boking.SchedulingTripDetail.TripDate,
                BokingId = boking.Id,
                UserId= boking.SchedulingTripDetail.EducationalBody.User.Id,
                TripBackDate=boking.TripToDate,
                TripType=boking.SchedulingTripDetail.TripType.Name
            };
            return View(foods);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Sociologist/LetterFoods/Create/{bokingId:int}")]
        public async Task<IActionResult> Create(int bokingId, LetterFoodCreateViewModel foods)
        {
            if (ModelState.IsValid)
            {
                var letter = new Letter() { TripBookingId = foods.BokingId, TypeLetter = 2 };
                _context.Letters.Add(letter);
                await _context.SaveChangesAsync();
                var leterFood = new LetterFood()
                {
                    UserId=foods.UserId,QtyStudents=foods.QtyStudents,
                    QtyMeals=foods.QtyMeals,LetterId= letter.Id,
                    FirstMealDate=foods.FirstMealDate,FirstMealTime=foods.FirstMealTime,
                    LastMealDate=foods.LastMealDate,LastMealTime=foods.LastMealTime
                };
                _context.LetterFoods.Add(leterFood);
                _context.AddRange(NotificationLetter(letter.Id).Result);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexByBoking", "Letters", new { bokingId = bokingId });
            }
            var boking = await _context.TripBookings
            .Include(u => u.StudentsParticipatingInTrips)
            .Include(t => t.SchedulingTripDetail.TripType)
            .Include(u => u.SchedulingTripDetail.EducationalBody.User)
            .Include(e => e.SchedulingTripDetail.EducationalBody)
            .FirstOrDefaultAsync(b => b.Id == bokingId);
            var food = new LetterFoodCreateViewModel()
            {
                EducationBody = boking.SchedulingTripDetail.EducationalBody.Name,
                EducationBodySupervisor = boking.SchedulingTripDetail.EducationalBody.User.FullName,
                EducationBodySupervisorMobile = boking.SchedulingTripDetail.EducationalBody.User.PhoneNumber,
                QtyStudents = boking.StudentsParticipatingInTrips.Count(),
                TripDate = boking.SchedulingTripDetail.TripDate,
                BokingId = boking.Id,
                UserId = boking.SchedulingTripDetail.EducationalBody.User.Id,
                TripBackDate = boking.TripToDate,
                TripType = boking.SchedulingTripDetail.TripType.Name

            };
            return View(food);
        }
        private async Task<IEnumerable<NotificationLetter>> NotificationLetter(int letterId)
        {
            var modle = new List<NotificationLetter>();
            var letter = await _context.Letters.FindAsync(letterId);
            var typeofletterSignutres = await _context.TypesOfLettersAndSignatures
                .Include(u => u.Signature).ThenInclude(u => u.User).Include(j => j.Signature.JobsSignatorie)
                .Where(s => s.TypesOfletterId == letter.TypeLetter).ToListAsync();
            foreach (var sig in typeofletterSignutres)
            {
                IhHaveDelegate(sig.Id, out bool wonerHaveChild);
                if (!wonerHaveChild)
                {
                    var notification = new NotificationLetter()
                    {
                        LetterId = letter.Id,
                        SignatureId = sig.SignatureId,
                        Status = false,
                        Time = DateTime.Now,
                        UserId = sig.Signature.UserId,
                        IsWonerSignutre = sig.IsSignatureOwner

                    };
                    if (sig.IsSignatureOwner)
                    {
                        notification.JobsSignatorieName = sig.Signature.JobsSignatorie.Name;
                    }
                    else
                    {
                        var b = await _context.TypesOfLettersAndSignatures
                            .Include(s => s.Signature.JobsSignatorie)
                            .FirstOrDefaultAsync(s => s.Id == sig.WonerSignatureId);
                        notification.JobsSignatorieName = b.Signature.JobsSignatorie.Name;

                    }
                    modle.Add(notification);
                }

            }
            return modle;
        }


        private void IhHaveDelegate(int typelettersignId, out bool wonerHaveChild)
        {
            var typelettersignbyId = _context.TypesOfLettersAndSignatures.Find(typelettersignId);
            var typelettersign = _context.TypesOfLettersAndSignatures.ToList();
            wonerHaveChild = typelettersign.Any(w => w.WonerSignatureId == typelettersignbyId.Id);


        }

        [Route("Sociologist/LetterFoods/Details/{letterId:int}")]
        public async Task<IActionResult> Details(int letterId)
        {

            if (!LetterExists(letterId))
            {
                Response.StatusCode = 404;
                return View("LetterFoodsNotFound");
            }
            var letter = await _context.Letters
                .Include(t => t.LetterFoods).ThenInclude(b => b.User)
                .Include(b => b.TripBooking)
                .Include(b => b.LetteSignutres)
                .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.StudentsParticipatingInTrips)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(d => d.NotificationLetters)

                .FirstOrDefaultAsync(l => l.Id == letterId);
            ViewBag.signutre = await _context.Signatures.Include(d => d.JobsSignatorie).ToListAsync();
            return View(letter);
        }

        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.Id == id);
        }
        [Route("Sociologist/LetterFoods/Print/{letterId:int}")]

        public async Task<IActionResult> Print(int letterId)
        {
            var letter = await _context.Letters.FindAsync(letterId);
            if (letter == null) { return NotFound(); }

            var foods = await _context.Letters
                .Include(f => f.LetterFoods).ThenInclude(u=>u.User)
                .Include(t => t.TripBooking.SchedulingTripDetail.TripType)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody)
                .Include(t => t.TripBooking.SchedulingTripDetail.EducationalBody.User)
                .Include(t => t.TripBooking.SchedulingTripDetail)
                .Include(t => t.TripBooking.SchedulingTripDetail.SchedulingTripHead)
                .Include(t => t.TripBooking.City)
                .Include(t => t.TripBooking.StudentsParticipatingInTrips)
                .Include(s => s.LetteSignutres).FirstOrDefaultAsync(i => i.Id == letterId);
            var signutres = await _context.Signatures.Include(s => s.JobsSignatorie).Include(s => s.User).ToListAsync();
            var appSettings = await _context.AppSettings.ToListAsync();
            PrintTripFoodView print = new PrintTripFoodView()
            {
                 EducationBody = foods.TripBooking.SchedulingTripDetail.EducationalBody.Name,
                 QtyStudents=foods.LetterFoods.FirstOrDefault().QtyStudents,
                 TripDate=foods.TripBooking.SchedulingTripDetail.TripDate,
                 TripType=foods.TripBooking.SchedulingTripDetail.TripType.Name,
                 QtyMeals=foods.LetterFoods.FirstOrDefault().QtyMeals,
                 FirstMealDate=foods.LetterFoods.FirstOrDefault().FirstMealDate,
                 FirstMealTime=foods.LetterFoods.FirstOrDefault().FirstMealTime,
                LastMealDate = foods.LetterFoods.FirstOrDefault().LastMealDate,
                LastMealTime = foods.LetterFoods.FirstOrDefault().LastMealTime,
                EducationBodySupervisor=foods.LetterFoods.FirstOrDefault().User.FullName,
                EducationBodySupervisorMobile=foods.LetterFoods.FirstOrDefault().User.PhoneNumber,
                Academicyear=foods.TripBooking.SchedulingTripDetail.SchedulingTripHead.AcademicYear,
                Stamp= appSettings.FirstOrDefault().Stamp,
                StampEducationBody=foods.TripBooking.SchedulingTripDetail.EducationalBody.Stamp,
               Signatures = signutres,
                Students=foods.TripBooking.StudentsParticipatingInTrips.ToList(),
                WhoHasSignutre=foods.LetteSignutres.ToList(),
                NoMersal=foods.NoMersal

            };
            return View(print);
        }

    }


}
