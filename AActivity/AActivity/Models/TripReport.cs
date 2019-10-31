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

        public int Status { get; set; }  // 0 / 1 / 2
        //0 انتظار
        //1 مقبول /
        //يجب التعديل 

        public IEnumerable<TripReportImage> TripReportImages { get; set; }

    }
}
