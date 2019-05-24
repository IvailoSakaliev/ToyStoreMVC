using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Order
        :BaseModel
    {
        public string OrderNumber { get; set; }
        public int SubjectID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }
        public Status Status { get; set; }
    }
}
