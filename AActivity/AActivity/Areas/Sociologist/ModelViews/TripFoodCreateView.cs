using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class TripFoodCreateView
    {
      
       
        public int TripBookingId { get; set; }

        public int TypeLetter { get; set; } = 2; //التغذية = 2


      
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "عدد الوجبات")]

        public int QtyMeals { get; set; }


        [Display(Name = " تاريخ أول وجبة "), Required(ErrorMessage = "{0} مطلوب"),
   DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:dd-MM}", ApplyFormatInEditMode = true)]
        public DateTime FirstMeal { get; set; }


        [Display(Name = " تاريخ آخر وجبة "), Required(ErrorMessage = "{0} مطلوب"),
   DataType(DataType.Date, ErrorMessage = "المدخل يجب ان يكون تاريخ"),
   DisplayFormat(DataFormatString = "{0:dd-MM}", ApplyFormatInEditMode = true)]

        public DateTime LastMeal { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "نوع اول وجبة ")]

        public string FirstMealText { get; set; }

        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "نوع آخر وجبة ")]

        public string LastMealText { get; set; }


    }
}
