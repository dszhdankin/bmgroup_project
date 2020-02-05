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

        public async Task Handle(HttpListenerContext context, Dictionary<string, string[]> urlMethodMap)
        {
            // before
            // TODO: check URL & method

            string response = null;
            try
            {
                response = await innerFunction(context);
                // after
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync($"Callback error: {e.Message}");
                response = "Error";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                using (var sw = new System.IO.StreamWriter(context.Response.OutputStream))
                {
                    await sw.WriteAsync(response);
                    await sw.FlushAsync();
                }
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.ContentType = "application/json";
            }
        }
    }


}
