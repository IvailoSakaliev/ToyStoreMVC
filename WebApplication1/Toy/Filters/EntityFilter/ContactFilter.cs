using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Toy.Atributes;

namespace Toy.Filters.EntityFilter
{
    public class ContactFilter
        : GenericFiler<Contact>
    {
        [FilterProperty(DisplayName ="Email")]
        public string Email { get; set; }

        [FilterProperty(DisplayName ="Subject")]
        public string  Name { get; set; }
        public override Expression<Func<Contact, bool>> BildFilter()
        {
            Expression<Func<Contact, bool>> filter =
                u => (string.IsNullOrEmpty(this.Email) || u.Name.Contains(this.Email.Trim()));
            return filter;
        }
    }
}
