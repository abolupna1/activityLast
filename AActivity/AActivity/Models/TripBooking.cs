using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
namespace AActivity.Models
{
    public class TripBooking
    {
      
        public int Id { get; set; }

        [Display(Name = "رقم الرحلة المجدولة "), Required(ErrorMessage = "{0} مطلوب")]
        public int SchedulingTripDetailId { get; set; }
        [ForeignKey("SchedulingTripDetailId")]
        public SchedulingTripDetail SchedulingTripDetail { get; set; }

        [Display(Name = "المدينة "), Required(ErrorMessage = "{0} مطلوب")]
        public int CityId { get; set; }
        [Display(Name = "المدينة ")]
        public City City { get; set; }

        [Display(Name = " الى تاريخ   "), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ")]
        public DateTime TripToDate { get; set; }

        [Display(Name = "عدد الأيام ")]
        public int TripQtyDays { get; set; }

        [Display(Name = "المكان "), Required(ErrorMessage = "{0} مطلوب")]

        public string TripLocationName { get; set; }

        public int TripStatus { get; set; } = 0;

        public IEnumerable<StudentsParticipatingInTrip> StudentsParticipatingInTrips { get; set; }
        public IEnumerable<TripDelegate> TripDelegates { get; set; }
        public IEnumerable<Letter> Letters { get; set; }
        public IEnumerable<TripReport> TripReports { get; set; }

        

    }
}
