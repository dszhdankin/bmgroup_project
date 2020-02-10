using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Handler
    {
        Func<HttpListenerContext, Task<string>> innerFunction;

        public Handler(Func<HttpListenerContext, Task<string>> handlerLogic)
        {
            innerFunction = handlerLogic;
        }

        // TODO: custom exceptions like UrlNotFoundException, MethodNotSupportedException, etc

        public async Task Handle(HttpListenerContext context, IEnumerable<ApiEndpointUrl> urls)
        {
            // before
            string response = null;
            DateTime start = DateTime.Now;
            try
            {
                var matchingApiUrl = urls.FirstOrDefault(apiUrl => apiUrl.ValidateUrl(context.Request.RawUrl));
                if (matchingApiUrl is null)
                    throw new ArgumentException("URL not found");

                if (!matchingApiUrl.supportedHttpMethods.Contains(context.Request.HttpMethod))
                    throw new ArgumentException("Method not supported");
                
                // handler logic call
                response = await innerFunction(context);
                // after
            }
            catch (ArgumentException e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                response = "Error";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Callback error: {e.Message}");
                response = "Error";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                context.Response.ContentEncoding    = Encoding.UTF8;
                context.Response.ContentType        = "application/json";
                using (var sw = new System.IO.StreamWriter(context.Response.OutputStream))
                {
                    await sw.WriteAsync(response);
                    await sw.FlushAsync();
                }
                var end = DateTime.Now;
                // TODO: implement a logging system worth using
                Console.Out.WriteLine($"{end} ({end - start}) {context.Response.StatusCode} {context.Request.HttpMethod:6} '{context.Request.Url}'");
            }
        }
    }


}
