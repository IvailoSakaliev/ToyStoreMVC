using ProjectToyStore.Data.Models;
using System;
using System.Linq.Expressions;
using Toy.Atributes;

namespace Toy.Filters.EntityFilter
{
    public class PruductFilter
        :GenericFiler<Product>
    {
        public PruductFilter()
        {

        }

        [FilterProperty(DisplayName ="Title")]
        public string Title { get; set; }

        public override Expression<Func<Product, bool>> BildFilter()
        {

            Expression<Func<Product, bool>> filter =
                u => (string.IsNullOrEmpty(this.Title) || u.Title.Contains(this.Title.Trim()));
            return filter;
        }
    }
}
