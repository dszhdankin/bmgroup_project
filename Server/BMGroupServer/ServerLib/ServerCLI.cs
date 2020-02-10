using System;
using System.Threading.Tasks;

namespace Server
{
    public class ServerCLI
    {
        private const uint MAX_LOAD = 5;
        private AsyncHttpServer _server;
        public static string[] availableCommands = { "help", "start" };
        
        public ServerCLI(AsyncHttpServer server)
        {
            _server = server;
        }

        public async Task Parse(string command)
        {
            string[] words = command.Split();

            switch (words[0])
            {
                case "help":
                    Console.WriteLine("help - show this message\nstart port_number - start the server listening at port_number");
                    break;

                case "start":
                    var exitEvent = new System.Threading.ManualResetEvent(false);

                    Console.CancelKeyPress += (sender, eventArgs) => {      // https://stackoverflow.com/a/13899429/10679134
                        eventArgs.Cancel = true;
                        exitEvent.Set();
                    };

                    if (words.Length > 1 && uint.TryParse(words[1], out uint port_number))
                    {
                        await Console.Out.WriteLineAsync($"Starting a server at port {port_number}");
                        _server.Port = port_number;
                        await _server.Start();
                        exitEvent.WaitOne();        // wait for Ctrl + C
                        await _server.Stop();
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
