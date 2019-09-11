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

        [ Display(Name = "رقم صادر مرسال ")]
        public int NoMersal { get; set; }

        [Display(Name = "نوع الخطاب")]

        public int TypeLetter { get; set; }//[transport, foods, delegations, advanced]
        public IEnumerable<LetterTransport> LetterTransports { get; set; }
        public IEnumerable<LetterSignutre> LetteSignutres { get; set; }
        
        public IEnumerable<LetterFood> LetterFoods { get; set; }
        public IEnumerable<LetterAdvancedDelegation> LetterAdvancedDelegations { get; set; }
        public IEnumerable<LetterAdvancedEducation> LetterAdvancedEducations { get; set; }

        

    }
}
