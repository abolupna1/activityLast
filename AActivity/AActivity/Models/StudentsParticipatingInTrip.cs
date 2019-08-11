using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class StudentsParticipatingInTrip
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الطالب")]
        [Compare("StudentNumber",ErrorMessage ="مكرر")]

        public int StudentNumber { get; set; }

        [Required(ErrorMessage ="{0} مطلوب"),Display(Name ="اسم الطالب")]
        public string StudentName  { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الحجز ")]
        public int TripBookingId { get; set; }
       [ Display(Name = " الحجز "), ForeignKey("TripBookingId")]
        public TripBooking TripBooking { get; set; }

    }
}
