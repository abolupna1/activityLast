using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class LetterFoodCreateViewModel
    {
      
     
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = " عدد الطلاب ")]
        public int QtyStudents { get; set; }

        [Display(Name = " مشرف الجهة  "), Required(ErrorMessage = "{0} مطلوب")]

        public int UserId { get; set; }

        [Display(Name = " عدد الوجبات   "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyMeals { get; set; }
        [Display(Name = " موعد اول وجبة    "), Required(ErrorMessage = "{0} مطلوب"),
              DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        DisplayFormat(DataFormatString = "{0:dd-MM}", ApplyFormatInEditMode = true)]     
        public DateTime FirstMealDate { get; set; }

        [Display(Name = " موعد آخر وجبة     "), Required(ErrorMessage = "{0} مطلوب"),
              DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        DisplayFormat(DataFormatString = "{0:dd-MM}", ApplyFormatInEditMode = true)]
        public DateTime LastMealDate { get; set; }

        [Display(Name = " نوع أول وجبة     "), Required(ErrorMessage = "{0} مطلوب")]
        public string FirstMealTime { get; set; }

        [Display(Name = " نوع آخر وجبة     "), Required(ErrorMessage = "{0} مطلوب")]
        public string LastMealTime { get; set; }

        [Display(Name = "  الجهة      ")]
        public string EducationBody { get; set; }

        [Display(Name = "  مشرف الجهة       ")]
        public string EducationBodySupervisor { get; set; }
        [Display(Name = " جوال مشرف الجهة       ")]

        public string EducationBodySupervisorMobile { get; set; }
        public int BokingId { get; set; }

        [Display(Name = "نوع الرحلة")]

        public string TripType { get; set; }

        [Display(Name = "  تاريخ الرحلة      "), DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TripDate { get; set; }



        [Display(Name = "  تاريخ الرجوع      "), DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TripBackDate { get; set; }
    }
}
