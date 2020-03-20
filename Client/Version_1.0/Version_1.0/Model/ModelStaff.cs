using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0.Model
{
    public class Employee : ModelItem
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string Info { get; set; }

        public override string getWay()
        {
            return "Employees/";
        }
    }
}
