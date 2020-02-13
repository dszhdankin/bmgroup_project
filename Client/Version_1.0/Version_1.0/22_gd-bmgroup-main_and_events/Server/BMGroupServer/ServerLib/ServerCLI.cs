using System;
using System.Threading.Tasks;

namespace Server
{
    public class ServerCLI
    {
        private AsyncHttpServer server;
        public static string[] availableCommands = { "help", "start" };

        public async Task Parse(string command)
        {
            string[] words = command.Split();

            switch (words[0])
            {
                case "help":
                    Console.WriteLine("help - show this message\nstart port_number - start the server listening at port_number");
                    break;

                case "start":
                    if (words.Length > 1 && uint.TryParse(words[1], out uint port_number))
                    {
                        server = new AsyncHttpServer(port_number);
                        await Console.Out.WriteLineAsync($"Starting a server at port {port_number}");
                        await server.Start();
                    }
                    else
                        Console.WriteLine("Incorrect command pattern");
                    break;

                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }

    }
}
