using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BMGroupServer.Views
{
    class ModelView<T> where T : Models.Model 
    {
        public static async Task<string> View(System.Net.HttpListenerContext context)
        {
            var js = new JavaScriptSerializer();
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                // create
                if (context.Request.HttpMethod == "POST")
                {
                    T t = await Services.Service<T>.CreateFromJson(reader.ReadToEnd(), 1);
                    return "";  // TODO: return id of t (or leave it this way)
                }

                int? id = ApiEndpointUrl.GetIntArgument(context.Request.RawUrl);
                // get one
                if (id.HasValue)
                {
                    T t = Services.Service<T>.Get(id.Value);
                    return js.Serialize(t);
                }
                // get list
                var ts = Services.Service<T>.GetList(1);
                return js.Serialize(ts);
            }
        }
    }
}
