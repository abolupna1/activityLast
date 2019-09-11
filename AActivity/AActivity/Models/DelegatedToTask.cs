using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class DelegatedToTask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الموظف")]
        public int EmployeeNumber { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "اسم الموظف")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "المسمى الوظيفي ")]
        public string JopName { get; set; }


        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الخطاب ")]
        public int LetterId { get; set; }
        [Display(Name = " رقم الخطاب "), ForeignKey("LetterId")]
    //    public Letter Letter { get; set; }

        public string Ranked { get; set; } // المرتبة 
        public int MandateResolution { get; set; } // قرار الإنتداب

        
        public bool IsFromEducationBody { get; set; } // هل المنتدب من الجهة التعليمية = 1 من العمادة 0

    }
}
