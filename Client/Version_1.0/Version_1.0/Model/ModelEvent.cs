using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Version_1._0.Model
{
    public class EventInfo
    {
        string name;
        string discription;
        DateTime date;

        public EventInfo(string nam, string dis, DateTime dat)
        {
            name = nam;
            discription = dis;
            date = dat;
        }
    }

    class ModelEvent
    {
        public EventInfo get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            StreamReader strm = new StreamReader(req.GetResponse().GetResponseStream());

            return jsoneParse(strm.ReadToEnd());
        }

        private EventInfo jsoneParse(string val)
        {
            JObject joj = JObject.Parse(val);
            return new EventInfo((string)joj["discription"], (string)joj["name"], (DateTime)joj["time"]);
        }
    }
}
