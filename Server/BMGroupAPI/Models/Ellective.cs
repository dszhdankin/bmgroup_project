using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Ellective
    {
        public int EllectiveId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }
    }
}