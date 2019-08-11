using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class SchedulingTripDetail
    {
        public int Id { get; set; }

        [Display(Name = "الجهة التعليمية"), Required(ErrorMessage = "{0} مطلوب")]
        public int EducationalBodyId { get; set; }
        [ForeignKey("EducationalBodyId"), Display(Name = "الجهة التعليمية")]
        public virtual EducationalBody EducationalBody { get; set; }

        [Display(Name = "رقم الجدول"), Required(ErrorMessage = "{0} مطلوب")]
        public int SchedulingTripHeadId { get; set; }
        [ForeignKey("SchedulingTripHeadId"), Display(Name = "رقم الجدول")]
        public virtual SchedulingTripHead SchedulingTripHead { get; set; }

        [Display(Name = "نوع الرحلة"), Required(ErrorMessage = "{0} مطلوب")]
        public int TripTypeId { get; set; }
        [ForeignKey("TripTypeId"), Display(Name = "نوع الرحلة")]
        public virtual TripType TripType { get; set; }

   
        [Display(Name = " التاريخ "), Required(ErrorMessage = "{0} مطلوب"),
   DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TripDate { get; set; }

   
        public string Notce { get; set; } = null;

        public int Status { get; set; } = 0;


        public IEnumerable<TripBooking> TripBookings { get; set; }


    }
}
