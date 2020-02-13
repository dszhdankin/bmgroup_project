using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    internal class AsyncHttpServer
    {
        private const string ResponseTemplate = "\"time\": \"{0}\"";

        private readonly HttpListener _listener;

        public AsyncHttpServer(uint portNumber)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(string.Format("http://localhost:{0}/", portNumber));
        }

        public async Task Start()
        {
            _listener.Start();

            while (true)
            {
                var ctx = await _listener.GetContextAsync();
                Console.Out.WriteLine($"{DateTime.Now} {ctx.Request.HttpMethod}: '{ctx.Request.Url}'");

                ctx.Response.Headers.Add("content-type: application/json; charset=UTF-8");

                var response = "{" + string.Format(ResponseTemplate, DateTime.Now) + "}"; // curly braces are added after template formatting (C# struggles with formatting otherwise)
                using (var sw = new StreamWriter(ctx.Response.OutputStream))
                {
                    await sw.WriteAsync(response);
                    await sw.FlushAsync();
                }
            }
        }

        public async Task Stop()
        {
            await Console.Out.WriteLineAsync("Stopping server...");

            if (_listener.IsListening)
            {
                _listener.Stop();
                _listener.Close();
            }
        }
    }
}
