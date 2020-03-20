using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Version_1._0.Model
{
    public abstract class ModelItem
    {
        public abstract string getWay();
    }

    public class ModelGet<T> where T : ModelItem, new()
    {
        public ObservableCollection<T> get(string url)
        {
            string way = "api/" + new T().getWay();
            string str = "";
            using (WebClient web = new WebClient())
            {
                try
                {
                    str = web.DownloadString(url + way);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return jsonEventParse(str);
        }

        public ObservableCollection<T> jsonEventParse(string str)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                T[] result = js.Deserialize<T[]>(str);

                var list = new ObservableCollection<T>();

                foreach (var item in result)
                    list.Add(item);
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
