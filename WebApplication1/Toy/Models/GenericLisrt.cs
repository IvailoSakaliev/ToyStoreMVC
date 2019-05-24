﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectToyStore.Data.Models;
using System.Collections.Generic;
using Toy.Filters;

namespace Toy.Models
{
    public class GenericList<Tentity, Tfilter> 
        : IGenericList<Tentity, Tfilter>
       where Tentity : BaseModel
       where Tfilter : GenericFiler<Tentity>, new()
    {
        public IList<Tentity> Items { get; set; }
        public IList<Tentity> AllItems { get; set; }
        public int Pages { get; set; }
        public string ControllerName { get; set; }
        public int StartItem { get; set; }
        public string ActionName { get; set; }
        public Tfilter Filter { get; set; }
        public List<int> QuantityList { get; set; }
        public IEnumerable<SelectListItem> Qua { get; set; }

        public GenericList()
        {
            this.Filter = new Tfilter();
            this.Items = new List<Tentity>();
            this.QuantityList = new List<int>();
        }


    }
}
