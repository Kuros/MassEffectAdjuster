using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ProcessAffinity
{
    public class ProcessMonitor
    {
        private const int SLEEP_TIMEOUT = 10000;

        private string processName;
        private Boolean wasShaked;
        private Object syncHandle;
        private Boolean threadRunning;

        public ProcessMonitor(string processName)
        {
            this.processName = processName;
            this.wasShaked = false;
            this.threadRunning = false;
            syncHandle = new Object();
        }

        public void StartMonitor()
        {
            if (!this.threadRunning)
            {
                threadRunning = true;
                Thread thread = new Thread(new ParameterizedThreadStart(ShakerThread));
                thread.Start(this.processName);
            }
        }

        public void StopMonitor()
        {
            threadRunning = false;
        }

        private void ShakerThread(Object parameters)
        {
            String processName = parameters as String;
            Boolean isRunning = true;

            while (isRunning)
            {
                Thread.Sleep(SLEEP_TIMEOUT);

                var processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    if (!wasShaked)
                    {
                        Process process = processes.Single();
                        ProcessAPI.ShakeAffinity(process);
                        wasShaked = true;
                    }

                    else
                    {
                        wasShaked = false;
                    }
                }
                lock (this.syncHandle)
                {
                    isRunning = this.threadRunning;
                }
            }
        }
    }
}
