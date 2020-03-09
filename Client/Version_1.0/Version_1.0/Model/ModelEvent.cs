using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public EventInfo(JToken joj)
        {
            if (joj["Title"].ToObject(typeof(string)) != null)
                Title = (string)joj["Title"];

            if (joj["EventId"].ToObject(typeof(int)) != null)
                EventId = (int)joj["EventId"];

            if (joj["Description"].ToObject(typeof(string)) != null)
                Description = (string)joj["Description"];

            if (joj["StartTime"].ToObject(typeof(DateTime)) != null)
                StartTime = ((DateTime)joj["StartTime"]).ToString(System.Globalization.CultureInfo.InstalledUICulture);

            if (joj["Photo"].HasValues)
            {
                Photo = new byte[joj["Photo"].Count()];
                for (int i = 0; i < Photo.Length; i++)
                {
                    Photo[i] = (byte)joj["Photo"][i];
                }
            }
        }

        public int EventId { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public string StartTime { get; private set; }
        public byte[] Photo { get; private set; }
    }

    public class ModelEvent
    {
        string way = "event/";

        public ObservableCollection<EventInfo> get(string url)
        {
            string str = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    str = web.DownloadString(url + way);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return jsonEventParse(str);
        }

        public ObservableCollection<EventInfo> jsonEventParse(string str)
        {
            JObject joj = JObject.Parse("{ \"arr\":" + str + "}");
            var list = new ObservableCollection<EventInfo>();

            foreach (var token in joj.First.First)
                list.Add(new EventInfo(token));
            return list;
        }
    }
}
