using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Atributes
{
    public class FilterProperty
        : Attribute
    {
        public string DisplayName { get; set; }

        public string DropDownTargetPropery { get; set; }
    }
}
