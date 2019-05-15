using ProjectToyStore.Data.Models;
using System;
using System.Linq.Expressions;

namespace ToyStore.Filters
{
    public abstract class GenericFiler<Tentity>
        :  IGenericFiler1<Tentity> where Tentity : BaseModel
    {
        public string Prefix { get; set; }
        public abstract Expression<Func<Tentity, bool>> BildFilter();
    }
}
