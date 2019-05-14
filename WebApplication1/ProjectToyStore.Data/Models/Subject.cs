using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Subject
        :BaseModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public double Price { get; set; }
    }
}
