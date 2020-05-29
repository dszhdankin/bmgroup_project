using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Windows;

namespace Version_1._0.Model
{
    public class Event : ModelItem
    {
        public int EventId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public string Photo { get; set; }

        public byte[] getBytePhoto()
        {
            if (Photo != null && Photo != "" && Photo != "QEA=")
                return Convert.FromBase64String(Photo);
            else
                return null;
        }

        public override string getWay()
        {
            return "Events/";
        }
    }

   
}
