using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMGroupServer.Models
{
    public class School : Model
    {
        public int SchoolId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
