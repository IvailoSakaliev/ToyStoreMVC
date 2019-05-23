using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toy.Filters.EntityFilter;

namespace Toy.Models.ViewModels.Order
{
    public class OrderList
        :GenericList<ProjectToyStore.Data.Models.Order,OrderFilter>
    {
        public OrderList()
            :base()
        {

        }
    }
}
