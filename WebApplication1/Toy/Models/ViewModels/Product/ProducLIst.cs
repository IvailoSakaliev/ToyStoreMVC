using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toy.Filters.EntityFilter;
using ToyStore.Models;

namespace Toy.Models.ViewModels.Product
{
    public class ProducLIst
        :GenericList<ProjectToyStore.Data.Models.Product, PruductFilter>
    {
        public ProducLIst()
            :base()
        {

        }
    }
}
