using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace ProcessAffinity
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		
		}

		

		private void button1_Click(object sender, EventArgs e)
		{
			string name = "FlashBuilder";

			
			try
			{
				Process process = Process.GetProcessesByName(name).Single();
				ShakeAffinity(process);
			}
			catch
			{
			}
			
		}

		private static void ShakeAffinity(Process process)
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
