using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string Title { get; set; }
    }
}