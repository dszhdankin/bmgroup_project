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
        public EventInfo(string name, string description, DateTime date, byte[] photo, int id)
        {
            EventId = id;
            Photo = photo;
            Title = name;
            Description = description;
            StartTime = date.ToString(System.Globalization.CultureInfo.InstalledUICulture);
        }

        public int EventId { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public string StartTime { get; private set; }
        public byte[] Photo { get; private set; }
    }

    class ModelEvent
    {
        string way = "event/";

        public ObservableCollection<EventInfo> get(string url)
        {
            string str = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    str = web.DownloadString(url);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return jsonEventParse(str);
        }

        public ObservableCollection<EventInfo> jsonEventParse(string str)
        {
            JObject joj = JObject.Parse("{ \"arr\":" + str + "}");
            var list = new ObservableCollection<EventInfo>();

            foreach (var token in joj.First.First)
                list.Add(new EventInfo((string)token["Title"], (string)token["Description"],
                    (DateTime)token["StartTime"], (byte[])token["Photo"], (int)token["EventId"]));

            return list;
        }
    }
}
