﻿using ProjectToyStore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.Models
{
    public class Login:BaseModel
    {
        public string  Email { get; set; }
        public string  Password { get; set; }
        public bool isRegisted { get; set; }
        public int Role { get; set; }
    }
}
