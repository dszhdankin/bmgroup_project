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
    public class Lesson
    {
        string name;
        DateTime date;
        string cabinetNumber;

        public Lesson(string nam, string cab, DateTime dat)
        {
            name = nam;
            cabinetNumber = cab;
            date = dat;
        }

        public string Name
        {
            get => name;
        }

        public string Cabinet
        {
            get => cabinetNumber;
        }

        public string Date
        {
            get => date.ToString(System.Globalization.CultureInfo.InstalledUICulture);
        }
    }

    class ModelLesson
    {
        public static ObservableCollection<Lesson> get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("StatusCode: " + response.StatusCode);
                Console.WriteLine(response.StatusDescription);
                return null;
            }
            StreamReader strm = new StreamReader(response.GetResponseStream());

            string str = strm.ReadToEnd();

            JObject joj = JObject.Parse("{ \"arr\":" + str + "}");

            ObservableCollection<Lesson> list = new ObservableCollection<Lesson>();

            foreach (var token in joj.First.First)
            {
                list.Add(new Lesson((string)token["name"], (string)token["cabinet"], (DateTime)token["time"]));
            }
            return list;
        }
    }
}