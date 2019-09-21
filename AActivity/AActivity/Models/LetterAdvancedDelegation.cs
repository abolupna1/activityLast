using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class LetterAdvancedDelegation
    {

        public int Id { get; set; }
        public int LetterId { get; set; }
        [ ForeignKey("LetterId")]
        public Letter Letter { get; set; }


        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "مبلغ الرحلة الأساسي")]
        public float Amount { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "مبلغ الرحلة الأضافي")]
        public float AmountAdditional { get; set; }

        public int QtyStudents { get; set; }
        public string CreditToEMployee { get; set; } //السلفة باسم الموظف
        public string EmployeeMobile { get; set; } 
        public string EmployeeNomber { get; set; }
        public string Statatus { get; set; } = null;
        public string Notes { get; set; } = null;



        public IEnumerable<LetterSignutreForAdvance> LetterSignutreForAdvances { get; set; }




    }
}
