using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class Signature
    {
        public int Id { get; set; }
        [Display(Name = "صاحب التوقيع"), Required(ErrorMessage = "{0} مطلوب")]

        public int UserId { get; set; }
        [ForeignKey("UserId"),Display(Name = "صاحب التوقيع   ")]
        public AppUser User { get; set; }

        [Display(Name = "التوقيع   "), Required(ErrorMessage = "{0} مطلوب")]

        public string SignaturePhoto { get; set; }

        [Display(Name = "الدرجة العلمية")]
        public string Degree { get; set; }

        

   [Display(Name = "الوظيفة"), Required(ErrorMessage = "{0} مطلوب")]
        public string SignatureRole { get; set; }

        [Display(Name = "حالة التوقيع"), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; }

        public IEnumerable<TripTransportSignature> TripTransportSignatures { get; set; }
        public IEnumerable<SignutreDelegate> SignutreDelegates { get; set; }


    }
}
