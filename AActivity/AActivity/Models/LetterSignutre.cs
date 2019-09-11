using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class LetterSignutre
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الخطاب ")]
        public int LetterId { get; set; }
        [Display(Name = " رقم الخطاب "), ForeignKey("LetterId")]
        public Letter Letter { get; set; }

        public int WhoHasSignutre { get; set; } //      - رئيس قسم النشاط =1 - مدير ادارة النشاط =2- وكيل العمادة=3 - عميد شؤون الطلاب=4 - المشرف على غدارة المدينة الجامعية=5

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "التوقيع  ")]
        public int SignatureId { get; set; }
        [Display(Name = "  التوقيع "), ForeignKey("SignatureId")]
        public Signature Signature { get; set; }

        public bool IsHeOwnerOfSignature { get; set; }
    }
}
