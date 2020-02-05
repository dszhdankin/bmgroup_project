using Server;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServerTest
{
    class Program
    {
        private static AsyncHttpServer server;

        static void Main()
        {
            server = new AsyncHttpServer(5, typeof(Placeholder));
            ServerCLI cli = new ServerCLI(server);

            Console.WriteLine("Available commands:\n\t" + string.Join("\n\t", ServerCLI.availableCommands));
            string command = Console.ReadLine();
            cli.Parse(command).Wait();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }

    
    public class Placeholder
    { 
        [ApiEndpoint("/", "GET")]
        public static async Task<string> HandlerPlaceholder(System.Net.HttpListenerContext context)
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
