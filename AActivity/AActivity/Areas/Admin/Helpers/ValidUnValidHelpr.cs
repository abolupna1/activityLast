using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.Helpers
{
    public class ValidUnValidHelpr
    {
        public bool Value { get; set; }
        public string Name { get; set; }

        public static IList<ValidUnValidHelpr> ValidUnValidHelprList()
        {
            IList<ValidUnValidHelpr> validUnValidHelpr = new List<ValidUnValidHelpr>()
            {
                new ValidUnValidHelpr(){Name = "صالح" ,Value=true},
                new ValidUnValidHelpr(){Name = "غيرصالح" ,Value=false},



            };
            return validUnValidHelpr;
        }
    }
}
