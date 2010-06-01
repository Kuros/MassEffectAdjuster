using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessAffinity.Properties;

namespace ProcessAffinity
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            if (Environment.ProcessorCount < 2)
            {
                MessageBox.Show(Resources.SingleCpuMessage, Resources.SingleCpuTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var context = new ProgramApplicationContext())
            {
                Application.Run(context);
            }
		}
	}
}
