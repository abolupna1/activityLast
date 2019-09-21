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
      
        [ Display(Name = "المبلغ المالي المخصص  للرحلة")]
        public float Amount { get; set; }

        [ Display(Name = "مبلغ الرحلة الأضافي")]
        public float AmountAdditional { get; set; }


        [ Display(Name = "عدد الطلاب")]
        public int QtyStudents { get; set; }

        [ Display(Name = " السلفة باسم")]
        public string CreditToEMployee { get; set; } //السلفة باسم الموظف

        [ Display(Name = "  رقم جوال الموظف")]
        public string EmployeeMobile { get; set; }

        [ Display(Name = "  نوع الرحلة  ")]
        public string TripType { get; set; }

        [ Display(Name = "  الجهة التعليمية   ")]
        public string EducationName { get; set; }

        [ Display(Name = "    الرحلة الى   ")]
        public string TripLocationName { get; set; }

        [ Display(Name = "     التاريخ   ")]
        public DateTime TripDate { get; set; }

        public int bokingId { get; set; }
        public string EmployeeNomber { get; set; }


    }
}
