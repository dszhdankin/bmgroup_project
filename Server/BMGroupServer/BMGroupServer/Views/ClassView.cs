using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ServerLib;

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
                catch (PageNotFoundException e)
                {
                    var classes = Services.ClassService.GetClasses(1);
                    return js.Serialize(classes);
                }
            }
        }

        [ApiEndpoint("/event/", "GET", "POST")]
        [ApiEndpoint("/event/<int>", "GET")]
        public static async Task<string> GetEvents(System.Net.HttpListenerContext context)
        {
            var js = new JavaScriptSerializer();
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                if (context.Request.HttpMethod == "POST")
                {
                    try
                    {
                        var evt = await Services.EventService.CreateFromJson(reader.ReadToEnd());
                        return evt.EventId.ToString();
                    } catch (Exception ex)
                    {
                        throw new PageNotFoundException("Wrong JSON object formst");
                    }
                }
                try
                {
                    int eventId = ApiEndpointUrl.GetIntArgument(context.Request.RawUrl);
                    var evt = Services.EventService.GetEvent(eventId);
                    return js.Serialize(evt);
                }
                catch (ArgumentException e)
                {
                    var events = Services.EventService.GetEvents(1);
                    return js.Serialize(events);
                }
            }
        }
    }
}
