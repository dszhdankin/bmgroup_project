using System;
using Server;

namespace ServerTest
{
    class Program
    {
        static void Main()
        {
            ServerCLI cli = new ServerCLI();
            Console.WriteLine("Available commands:\n\t" + String.Join("\n\t", ServerCLI.availableCommands));
            string command = Console.ReadLine();
            cli.Parse(command).Wait();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
