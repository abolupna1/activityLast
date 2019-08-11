using AActivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class SchedulingTripDetailsView
    {
        public IList<EducationalBody> EducationalBodies { get; set; }
        public IList<SchedulingTripDetail> SchedulingTripDetails { get; set; }
        public SchedulingTripHead SchedulingTripHead { get; set; }

    }
}
