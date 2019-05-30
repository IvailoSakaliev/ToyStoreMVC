using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Contact
        :BaseModel
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string  Date { get; set; }
    }
}
