﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Toy.Models.ViewModels.Product
{
    public class ProductVM
    {
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
        public List<string> Image { get; set; }


        public ProductVM()
        {
            Image = new List<string>();
        }
    }
}
