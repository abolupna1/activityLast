using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class AppSetting
    {
        public int Id { get; set; }

        [Display(Name ="تحجز الرحلات قبل "),Required(ErrorMessage = "{0} مطلوب")]
        public int BookingTime { get; set; }

        [Display(Name = "عدد المنتدبين من العمادة"), Required(ErrorMessage = "{0} مطلوب")]
        public int QtyDeanshipDelegates { get; set; }

        [Display(Name = "عدد المنتدبين من المعاهد والدور "), Required(ErrorMessage = "{0} مطلوب")]
        public int QtyInstitutesDelegates { get; set; }

        [Display(Name = "عدد المنتدبين من الكليات"), Required(ErrorMessage = "{0} مطلوب")]
        public int QtyCollegesDelegates { get; set; }

        [Display(Name = "اقل عدد طلاب  في كل باص "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyPassengersInOneBus{ get; set; }


        [Display(Name = "عدد ايام رحلة العمرة "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyOmrahDaysTrip { get; set; }

        [Display(Name = "عدد ايام الرحلة الداخلية  "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyInternalDaysTrip { get; set; }

        [Display(Name = "عدد ايام الرحلة الخارجية  "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyExternalDaysTrip { get; set; }


        [Display(Name = "مبلغ سلفة  الرحلة الخارجية  "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountExternalCreditToTrip { get; set; }

        [Display(Name = "مبلغ سلفة  الرحلة الداخلية  "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountInternalCreditToTrip { get; set; }

        [Display(Name = "مبلغ سلفة  رحلة العمرة  "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountOmrahCreditToTrip { get; set; }


    }
}
