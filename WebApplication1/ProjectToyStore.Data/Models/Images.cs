using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Images
        :BaseModel
    {
        public string  Path { get; set; }
        public int Subject_id { get; set; }
    }
}
