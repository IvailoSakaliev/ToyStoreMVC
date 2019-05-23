using ProjectToyStore.Data.Models;
using System;
using System.Linq.Expressions;

namespace Toy.Filters
{
    public abstract class GenericFiler<Tentity>
        :  IGenericFiler<Tentity> where Tentity : BaseModel
    {
        public string Prefix { get; set; }
        public abstract Expression<Func<Tentity, bool>> BildFilter();
    }
}
