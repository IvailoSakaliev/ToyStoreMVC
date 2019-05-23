using System;
using System.Linq.Expressions;
using ProjectToyStore.Data.Models;

namespace Toy.Filters
{
    public interface IGenericFiler<Tentity> where Tentity : BaseModel
    {
        string Prefix { get; set; }

        Expression<Func<Tentity, bool>> BildFilter();
    }
}