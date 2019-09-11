using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class LetterTransport
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الخطاب ")]
        public int LetterId { get; set; }
        [Display(Name = " رقم الخطاب "), ForeignKey("LetterId")]
        public Letter Letter { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "  مشرف الجهة  ")]

        public int UserId { get; set; }
        [Display(Name = " مشرف الجهة  "), ForeignKey("UserId")]
        public AppUser User { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = " عدد الطلاب ")]
        public int QtyStudents { get; set; }
        public int QtyBuses { get; set; }
        


    }
}
