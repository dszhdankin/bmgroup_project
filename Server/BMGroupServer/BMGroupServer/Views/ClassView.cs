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
    class ClassView
    {

        // TODO
        // Note: this is pure garbage, needs a lot of refactoring
        [ApiEndpoint("/class/", "GET", "POST")]
        [ApiEndpoint("/class/<int>", "GET")]
        public static async Task<string> GetClasses(System.Net.HttpListenerContext context)
        {
            var js = new JavaScriptSerializer();
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                if (context.Request.HttpMethod == "POST")
                {
                    var cls = await Services.ClassService.CreateFromJson(reader.ReadToEnd());
                    return cls.ClassId.ToString();
                }
                try
                {
                    int classId = ApiEndpointUrl.GetIntArgument(context.Request.RawUrl);
                    var cls = Services.ClassService.GetClass(classId);
                    return js.Serialize(cls);
                }
                catch (ArgumentException e)
                {
                    var classes = Services.ClassService.GetClasses(1);
                    return js.Serialize(classes);
                }
            }
        }
    }
}
