using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectToyStore.Data.Models;

namespace Toy.Models.ViewModels.Product
{
    public class ProductVM
    {

        public ProductVM()
        {
            Image = new List<string>();
            ImageS = new List<Images>();
        }


        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public IEnumerable<SelectListItem> Type { get; set; }

        [Required]
        public IEnumerable<SelectListItem> BaseType { get; set; }
        // next line of code is for edit pages

        public string FrontImage { get; set; }

        public string TypeString { get; set; }

        [Required]
        public string DateOfEdit { get; set; }

        public List<Images> ImageS { get; set; }

        [Display(Name = "Add new image")]
        public List<string> Image { get; set; }

    }
}
