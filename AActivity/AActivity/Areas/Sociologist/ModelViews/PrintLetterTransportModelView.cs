using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class PrintLetterTransportModelView
    {
        public string TripType { get; set; }
        public string EducationBody { get; set; }
        public int QtyStudents { get; set; }
        public int QtyBuses { get; set; }
        
        public string TripSupervisor { get; set; }
        public string TripSupervisorMobile { get; set; }
        public string TripToCity { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TripDate { get; set; }
        public IEnumerable<LetterSignutre> WhoHasSignutre { get; set; }
        public IEnumerable<Signature> Signatures { get; set; }
        public CultureInfo cultureInfo { get; set; }

        

    }
}
