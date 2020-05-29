using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Ellective> Ellectives { get; set; }
    }
}