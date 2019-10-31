using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AActivity.Models;

namespace AActivity.Areas.Sociologist.ModelViews
{
    public class SignaturesModelView
    {
        public string SignPhoto { get; set; }
        public string OwnerSign { get; set; }
        public string Jop { get; set; }
        public bool Status { get; set; }
        public List<SignutreDelegateModelView> DelegatesToMySighn { get; set; } // المفوضين بتوقيعي
        public List<SignutreDelegateModelView> DelegatedByAnotherSighns { get; set; } // مفوض بتواقيع اخرين


    }
}
