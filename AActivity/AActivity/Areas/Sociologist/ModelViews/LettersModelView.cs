using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class LettersModelView
    {

        [Display(Name = " رقم الخطاب   ")]
        public int LetterId { get; set; }

        [ Display(Name = " رقم الرحلة   ")]
        public int TripId { get; set; }

        [Display(Name = " الجهة")]
        public string EducationBody { get; set; }

        [Display(Name = " نوع الرحلة ")]
        public string TripType { get; set; }

        [Display(Name = " نوع الخطاب")]
        public string TypeLetter { get; set; }

        [Display(Name = " رقم صادر مرسال")]
        public string NoMersal { get; set; }

        [Display(Name = " تاريخ الرحلة ")]
        public DateTime TripDate { get; set; }

        public string ControlleName { get; set; } 
        public bool NotificationLettersStatus { get; set; }
        public int NotificationLettersUserId { get; set; }
    }
}
