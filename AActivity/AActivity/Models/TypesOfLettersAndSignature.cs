using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TypesOfLettersAndSignature
    {
        public int Id { get; set; }


        [Display(Name = "  الموقع "), Required(ErrorMessage = "{0} مطلوب")]
        public int SignatureId { get; set; }

        [Display(Name = " الموقع "), ForeignKey("SignatureId")]
        public Signature Signature { get; set; }


        [Display(Name = "  نوع الخطاب "), Required(ErrorMessage = "{0} مطلوب")]
        public int TypesOfletterId { get; set; }

        [Display(Name = "  نوع الخطاب "), ForeignKey("TypesOfletterId")]
        public TypesOfletter TypesOfletter { get; set; }


        [Display(Name = "  مالك التوقيع ")]
        public int? WonerSignatureId { get; set; } = null;

        [Display(Name = " الموقع "), ForeignKey("WonerSignatureId")]
        public TypesOfLettersAndSignature WonerSignature { get; set; }





        [Display(Name = " حالة الموقع ")]
        public bool IsSignatureOwner { get; set; } // true to owner fals to Delgates



        [Display(Name = "   تاريخ  "),
DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartAtDate { get; set; }

        //        [Display(Name = " ينتهي  في  تاريخ  "), Required(ErrorMessage = "{0} مطلوب"),
        //DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
        //DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //        public DateTime EndAtDate { get; set; }

        //        [Display(Name = " حالة التوقيع   "), Required(ErrorMessage = "{0} مطلوب")]
        //        public bool Status { get; set; } // منتهي // ساري 


    }
}
