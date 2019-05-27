using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Models.ViewModels.Type
{
    public class TypeVM
    {
        public int ID { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public IEnumerable<SelectListItem> BaseModel { get; set; }

        public int selectedItem { get; set; }
    }
}
