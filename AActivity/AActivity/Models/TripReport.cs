using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TripReport
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الحجز ")]
        public int TripBookingId { get; set; }
        [Display(Name = " الحجز "), ForeignKey("TripBookingId")]
        public TripBooking TripBooking { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "التقرير  ")]
        public string TextBody { get; set; }

        [ Display(Name = "حالة التقرير  ")]

        public bool Status { get; set; } = false;

        public IEnumerable<TripReportImage> TripReportImages { get; set; }
        public IEnumerable<TripReportsNote> TripReportsNotes { get; set; }

    }
}
