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
    public class Teacher
    {
        public Teacher(int id, string name, string surname, string patronymic, byte[] photo, string subjects)
        {
            TeacherId = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Photo = photo;
            Subjects = subjects;
        }

        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
        public string Subjects { get; set; }
    }

    public class ModelStaff
    {
        public ObservableCollection<Teacher> get(string url)
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

            var list = jsonParse(joj);

            Console.WriteLine("Got events");

            return list;
        }

        public ObservableCollection<Teacher> jsonParse(JObject joj)
        {
            var list = new ObservableCollection<Teacher>();

            foreach (var token in joj.First.First)
            {
                list.Add(new Teacher((int)token["TeacherId"], (string)token["Name"], (string)token["Surname"],
                    (string)token["Patronymic"], (byte[])token["Photo"], (string)token["Subjects"]));
            }
            return list;
        }
    }
}
