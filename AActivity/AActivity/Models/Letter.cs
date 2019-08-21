using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class Letter
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الحجز ")]
        public int TripBookingId { get; set; }
        [Display(Name = " الحجز "), ForeignKey("TripBookingId")]
        public TripBooking TripBooking { get; set; }

        [ Display(Name = "رقم مرسال ")]

        public string NoMersal { get; set; } = null;

        [ Display(Name = " حالة الخطاب ")]

        public bool Status { get; set; } = false;

        [Display(Name = " نوع الخطاب  ")]
        public int TypeLetter { get; set; } // النقل=1 - التغذية=2 -الانتداب =3 - السلفة = 4 - الى من يهمه الأمر=5 - وثسقة اداء مهمة =6  - 


        public IEnumerable<TripTransportSignature> TripTransportSignatures { get; set; }

    }
}
