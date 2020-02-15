using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerLib
{
    class Logger
    {
        const int BUFFER_LENGTH     = 500;
        const int MS_SLEEPING       = 5000;
        const int NUMBER_OF_ERRORS  = 100;

        ConcurrentQueue<string> logStrings = new ConcurrentQueue<string>();
        bool writeToFile;
        bool writeToConsole;
        bool isLogging;

        public Logger(bool writeToFile = false, bool writeToConsole = true)
        {
            this.writeToConsole = writeToConsole;
            this.writeToFile = writeToFile;
            isLogging = false;
        }

        public async Task AddLog(string msg)
        {
            if (isLogging)
            {
                logStrings.Enqueue(msg);
                if (logStrings.Count >= BUFFER_LENGTH)
                    Monitor.Pulse(logStrings);
            }
        }

        public async Task StartLog()
        {
            isLogging = true;

            while (isLogging)
            {
                string msg;
                using (StreamWriter sw = new StreamWriter("log.txt"))
                {
                    int attempts = 0;
                    while (!logStrings.IsEmpty)
                    {
                        if (logStrings.TryDequeue(out msg))
                        {
                            if (attempts == NUMBER_OF_ERRORS)
                                throw new IOException("Queue is empty");
                            attempts++;
                            Thread.Sleep(100);
                            continue;
                        }
                        attempts = 0;

                        if (writeToConsole)
                            await Console.Out.WriteLineAsync(msg);
                        if (writeToFile)
                            await sw.WriteLineAsync(msg);
                    }
                }
                Monitor.Wait(logStrings, MS_SLEEPING);
            }
        }


        public async Task StopLog()
        {
            isLogging = false;
            string msg;

            for (int i = logStrings.Count - 1; i >= 0; i--)
            {
                logStrings.TryDequeue(out msg);
                if (writeToConsole)
                    await Console.Out.WriteLineAsync(msg);
                if (writeToFile)
                {
                    using (StreamWriter sw = new StreamWriter("log.txt"))
                    {
                        await sw.WriteLineAsync(msg);
                    }
                }
            }
        }
    }
}
