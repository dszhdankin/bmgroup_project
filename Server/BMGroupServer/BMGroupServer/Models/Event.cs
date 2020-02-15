using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMGroupServer.Models
{
    public class Event : Model
    {
        public int EventId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public byte[] Photo { get; set; }
    }
}
