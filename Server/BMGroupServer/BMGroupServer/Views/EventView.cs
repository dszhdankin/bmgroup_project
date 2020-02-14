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
    class EventView
    {
        [ApiEndpoint("/event/", "GET", "POST")]
        [ApiEndpoint("/event/<int>", "GET")]
        public static async Task<string> GetEvents(System.Net.HttpListenerContext context)
        {
            var js = new JavaScriptSerializer();
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                if (context.Request.HttpMethod == "POST")
                {
                    var evt = await Services.EventService.CreateFromJson(reader.ReadToEnd());
                    return evt.EventId.ToString();
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
