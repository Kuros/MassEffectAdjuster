using System;
using System.Diagnostics;
using System.Threading;
using System.Security;
using System.Security.Permissions;

namespace ProcessAffinity
{
	public static class ProcessAPI
	{
        [SecurityPermission(SecurityAction.LinkDemand)]
		public static void ShakeAffinity(Process process)
		{
			IntPtr affinity = process.ProcessorAffinity;

            if (process.ProcessorAffinity.ToInt64() > 1)
            {
                IntPtr newAffinity = (IntPtr)(affinity.ToInt64() ^ 2);
                process.ProcessorAffinity = newAffinity;
                Thread.Sleep(500);
                process.ProcessorAffinity = affinity;
            }
		}
	}
}
