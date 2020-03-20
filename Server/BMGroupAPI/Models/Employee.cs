﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string Info { get; set; }
    }
}