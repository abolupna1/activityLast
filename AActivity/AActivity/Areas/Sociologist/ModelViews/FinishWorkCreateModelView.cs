using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class FinishWorkCreateModelView
    {
        public int Id { get; set; }

        public int TripBookingId { get; set; }
        public int TripDelegateId { get; set; }
        [ Display(Name = "رقم الموظف")]
        public int EmployeeNumber { get; set; }

        [ Display(Name = "اسم الموظف")]
        public string EmployeeName { get; set; }

        [ Display(Name = "المسمى الوظيفي ")]
        public string JopName { get; set; }

        [Display(Name = "المرتبة"), Required(ErrorMessage = "{0} مطلوب")]
        public string JopDegree { get; set; }

        [Display(Name = "رقم قرار الانتداب"), Required(ErrorMessage = "{0} مطلوب")]
        public int DelegationNumber { get; set; }

        [Display(Name = "مدة الانتداب")]
        public int EndWorkDuration { get; set; }

        [Display(Name = "تاريخ رقم الانتداب"),Required(ErrorMessage ="{0} مطلوب"), DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDelgation { get; set; }

        [Display(Name = "اعتبارا من تاريخ"),DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDateDelgation { get; set; }

        [Display(Name = "جهة الانتداب"),Required(ErrorMessage ="{0} مطلوب")]
        public string DelegationBudy { get; set; }//[جهة الانتداب

        [Display(Name = " وسيلة النقل على حساب    "),Required(ErrorMessage ="{0} مطلوب")]
        public bool TransportBuy { get; set; }

        [Display(Name = "  السكن على حساب"), Required(ErrorMessage = "{0} مطلوب")]
        public bool LivingBuy { get; set; }

        [Display(Name = "  الطعام على حساب"), Required(ErrorMessage = "{0} مطلوب")]
        public bool FoodsBuy { get; set; }

        [Display(Name = "المواصلات للعمل الرسمي على حساب "), Required(ErrorMessage = "{0} مطلوب")]
        public bool TransportToToWorkBuy { get; set; }

        [Display(Name = "هل سبق صرف سلفة نقدية على حساب مصاريف السفرية"), Required(ErrorMessage = "{0} مطلوب")]
        public bool CashAdvance { get; set; }

        [Display(Name = "مقدار السلفة "), Required(ErrorMessage = "{0} مطلوب")]
        public float CashAdvanceAmont { get; set; }
    }
}
