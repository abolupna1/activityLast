using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class PrintLetterDelegatesViews
    {
        public IEnumerable<TripDelegate> Employees { get; set; }
        public string TripToCity { get; set; }
        public int Duration { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime TripStartAt { get; set; }
        public string ToSupervise { get; set; } // للاشراف على
        public string TripType { get; set; }
        public string ActivityBossSignutre { get; set; }
        public string ActivityManagerSignutre { get; set; }
        public string ActivityAgentSignutre { get; set; }
        public string ActivityDeanSignutre { get; set; }
      //  public IEnumerable<TripDelegationSignutre> WhoHasSignutre { get; set; }
        public IEnumerable<Signature> Signatures { get; set; }

    }
}
