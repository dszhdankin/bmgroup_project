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
        private readonly Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task> _callback;
        public IEnumerable<ApiEndpointUrl> urls;

        public AsyncHttpListener(uint maxRequests, IEnumerable<ApiEndpointUrl> urls, Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task> callback)
        {
            _listener = new HttpListener();
            if (urls.Count() == 0)
                throw new ArgumentException("Empty URL list");

            foreach (var url in urls)
                _listener.Prefixes.Add(url.GetAbsoluteUrl());

            _width          = maxRequests;
            _callback       = callback;
            _run            = true;
            _requestSet     = new HashSet<Task>();
            this.urls       = urls;
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
                        // TODO: decide if the callback tasks should be in the _requestSet
                        _requestSet.Add(_callback(context, urls));                      // add the async callback task to the queue
                        _requestSet.Add(_listener.GetContextAsync());                   // add new Task<HttpListenerContext> (we have removed one before)
                    }
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
            await Console.Out.WriteLineAsync("Stopping listener...");

            _run = false;
            if (_listener.IsListening)
            {
                _listener.Stop();
                _listener.Close();
            }
        }
    }
}
