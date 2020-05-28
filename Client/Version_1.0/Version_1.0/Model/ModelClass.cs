using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0.Model
{
    public class Class : ModelItem
    {
        public int ClassId { get; set; }
        public string Title { get; set; }

        public override string getWay()
        {
            return "Classes/";
        }
    }
}
