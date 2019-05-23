using ProjectToyStore.Data.Models;
using System;
using System.Linq.Expressions;
using Toy.Atributes;

namespace Toy.Filters.EntityFilter
{
    public class TypeFilter
        :GenericFiler<TypeSubject>
    {

        [FilterProperty(DisplayName = "Type")]
        public string Type { get; set; }

        public override Expression<Func<TypeSubject, bool>> BildFilter()
        {

            Expression<Func<TypeSubject, bool>> filter =
                u => (string.IsNullOrEmpty(this.Type) || u.Name.Contains(this.Type.Trim()));
            return filter;
        }
    }
}
