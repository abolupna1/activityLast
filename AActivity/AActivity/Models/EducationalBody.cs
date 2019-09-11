using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class EducationalBody
    {
        public int Id { get; set; }

        [Display(Name = "الجهة"), StringLength(50,
            ErrorMessage = "{0} يجب ان يكون طول النص  اكبر من " +
            " {2}  أحرف  و اصغر من" +
            " {1} حرف",
            MinimumLength = 6), Required(ErrorMessage = "{0} مطلوب")]


        public string Name { get; set; }

        //[Display(Name = "المدينة "), Required(ErrorMessage = "{0} مطلوب")]
        //public string City { get; set; }

        //[Display(Name = "الختم"), Required(ErrorMessage = "{0} مطلوب")]

     //   public string SignaturePhoto { get; set; }

        [Display(Name = "نوع الجهة"), Required(ErrorMessage = "{0} مطلوب")]
        public string EntityType { get; set; }

        [Display(Name = "مشرف الجهة"), Required(ErrorMessage = "{0} مطلوب")]
        public int UserId { get; set; }

        [Display(Name = "مشرف الجهة"), ForeignKey("UserId")]
        public AppUser User { get; set; }

        [Display(Name = "ختم الجهة التعليمية"), Required(ErrorMessage = "{0} مطلوب")]

        public string Stamp { get; set; }


        public IEnumerable<SchedulingTripDetail> SchedulingTripDetails { get; set; }

    }
}
