using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class City
    {
        public int Id { get; set; }
        [Display(Name = "المدينة"), Required(ErrorMessage = "{0} مطلوب")]

        public string LocationName { get; set; }


    }
}
