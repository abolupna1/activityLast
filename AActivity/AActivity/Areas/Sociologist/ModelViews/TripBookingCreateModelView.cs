using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class TripBookingCreateModelView
    {
        public int Id { get; set; }

        [Display(Name = "رقم الرحلة المجدولة "), Required(ErrorMessage = "{0} مطلوب")]
        public int SchedulingTripDetailId { get; set; }
   

        [Display(Name = "المدينة "), Required(ErrorMessage = "{0} مطلوب")]
        public int CityId { get; set; }
        public int QtyDays { get; set; }

        public string EducationName { get; set; }
        public string EducationCity { get; set; }

        public int QtyDaysVisitInternal { get; set; }
        public int QtyDaysVisitEternal { get; set; }


        public string TripType { get; set; }
        public DateTime TripTime { get; set; }

        [Display(Name = " الى تاريخ   "), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ")]
        public DateTime TripToDate { get; set; }

        [Display(Name = "عدد الأيام ")]
        public int TripQtyDays { get; set; }

        [Display(Name = "المكان "), Required(ErrorMessage = "{0} مطلوب")]

        public string TripLocationName { get; set; }
        public string TripTypeName { get; set; } // (زيارة خارجية - زيارة داخلية - خارجية - داخلية - عمرة)

        public int TripStatus { get; set; } = 0;
    }
}
