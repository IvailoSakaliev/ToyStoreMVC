using Toy.Filters.EntityFilter;
using ProjectToyStore.Data.Models;

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
