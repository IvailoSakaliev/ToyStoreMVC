using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toy.Filters.EntityFilter;

namespace Toy.Models.ViewModels.BaseTypes
{
    public class BaseTypeList
        :GenericList<BaseType, BaseTypeFilter>
    {
        public BaseTypeList()
            :base()
        {

        }
    }
}
