using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Func<HttpListenerContext, Dictionary<string, string[]>, Task>, 
            Dictionary<string, string[]>> urlMap;
        
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
                Func<HttpListenerContext, Dictionary<string, string[]>, Task>,
                Dictionary<string, string[]>>();

            Dictionary<string, string[]> urlMethodsMap = new Dictionary<string, string[]>();
            foreach (var type in apiClasses)
            {
                foreach (var method in type.GetMethods())
                {
                    urlMethodsMap = new Dictionary<string, string[]>();
                    if (Attribute.IsDefined(method, typeof(ApiEndpointAttribute)))
                    {
                        var meta = (ApiEndpointAttribute[])Attribute.GetCustomAttributes(method, typeof(ApiEndpointAttribute));
                        foreach (var att in meta)
                            urlMethodsMap["http://{0}:{1}" + att.URL] = (att.httpMethods.Length > 0) ? att.httpMethods : new string[] { "GET" };
                    }
                    if (urlMethodsMap.Count == 0)
                        continue;
                    
                    // TODO: handler decorator

                    // assuming ApiEndpoint attributes are used correctly (public static async Task ...(HttpListenerContext) { ... })
                    var handler = (Func<HttpListenerContext, Task<string>>) Delegate.CreateDelegate(
                        typeof(Func<HttpListenerContext, Task<string>>), type, method.Name);
                    var wrappedHandler = (Func<HttpListenerContext, Dictionary<string, string[]>, Task>) Delegate.CreateDelegate(
                        typeof(Func<HttpListenerContext, Dictionary<string, string[]>, Task>), 
                        new Handler(handler), 
                        "Handle");
                    urlMap.Add(wrappedHandler, urlMethodsMap);
                    
                }
            }

        }

        // TODO: REST url (with arguments)
        // Note: actually, urls with arguments are matched to the closest existing url,
        // so it can be checked in the decorator before the handler call
        private void AddListener(Func<HttpListenerContext, Dictionary<string, string[]>, Task> handler, Dictionary<string, string[]> urlMethodsMap)
        {
            _listeners.Add(new AsyncHttpListener(_max_load, urlMethodsMap, handler));
        }

        // Start listeners (not awaiting!)
        public async Task Start()
        {
            if (Port == 0)
                Port = 8080;

            foreach (var handler in urlMap.Keys)
            {
                var finalMap = new Dictionary<string, string[]>();
                foreach (var url in urlMap[handler].Keys)
                    finalMap[string.Format(url, HOST, Port)] = urlMap[handler][url];
                AddListener(handler, finalMap);
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
