using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public byte[] Photo { get; set; }
    }
}