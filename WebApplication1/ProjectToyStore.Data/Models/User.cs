using ProjectToyStore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class User
        :BaseModel
    {
        public string SecondName { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string  Telephone { get; set; }
        public int LoginID { get; set; }
    }
}
