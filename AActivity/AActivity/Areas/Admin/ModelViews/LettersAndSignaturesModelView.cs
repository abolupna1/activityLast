using AActivity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Areas.Admin.ModelViews
{
    public class LettersAndSignaturesModelView
    {
        public int TypeOfLetterAndSignutreId { get; set; }
        public int TypesOfletterId { get; set; }
        public string TypesOfletterName { get; set; }
        public bool IsSelected { get; set; }


    }
}
