using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;


namespace BMGroupServer
{
    class Program
    {
        private static AsyncHttpServer server;

        static void Main()
        {
            server = new AsyncHttpServer(5, typeof(Views.PlaceholderView));
            ServerCLI cli = new ServerCLI(server);

            Console.WriteLine("Available commands:\n\t" + string.Join("\n\t", ServerCLI.availableCommands));
            string command = Console.ReadLine();
            cli.Parse(command).Wait();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
