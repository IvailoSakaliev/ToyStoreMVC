using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Login:BaseModel
    {
        public string  Email { get; set; }
        public string  Password { get; set; }
        public int UserID { get; set; }
    }
}
