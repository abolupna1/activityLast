using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        [Display(Name = "السنة الدراسية"), Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }
    }
}
