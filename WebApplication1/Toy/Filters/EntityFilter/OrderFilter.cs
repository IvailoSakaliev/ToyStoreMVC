using ProjectToyStore.Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Toy.Atributes;

namespace Toy.Filters.EntityFilter
{
    public class OrderFilter
        : GenericFiler<Order>
    {
        [FilterProperty(DisplayName ="Title")]
        public string Title { get; set; }


        public override Expression<Func<Order, bool>> BildFilter()
        {
            Expression<Func<Order, bool>> filter =
                u => (string.IsNullOrEmpty(this.Title) || u.Name.Contains(this.Title.Trim()));
            return filter;
        }
    }
}
