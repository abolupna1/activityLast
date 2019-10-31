using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class JobsSignatorie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Signature> Signatures { get; set; }
    }
}
