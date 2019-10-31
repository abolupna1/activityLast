using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class DelegateToSignutre
    {
        
        public int Id { get; set; }

    [Display(Name = " مالك التوقيع ")]
        public int WonerSignutreId { get; set; }
        [Display(Name = " مالك التوقيع "), ForeignKey("WonerSignutreId")]
        public Signature WonerSignutre { get; set; }

        [Display(Name = " المفوض بالتوقيع  ")]

        public int DelegatedToSignutreId { get; set; }
        [Display(Name = " المفوض بالتوقيع "), ForeignKey("DelegatedToSignutreId")]
        public Signature DelegatedToSignutre { get; set; }


        [Display(Name = " تاريخ التفويض "), Required(ErrorMessage = "{0} مطلوب"),
   DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime DateDelegate { get; set; }

        [Display(Name = "حالة التفويض")]

        public bool Status { get; set; }

    }
}
