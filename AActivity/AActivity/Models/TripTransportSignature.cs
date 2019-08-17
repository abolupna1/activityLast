using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TripTransportSignature
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الخطاب ")]
        public int LetterId { get; set; }
        [Display(Name = " رقم الخطاب "), ForeignKey("LetterId")]
        public Letter Letter { get; set; }

        public int StatusSignature { get; set; } //      notificationStop = 0 - رئيس قسم النشاط =3 - مدير ادارة النشاط =2- وكيل العمادة=1

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "التوقيع  ")]
        public int SignatureId { get; set; }
        [Display(Name = "  التوقيع "), ForeignKey("SignatureId")]
        public Signature Signature { get; set; }


    }
}
