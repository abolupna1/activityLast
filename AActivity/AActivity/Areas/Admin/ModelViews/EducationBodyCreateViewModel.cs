using AActivity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.ModelViews
{
    public class EducationBodyCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "الجهة"), StringLength(50,
            ErrorMessage = "{0} يجب ان يكون طول النص  اكبر من " +
            " {2}  أحرف  و اصغر من" +
            " {1} حرف",
            MinimumLength = 6), Required(ErrorMessage = "{0} مطلوب")]


        public string Name { get; set; }

        [Display(Name = "نوع الجهة"), Required(ErrorMessage = "{0} مطلوب")]
        public string EntityType { get; set; }

        [Display(Name = "مشرف الجهة"), Required(ErrorMessage = "{0} مطلوب")]
        public int UserId { get; set; }

   

   

        [Display(Name = "ختم الجهة التعليمية"), Required(ErrorMessage = "{0} مطلوب")]


        public IFormFile StampFile { get; set; }

        [Display(Name = "المكان"), Required(ErrorMessage = "{0} مطلوب")]

        public string City { get; set; }
    }
}
