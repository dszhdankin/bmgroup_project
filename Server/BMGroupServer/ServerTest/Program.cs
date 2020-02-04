using Server;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServerTest
{
    class Program
    {
        static void Main()
        {
            // TODO: refactor with attributes like [ApiEndpoint("/")]
            var urlMap = new Dictionary<Func<System.Net.HttpListenerContext, Task>, IEnumerable<string>>();
            urlMap.Add(HandlerPlaceholder, new string[] { "/" });
            ServerCLI cli = new ServerCLI(urlMap);

            Console.WriteLine("Available commands:\n\t" + string.Join("\n\t", ServerCLI.availableCommands));
            string command = Console.ReadLine();
            cli.Parse(command).Wait();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }

        // TODO: Handler - a decorator that will accept a {synchronous delegate returning JSON} and wrap it as Function<HttpListenerContext, Task>
        // Before the inner function call: 
        // * authentication?
        // * check URL (an HttpListener with the CLOSEST URL is chosen), abort if doesn't match
        // After the inner function call: 
        // * Catch exceptions, respond with error code
        // * Write the response to context.Response.OutputStream;
        static async Task HandlerPlaceholder(System.Net.HttpListenerContext context)
        {
            const string ResponseTemplate = "\"time\": \"{0}\"";
            // curly braces are added after template formatting (C# struggles with formatting otherwise)
            var response = "{" + string.Format(ResponseTemplate, DateTime.Now) + "}";
            using (var sw = new System.IO.StreamWriter(context.Response.OutputStream))
            {
                await sw.WriteAsync(response);
                await sw.FlushAsync();
            }
        }
    }
}
