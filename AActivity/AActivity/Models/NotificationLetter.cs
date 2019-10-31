using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class NotificationLetter
    {
        public int Id { get; set; }

        [Display(Name = "الموقع")]
        public int UserId { get; set; }
        [Display(Name = "الموقع"), ForeignKey("UserId")]
        public AppUser User { get; set; }

        [Display(Name = "التوقيع")]
        public int SignatureId { get; set; }
        [Display(Name = "التوقيع"), ForeignKey("SignatureId")]
        public Signature Signature { get; set; }

        [Display(Name = "الخطاب")]
        public int LetterId { get; set; }
        [Display(Name = "الخطاب"), ForeignKey("LetterId")]
        public Letter Letter { get; set; }

        [Display(Name = "الوقت"),
        DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Display(Name = "الحالة")]
        public bool Status { get; set; }

        [Display(Name = "مالك للتوقيع")]
        public bool IsWonerSignutre { get; set; }

        [Display(Name = "الوظيفة")]
        public string JobsSignatorieName { get; set; }
    }
}
