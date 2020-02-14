using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    public class AsyncHttpServer
    {
        private const string HOST = "localhost";
        private readonly uint _max_load;
        private readonly List<AsyncHttpListener> _listeners;
        private readonly HashSet<Task> _tasks;

        private Dictionary<
            Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task>, 
            IEnumerable<ApiEndpointUrl>> urlMap;
        
        private uint _port = 0;
        public uint Port 
        {
            get => _port; 
            set
            {
                if (_port != 0)
                    throw new Exception("Port reseting is allowed only once");
                _port = value;
                
            } 
        }

        public AsyncHttpServer(uint maxLoad, params Type[] apiClasses)
        {
            _listeners  = new List<AsyncHttpListener>();
            _max_load   = maxLoad;
            _tasks      = new HashSet<Task>();
            urlMap      = new Dictionary<
                Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task>,
                IEnumerable<ApiEndpointUrl>>();

            List<ApiEndpointUrl> urls = new List<ApiEndpointUrl>();
            foreach (var type in apiClasses)
            {
                foreach (var method in type.GetMethods())
                {
                    urls = new List<ApiEndpointUrl>();
                    if (Attribute.IsDefined(method, typeof(ApiEndpointAttribute)))
                    {
                        var meta = (ApiEndpointAttribute[])Attribute.GetCustomAttributes(method, typeof(ApiEndpointAttribute));
                        foreach (var att in meta)
                            urls.Add(att.URL);
                    }
                    if (urls.Count == 0)
                        continue;
                    
                    // assuming ApiEndpoint attributes are used correctly (public static async Task ...(HttpListenerContext) { ... })
                    var handler = (Func<HttpListenerContext, Task<string>>) Delegate.CreateDelegate(
                        typeof(Func<HttpListenerContext, Task<string>>), type, method.Name);
                    var wrappedHandler = (Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task>) Delegate.CreateDelegate(
                        typeof(Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task>), 
                        new Handler(handler), 
                        "Handle");
                    urlMap.Add(wrappedHandler, urls);
                    
                }
            }
        }

        private void AddListener(Func<HttpListenerContext, IEnumerable<ApiEndpointUrl>, Task> handler, IEnumerable<ApiEndpointUrl> urls)
        {
            _listeners.Add(new AsyncHttpListener(_max_load, urls, handler));
        }

        // Start listeners (not awaiting!)
        public async Task Start()
        {
            if (Port == 0)
                Port = 8080;

            foreach (var handler in urlMap.Keys)
            {
                foreach (var url in urlMap[handler])
                    url.absolutePrefix = $"http://{HOST}:{Port}";
                AddListener(handler, urlMap[handler]);
            }

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
