using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class SignutreDelegateModelView
    {
        public int Id { get; set; }
        public string DelegatesName { get; set; }
        public string Jop { get; set; }
        public bool Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
