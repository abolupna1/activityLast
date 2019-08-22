using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TripFood
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب"), Display(Name = "رقم الخطاب ")]
        public int LetterId { get; set; }
        [Display(Name = " رقم الخطاب "), ForeignKey("LetterId")]
        public Letter Letter { get; set; }


        public int QtyMeals { get; set; }
        public DateTime FirstMeal { get; set; }
        public DateTime LastMeal { get; set; }
        public string FirstMealText { get; set; }
        public string LastMealText { get; set; }
    }
}
