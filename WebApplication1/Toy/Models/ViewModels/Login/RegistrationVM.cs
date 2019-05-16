using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Models.ViewModels.Login
{
    public class RegistrationVM
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string SecondName { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required]
        [MinLength(3)]
        public string  City { get; set; }

        [Required]
        [MinLength(6)]
        public string Adress { get; set; }
    }
}
