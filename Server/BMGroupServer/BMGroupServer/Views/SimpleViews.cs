using Server;
using System.Threading.Tasks;

namespace BMGroupServer.Views
{
    public class SimpleViews
    {
        [ApiEndpoint("/class/", "GET", "POST")]
        [ApiEndpoint("/class/<int>", "GET")]
        public static Task<string> ClassView(System.Net.HttpListenerContext context) 
            => ModelView<Models.Class>.View(context);


        [ApiEndpoint("/event/", "GET", "POST")]
        [ApiEndpoint("/event/<int>", "GET")]
        public static Task<string> EventView(System.Net.HttpListenerContext context) 
            => ModelView<Models.Event>.View(context);


        [ApiEndpoint("/staff/", "GET", "POST")]
        [ApiEndpoint("/staff/<int>", "GET")]
        public static Task<string> StaffView(System.Net.HttpListenerContext context)
            => ModelView<Models.Teacher>.View(context);
    }
}
