using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TripReportImage
    {
        public int Id { get; set; }
        public int TripReportId { get; set; }
        [ ForeignKey("TripReportId")]
        public TripReport TripReport { get; set; }

        public string ImagePath { get; set; }
    }
}
