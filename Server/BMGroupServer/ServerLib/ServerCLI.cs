using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    public class ServerCLI
    {
        private const uint MAX_LOAD = 5;
        private AsyncHttpServer server;
        public static string[] availableCommands = { "help", "start" };
        Dictionary<Func<HttpListenerContext, Task>, IEnumerable<string>> urlMap;

        // {handler: [IEnumerable of URLs relative to root]}
        public ServerCLI(Dictionary<Func<HttpListenerContext, Task>, IEnumerable<string>> handlerUrlsMap)
        {
            server = new AsyncHttpServer(MAX_LOAD);
            urlMap = handlerUrlsMap;
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

                        foreach (var handler in urlMap.Keys)
                        {
                            var urls = from url in urlMap[handler]
                                       select $"http://localhost:{port_number}" + url;
                            server.AddListener(urls, handler);
                        }

                        await server.Start();
                        exitEvent.WaitOne();        // wait for Ctrl + C
                        await server.Stop();
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
