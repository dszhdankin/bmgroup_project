using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0.Model
{
    public class Organization : ModelItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Event> Events { get; set; }

        public override string getWay()
        {
            return "Organizations/";
        }
    }
}
