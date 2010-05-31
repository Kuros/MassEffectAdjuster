using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using System.Diagnostics;

namespace ProcessAffinity
{
	public class ProgramApplicationContext : ApplicationContext
	{
        private const String MASS_EFFECT_2 = "MassEffect2";

		private NotifyIcon notifyIcon;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem exitToolStripMenuItem;
        private ProcessMonitor monitor;

		public ProgramApplicationContext()
		{
			this.notifyIcon = new System.Windows.Forms.NotifyIcon();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();


			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon.Icon = Properties.Resources.notifyIcon;
			this.notifyIcon.Text = "Mass Effect 2 Performance Adjuster";
			this.notifyIcon.Visible = true;
			
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);

            monitor = new ProcessMonitor(MASS_EFFECT_2);
            monitor.StartMonitor();

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
            monitor.StopMonitor();
			Application.Exit();
		}

		
	

	}
}
