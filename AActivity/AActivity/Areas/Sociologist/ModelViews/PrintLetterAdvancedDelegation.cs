using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class PrintLetterAdvancedDelegation
    {
        public string TripType { get; set; }
        public string EduName { get; set; }
        public string TripTo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TripDate { get; set; }
        public int QtyStudent { get; set; }
        public float Amount { get; set; }
        public string AdvanceForEmp { get; set; }
        public string EmpMobile { get; set; }
        public IEnumerable<Signature> Signatures { get; set; }
        public IEnumerable<LetterSignutreForAdvance> WhoHasSignutre { get; set; }

    }
}
