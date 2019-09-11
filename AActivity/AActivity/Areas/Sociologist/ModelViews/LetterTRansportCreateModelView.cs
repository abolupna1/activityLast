using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class LetterTRansportCreateModelView
    {
        [Required(ErrorMessage = "{0} مطلوب")]

        public int BokingId  { get; set; }
      
        public int UserId  { get; set; } // education superfisor

        [Display(Name = "المشرف على الرحلة ")]
        public string FullName { get; set; }
        [Display(Name = "  رقم جوال المشرف ")]

        public string Mobile { get; set; }
        [Display(Name = "  عدد الطلاب    ")]

        public int QtyStudents { get; set; }
        [Display(Name = "  نوع الرحلة    ")]

        public string TripType { get; set; }

        [Display(Name = "  الجهة      ")]

        public string EducationBody { get; set; }

        [Display(Name = "  عدد الباصات      ")]

        public int QtyBuses { get; set; }
        

    }
}
