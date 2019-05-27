using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Models.ViewModels.BaseTypes
{
    public class BaseTypeEditVm
    {
        public int ID { get; set; }

        [Required]
        public string  Name { get; set; }
    }
}
