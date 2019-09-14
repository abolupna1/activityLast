using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class LetterDelegationCreateModelView
    {

        public int EmployeeNumber { get; set; }
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public string JopName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "تأكيد الإنتداب ")]

        public bool Confirmed { get; set; }

        public int BokingId { get; set; }

        [Display(Name = "من مدينة ")]
        public string FromCity { get; set; }

        [Display(Name = "الى مدينة ")]
        public string ToCityName { get; set; }

        [Display(Name = "المدة  ")]
        public int Duration { get; set; } // المدة

        [Display(Name = "التاريخ  "), DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TripDate { get; set; }

        [Display(Name = "الجهة التعليمية  ")]
        public string EducationBody { get; set; }

        [Display(Name = "نوع الرحلة  ")]
        public string TripType { get; set; }
    }
}
