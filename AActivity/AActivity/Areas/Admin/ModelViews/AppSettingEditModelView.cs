using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.ModelViews
{
    public class AppSettingEditModelView
    {
        public int Id { get; set; }

        [Display(Name = "تحجز الرحلات قبل "), Required(ErrorMessage = "{0} مطلوب")]
        public int BookingTime { get; set; }

        [Display(Name = "عدد المنتدبين من العمادة"), Required(ErrorMessage = "{0} مطلوب")]
        public int QtyDeanshipDelegates { get; set; }

        [Display(Name = "عدد المنتدبين من المعاهد والدور "), Required(ErrorMessage = "{0} مطلوب")]
        public int QtyInstitutesDelegates { get; set; }

        [Display(Name = "عدد المنتدبين من الكليات"), Required(ErrorMessage = "{0} مطلوب")]
        public int QtyCollegesDelegates { get; set; }

        [Display(Name = "اقل عدد طلاب  في كل باص "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyPassengersInOneBus { get; set; }



        [Display(Name = "عدد ايام رحلة العمرة / مكة المكرمة "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyOmrahMakkahDaysTrip { get; set; }


        [Display(Name = "عدد ايام رحلة العمرة / المدينة المنورة "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyOmrahMedinaDaysTrip { get; set; }

        [Display(Name = "عدد ايام الرحلة الداخلية  "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyInternalDaysTrip { get; set; }

        [Display(Name = "عدد ايام الرحلة الخارجية  "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyExternalDaysTrip { get; set; }

        [Display(Name = "عدد ايام رحلة الزيارة الخارجية  "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyDaysVisitInternal { get; set; }

        [Display(Name = "عدد ايام رحلة الزيارة الداخلية  "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyDaysVisitEternal { get; set; }

        [Display(Name = "مبلغ سلفة  الرحلة الخارجية  "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountExternalCreditToTrip { get; set; }

        [Display(Name = "مبلغ سلفة  الرحلة الداخلية  "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountInternalCreditToTrip { get; set; }

        [Display(Name = "مبلغ سلفة  رحلة العمرة  "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountOmrahCreditToTrip { get; set; }

        [Display(Name = "مبلغ سلفة  رحلة زيارة   "), Required(ErrorMessage = "{0} مطلوب")]

        public float AmountVisitCreditToTrip { get; set; }


        public string Stamp { get; set; }
        [Display(Name = "ختم النشاط  ")]

        public IFormFile StampFile { get; set; }


        [Display(Name = "عدد الباصات لرحلة عمرة   "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyUmrahBuses { get; set; }

        [Display(Name = "عدد الباصات لرحلة الزيارة الداخلية   "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyVisitIntirnalBuses { get; set; }

        [Display(Name = "عدد الباصات لرحلة الزيارة الخارجية   "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyVisitExtirnalBuses { get; set; }

        [Display(Name = "عدد الباصات لرحلة الخارجية   "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyExtirnalBuses { get; set; }

        [Display(Name = "عدد الباصات لرحلة الداخلية   "), Required(ErrorMessage = "{0} مطلوب")]

        public int QtyIntirnalBuses { get; set; }


    }
}
