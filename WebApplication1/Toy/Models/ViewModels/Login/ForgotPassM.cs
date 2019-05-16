using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toy.Models.ViewModels.Login
{
    public class ForgotPassM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }
    }
}
