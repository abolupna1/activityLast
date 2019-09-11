using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class LetterFood
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الخطاب ")]
        public int LetterId { get; set; }
        [Display(Name = " رقم الخطاب "), ForeignKey("LetterId")]
        public Letter Letter { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = " عدد الطلاب ")]
        public int QtyStudents { get; set; }

        public int UserId { get; set; }
        [Display(Name = " مشرف الجهة  "), ForeignKey("UserId")]
        public AppUser User { get; set; }

        public int QtyMeals { get; set; }
        public DateTime FirstMealDate { get; set; }
        public DateTime LastMealDate { get; set; }
        public string FirstMealTime { get; set; }
        public string LastMealTime { get; set; }
    }
}
