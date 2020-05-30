using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Ellective
    {
        [Key]
        public int EllectiveId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }
    }
}