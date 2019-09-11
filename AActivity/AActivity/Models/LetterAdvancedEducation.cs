using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class LetterAdvancedEducation
    {
        public int Id { get; set; }
        public int LetterId { get; set; }
        [ForeignKey("LetterId")]
        public Letter Letter { get; set; }

        [Display(Name = "مشرف الجهة"), Required(ErrorMessage = "{0} مطلوب")]
        public int UserId { get; set; }

        [Display(Name = "مشرف الجهة"), ForeignKey("UserId")]
        public AppUser User { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "مبلغ الرحلة الأساسي")]
        public float Amount { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "مبلغ الرحلة الأضافي")]
        public float AmountAdditional { get; set; }
    }
}
