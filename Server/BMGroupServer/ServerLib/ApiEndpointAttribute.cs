using System;

namespace Server
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ApiEndpointAttribute: Attribute
    {
        public ApiEndpointUrl URL;

        public ApiEndpointAttribute(string URL, params string[] httpMethods)
        {
            this.URL = new ApiEndpointUrl(URL, httpMethods);
        }
    }
}
