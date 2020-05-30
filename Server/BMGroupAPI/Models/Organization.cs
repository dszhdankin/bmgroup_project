using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Ellective> Ellectives { get; set; }
    }
}