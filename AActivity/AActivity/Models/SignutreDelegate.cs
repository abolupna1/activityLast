using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class SignutreDelegate
    {

        
        public int Id { get; set; }

        [Display(Name = " صاحب التوقيع "), Required(ErrorMessage = "{0} مطلوب")]
        public int SignatureId { get; set; }

        [Display(Name = " التوقيع "), ForeignKey("SignatureId")]
        public Signature Signature { get; set; }

        [Display(Name = " المفوض باالتوقيع "), Required(ErrorMessage = "{0} مطلوب")]
        public int UserId { get; set; }

        [Display(Name = " المفوض باالتوقيع "), ForeignKey("UserId")]
        public AppUser User { get; set; }

        [Display(Name = " يبدا في  تاريخ  "), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartAtDate { get; set; }

        [Display(Name = " ينتهي  في  تاريخ  "), Required(ErrorMessage = "{0} مطلوب"),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndAtDate { get; set; }

        [Display(Name = " حالة التوقيع "), Required(ErrorMessage = "{0} مطلوب")]
        public bool Status { get; set; }

        public DateTime TimeStamp { get; set; }


    }
}
