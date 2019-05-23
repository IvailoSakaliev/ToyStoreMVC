using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Models.ViewModels.Type
{
    public class TypeVM
    {
        [Required]
        public string Type { get; set; }
    }
}
