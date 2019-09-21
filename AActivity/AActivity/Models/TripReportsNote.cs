using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TripReportsNote
    {
        public int Id { get; set; }
        public int TripReportId { get; set; }
        [ForeignKey("TripReportId")]
        public TripReport TripReport { get; set; }
        public string Note { get; set; }
        public DateTime DateNotes { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public bool WhoIsWriteNote { get; set; } // true for الجهة التغليمية false for النشاط 
    }
}
