using System;
using Server;


namespace BMGroupServer
{
    class Program
    {
        private static AsyncHttpServer server;

        static void Main()
        {
            server = new AsyncHttpServer(5, typeof(Views.PlaceholderView), typeof(Views.ClassView));
            ServerCLI cli = new ServerCLI(server);

            Console.WriteLine("Available commands:\n\t" + string.Join("\n\t", ServerCLI.availableCommands));
            string command = Console.ReadLine();
            cli.Parse(command).Wait();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
