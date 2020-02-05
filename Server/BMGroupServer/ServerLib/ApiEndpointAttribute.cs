using System;
// using Server;

namespace Server
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ApiEndpointAttribute: Attribute
    {
        // public AsyncHttpServer server;
        public string URL;
        public string[] httpMethods;

        public ApiEndpointAttribute(string URL, params string[] httpMethods)
        {
            // this.server = server;
            this.URL = URL;
            this.httpMethods = httpMethods;
        }
    }
}
