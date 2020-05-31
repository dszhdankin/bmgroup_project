using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Version_1._0.Model
{
    public abstract class ModelItem
    {
        public abstract string getWay();
    }

    public class ModelGet<T> where T : ModelItem, new()
    {
        private static string Token = "";
        private static string url = "";

        public static void authenticate()
        {
            using (StreamReader sr = new StreamReader("config.txt"))
            {
                url = sr.ReadLine();
                string email = sr.ReadLine();
                string password = sr.ReadLine();
                string way = url + "Token";
                string str = "";
                using (WebClient web = new WebClient())
                {
                    web.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    str = web.UploadString(way, "POST", "userName=" + email + "&password=" + password + "&grant_type=password");
                    var joj = JObject.Parse(str);
                    Token = (string)joj["access_token"];
                }
            }
        }

        public ObservableCollection<T> get(string url1)
        {
            string way = "api/" + new T().getWay();
            string str = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    if (Token == "")
                        authenticate();
                    web.Headers[HttpRequestHeader.Authorization] = $"Bearer {Token}";
                    str = web.DownloadString(url + way);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return jsonEventParse(str);
        }

        public static T put(string url1, T data, int id)
        {
            string way = "api/" + new T().getWay();
            string str = "";
            using (WebClient web = new WebClient())
            {
                if (Token == "")
                    authenticate();
                web.Headers[HttpRequestHeader.Authorization] = $"Bearer {Token}";
                web.Encoding = System.Text.Encoding.UTF8;
                web.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = eventToJson(data);
                str = web.UploadString(url + way + id, "PUT", json);
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                return js.Deserialize<T>(str);
            }
        }

        public static void delete(string url1, int id)
        {
            string way = "api/" + new T().getWay();
            using (WebClient web = new WebClient())
            {
                if (Token == "")
                    authenticate();
                web.Headers[HttpRequestHeader.Authorization] = $"Bearer {Token}";
                web.UploadString(url + way + id, "DELETE", "");
            }
        }

        public static ObservableCollection<T> getByDateId(string url1, DateTime time, int id)
        {
            string way = "api/" + new T().getWay();
            string str = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    if (Token == "")
                        authenticate();
                    web.Headers[HttpRequestHeader.Authorization] = $"Bearer {Token}";
                    str = web.DownloadString(url + way + "?classId=" + id + "&date=" + time.ToUniversalTime().ToString("o"));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return jsonEventParse(str);
        }

        public static ObservableCollection<T> getByDate(string url1, DateTime time)
        {
            string way = "api/" + new T().getWay();
            string str = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    if (Token == "")
                        authenticate();
                    web.Headers[HttpRequestHeader.Authorization] = $"Bearer {Token}";
                    str = web.DownloadString(url + way + "?date=" + time.ToUniversalTime().ToString("o"));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return jsonEventParse(str);
        }

        public static T post(string url1, T data)
        {
            string way = "api/" + new T().getWay();
            string str = "";
            using (WebClient web = new WebClient())
            {
                if (Token == "")
                    authenticate();
                web.Headers[HttpRequestHeader.Authorization] = $"Bearer {Token}";
                web.Encoding = System.Text.Encoding.UTF8;
                web.Headers[HttpRequestHeader.ContentType] = "application/json";
                string json = eventToJson(data);
                str = web.UploadString(url + way, "POST", json);

                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                return js.Deserialize<T>(str);
            }
        }


        public static ObservableCollection<T> jsonEventParse(string str)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                T[] result = js.Deserialize<T[]>(str);

                var list = new ObservableCollection<T>();

                foreach (var item in result)
                {
                    object cur = item;
                    if (cur is Lesson)
                        (cur as Lesson).Time = (cur as Lesson).Time.ToLocalTime();
                    else if (cur is Elective)
                        (cur as Elective).Time = (cur as Elective).Time.ToLocalTime();
                    else if (cur is Event)
                        (cur as Event).StartTime = (cur as Event).StartTime.ToLocalTime();
                    list.Add(item);
                }
                    
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static string eventToJson(T data)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                //js.RegisterConverters(new[] { new DateTimeJavaScriptConverter() });
                return js.Serialize(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
