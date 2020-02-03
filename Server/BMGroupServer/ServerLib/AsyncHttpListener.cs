using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Server
{
    // TODO: implement load balancing (dynamically changing _width)
    internal class AsyncHttpListener
    {
        private readonly uint _width;
        private readonly HashSet<Task> _requestSet;                                      // TODO: compare with (Concurrent?)Queue
        private bool _run;
        private readonly HttpListener _listener;
        private readonly Func<HttpListenerContext, Task> _callback;

        public AsyncHttpListener(uint maxRequests, IEnumerable<string> urls, Func<HttpListenerContext, Task> callback)
        {
            _listener = new HttpListener();
            if (urls.Count() == 0)
                throw new ArgumentException("Empty URL list");

            foreach (string url in urls)
                _listener.Prefixes.Add(url);

            _width      = maxRequests;
            _callback   = callback;
            _run        = true;
            _requestSet = new HashSet<Task>();
        }

        public async Task Start()
        {
            _listener.Start();
            // filling the request set
            for (uint i = 0; i < _width; ++i)
                _requestSet.Add(_listener.GetContextAsync());

            while (_run)
            {
                try
                {
                    // https://stackoverflow.com/questions/9034721/handling-multiple-requests-with-c-sharp-httplistener

                    // await completes on request or callback
                    Task t = await Task.WhenAny(_requestSet);
                    _requestSet.Remove(t);
                    if (t is Task<HttpListenerContext>)                                 // if t was created by _listener.GetContextAsync()  (callbacks are processed on their own)
                    {
                        var context = (t as Task<HttpListenerContext>).Result;
                        // TODO: implement a logging system worth using
                        Console.Out.WriteLine($"{DateTime.Now} {context.Request.HttpMethod}: '{context.Request.Url}'");

                        context.Response.ContentType = "application/json";
                        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                        // TODO: decide if the callback tasks should be in the _requestSet
                        _requestSet.Add(_callback(context));                            // add the async callback task to the queue
                        _requestSet.Add(_listener.GetContextAsync());                   // add new Task<HttpListenerContext> (we have removed one before)
                    }
                    // if something needs to be done with a callback right after its launch it should be done here, in the else clause
                }
                catch (AggregateException e)
                {
                    if (!_run && e.InnerException is ObjectDisposedException)
                        return;
                    Console.WriteLine($"Callback exception:\n{e.InnerException.Message}");
                }
                catch (HttpListenerException e)
                {
                    Console.WriteLine($"HttpListenerException:\n{e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected exception: {e.Message}");
                    _listener.Stop();
                    return;
                }
            }
        }

        public async Task Stop()
        {
            await Console.Out.WriteLineAsync("Stopping server...");

            _run = false;
            if (_listener.IsListening)
            {
                _listener.Stop();
                _listener.Close();
            }
        }
    }
}
