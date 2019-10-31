using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AActivity.Models
{
    public class TypesOfletter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<TypesOfLettersAndSignature> TypesOfLettersAndSignatures { get; set; }
    }
}
