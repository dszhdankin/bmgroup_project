using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0.Model
{
    public class Elective : ModelItem
    {
        public int EllectiveId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }

        public override string getWay()
        {
            return "Ellectives/";
        }
    }
}
