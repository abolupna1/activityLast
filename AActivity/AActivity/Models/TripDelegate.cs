using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TripDelegate
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الموظف")]
        public int EmployeeNumber { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "اسم الموظف")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "المسمى الوظيفي ")]
        public string JopName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الحجز ")]
        public int TripBookingId { get; set; }
        [Display(Name = " الحجز "), ForeignKey("TripBookingId")]
        public TripBooking TripBooking { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = " منتدب من  ")]
        public bool IsFromEducationBody { get; set; } // هل المنتدب من الجهة التعليمية = 1 من العمادة 0

    }
}
