using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class PrintLetterDelegationsModelView
    {
        public IEnumerable<TripDelegate> Employees { get; set; }
        public IEnumerable<Signature> Signatures { get; set; }
        public IEnumerable<LetterSignutre> WhoHasSignutre { get; set; }

        public string Day { get; set; }

        [Display(Name = "من مدينة ")]
        public string FromCity { get; set; }

        [Display(Name = "الى مدينة ")]
        public string ToCityName { get; set; }

        [Display(Name = "المدة  ")]
        public int Duration { get; set; } // المدة

        [Display(Name = "التاريخ  "), DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TripDate { get; set; }

        [Display(Name = "الجهة التعليمية  ")]
        public string EducationBody { get; set; }

        [Display(Name = "نوع الرحلة  ")]
        public string TripType { get; set; }
    }
}
