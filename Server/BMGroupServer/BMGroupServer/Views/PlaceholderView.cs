using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMGroupServer.Views
{
    class PlaceholderView
    {
        [ApiEndpoint("/", "GET")]
        public static async Task<string> HandlerPlaceholder1(System.Net.HttpListenerContext context)
        {
            var response = $"{{\"time\": \"{DateTime.Now}\"}}";
            return response;
        }

        [ApiEndpoint("/AAA/", "GET", "POST")]
        [ApiEndpoint("/BBB/", "GET")]
        public static async Task<string> HandlerPlaceholder2(System.Net.HttpListenerContext context)
        {
            var response = "AAAAA";
            return response;
        }
    }
}
