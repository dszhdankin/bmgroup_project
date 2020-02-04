using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    public class AsyncHttpServer
    {
        private readonly uint _max_load;
        private readonly List<AsyncHttpListener> _listeners;
        private readonly HashSet<Task> _tasks;
        public AsyncHttpServer(uint max_load)
        {
            _listeners  = new List<AsyncHttpListener>();
            _max_load   = max_load;
            _tasks      = new HashSet<Task>();
        }

        // TODO: REST url (with arguments)
        // Note: actually, urls with arguments are matched to the closest existing url,
        // so it can be checked in the decorator before the handler call
        public void AddListener(IEnumerable<string> urls, Func<HttpListenerContext, Task> handler)
        {
            _listeners.Add(new AsyncHttpListener(_max_load, urls, handler));
        }

        public async Task Start()
        {
            foreach (var listener in _listeners)
            {
                _tasks.Add(listener.Start());
                await Console.Out.WriteLineAsync("Started a listener");
            }
        }

        public async Task Stop()
        {
            foreach (var listener in _listeners)
                await listener.Stop();
        }
    }
}
