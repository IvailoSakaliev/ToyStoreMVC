using System;
using System.Linq.Expressions;
using ProjectToyStore.Data.Models;

namespace ToyStore.Filters
{
    public interface IGenericFiler<Tentity> where Tentity : BaseModel, new()
    {
        string Prefix { get; set; }

        
    }
}