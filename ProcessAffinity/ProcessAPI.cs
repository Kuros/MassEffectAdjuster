using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace ProcessAffinity
{
	public static class ProcessAPI
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern Boolean SetProcessAffinityMask(IntPtr hProcess, IntPtr dwProcessAffinityMask);
		
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetProcessAffinityMask(IntPtr hProcess, out IntPtr lpProcessAffinityMask, out IntPtr lpSystemAffinityMask);

		public static void ShakeAffinity(Process process)
		{
			IntPtr affinity, systemAffinity;
			ProcessAPI.GetProcessAffinityMask(process.Handle, out affinity, out systemAffinity);
			IntPtr newAffinity = (IntPtr)(affinity.ToInt64() ^ 2);
			ProcessAPI.SetProcessAffinityMask(process.Handle, newAffinity);
			Thread.Sleep(500);
			ProcessAPI.SetProcessAffinityMask(process.Handle, affinity);
		}
	}
}
