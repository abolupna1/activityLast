using AActivity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class TripReportEditModelView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} مطلوب")]
        public int TripBookingId { get; set; }
        public int DitailId { get; set; }


        [ Display(Name = "التقرير  "), Required(ErrorMessage = "{0} مطلوب")]
        public string TextBody { get; set; }

        [ Display(Name = "صور الرحلة  ")]    
        public List<IFormFile> Photos { get; set; }
        public List<TripReportImage> Images { get; set; }
    }
}
