using ProjectToyStore.Data.Models;
using System.Collections.Generic;
using Toy.Filters.EntityFilter;

namespace Toy.Models.ViewModels.Type
{
    public class TypeList
        : GenericList<TypeSubject, TypeFilter>
    {
        public TypeList()
            :base()
        {
        }
    }
}
