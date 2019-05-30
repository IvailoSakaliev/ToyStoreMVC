using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toy.Filters.EntityFilter;

namespace Toy.Models.ViewModels.Contacts
{
    public class ContactLIst
        :GenericList<Contact, ContactFilter>
    {
        public ContactLIst()
            :base()
        {

        }
    }
}
