using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Models.ViewModels.User
{
    public class UserEditVm
    {

        public int ID { get; set; }
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string Telephone { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }

        [DataType(DataType.Password)]
        public string  Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public IList<ProjectToyStore.Data.Models.Order> Orders { get; set; }
    }
}
