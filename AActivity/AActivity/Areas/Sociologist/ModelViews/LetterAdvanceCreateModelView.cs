using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class LetterAdvanceCreateModelView
    {
        public int Id { get; set; }
        public int LetterId { get; set; }
      
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "المبلغ المالي المخصص  للرحلة")]
        public float Amount { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "مبلغ الرحلة الأضافي")]
        public float AmountAdditional { get; set; }


        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "عدد الطلاب")]
        public int QtyStudents { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = " السلفة باسم")]

        public string CreditToEMployee { get; set; } //السلفة باسم الموظف
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "  رقم جوال الموظف")]

        public string EmployeeMobile { get; set; }
      
    }
}
