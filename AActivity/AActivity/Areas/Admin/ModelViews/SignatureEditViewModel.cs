using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.ModelViews
{
    public class SignatureEditViewModel 
    {
        public int Id { get; set; }
        [Display(Name = "صاحب التوقيع"), Required(ErrorMessage = "{0} مطلوب")]

        public int UserId { get; set; }


        [Display(Name = "التوقيع   ")]

        public IFormFile SignaturePhoto { get; set; }


        public string SignaturePhotoExists { get; set; }

        [Display(Name = "الدرجة العلمية")]
        public string Degree { get; set; }

        [Display(Name = "الوظيفة"), Required(ErrorMessage = "{0} مطلوب")]
        public int JobsSignatorieId { get; set; }

        [Display(Name = "حالة التوقيع"), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; }

    }
}
