using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CockCs
{
	// Token: 0x02000002 RID: 2
	public static class AntiDebug
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		public static void CheckProcesses()
		{
			try
			{
				if (AntiDebug.isadmin)
				{
					MessageBox.Show("Developer build activated, you are now allowed to use black listed programs");
				}
				else
				{
					if (Process.GetProcessesByName("dnSpy").Length != 0)
					{
						foreach (Process process in Process.GetProcessesByName("dnSpy"))
						{
							process.Kill();
							process.WaitForExit();
							process.Dispose();
						}
					}
					if (Process.GetProcessesByName("JustDecompile").Length != 0)
					{
						foreach (Process process2 in Process.GetProcessesByName("JustDecompile"))
						{
							process2.Kill();
							process2.WaitForExit();
							process2.Dispose();
						}
					}
					if (Process.GetProcessesByName("cheatengine-x86_64-SSE4-AVX2").Length != 0)
					{
						foreach (Process process3 in Process.GetProcessesByName("cheatengine-x86_64-SSE4-AVX2"))
						{
							process3.Kill();
							process3.WaitForExit();
							process3.Dispose();
						}
					}
					if (Process.GetProcessesByName("ida").Length != 0)
					{
						foreach (Process process4 in Process.GetProcessesByName("ida"))
						{
							process4.Kill();
							process4.WaitForExit();
							process4.Dispose();
						}
					}
					if (Process.GetProcessesByName("ida64").Length != 0)
					{
						foreach (Process process5 in Process.GetProcessesByName("ida64"))
						{
							process5.Kill();
							process5.WaitForExit();
							process5.Dispose();
						}
					}
					if (Process.GetProcessesByName("de4dot-x64").Length != 0)
					{
						foreach (Process process6 in Process.GetProcessesByName("ida64"))
						{
							process6.Kill();
							process6.WaitForExit();
							process6.Dispose();
						}
					}
				}
			}
			catch (Exception)
			{
				Environment.Exit(0);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021F8 File Offset: 0x000003F8
		public static void SetTimer()
		{
			Timer timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += AntiDebug.timer_Tick;
			timer.Start();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002221 File Offset: 0x00000421
		public static void timer_Tick(object sender, EventArgs e)
		{
			AntiDebug.CheckProcesses();
		}

		// Token: 0x04000001 RID: 1
		public static bool isadmin;
	}
}
