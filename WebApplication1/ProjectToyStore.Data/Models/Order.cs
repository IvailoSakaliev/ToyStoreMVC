using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Order
        :BaseModel
    {
        public int SubjectID { get; set; }
        public int UserID { get; set; }
        public Status Status { get; set; }
    }
}
