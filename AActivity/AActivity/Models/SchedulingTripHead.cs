using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class SchedulingTripHead
    {
      
        public int Id { get; set; }

        [Display(Name = "السنة الدراسية"), Required(ErrorMessage = "{0} مطلوب")]
        public string AcademicYear { get; set; }

        [Display(Name = "الفصل الدراسي"), Required(ErrorMessage = "{0} مطلوب")]
        public string Semester { get; set; }

        [Display(Name = "من تاريخ"), Required(ErrorMessage = "{0} مطلوب"),
           DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
           DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Display(Name = "الى تاريخ"), Required(ErrorMessage = "{0} مطلوب"),
           DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
           DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Display(Name = "منشئ الجدول "), Required(ErrorMessage = "{0} مطلوب")]
        public int UserId { get; set; }

        [Display(Name = " منشئ الجدول "), ForeignKey("UserId")]
        public AppUser User { get; set; }
        [Display(Name = "الحالة ")]
        public bool Status { get; set; } = false;

        public IEnumerable<SchedulingTripDetail> SchedulingTripDetails { get; set; }

    }


}
