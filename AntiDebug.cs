using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockCs
{
    public static class AntiDebug
    {
        public static bool isadmin;

        public static void CheckProcesses()
        {
            try
            {
                if (isadmin)
                {
                    MessageBox.Show("Developer build activated, you are now allowed to use black listed programs");
                    return;
                }
                if (Process.GetProcessesByName("dnSpy").Length > 0)
                {
                    Process[] proc = Process.GetProcessesByName("dnSpy");
                    foreach (Process worker in proc)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }
                if (Process.GetProcessesByName("JustDecompile").Length > 0)
                {
                    Process[] proc = Process.GetProcessesByName("JustDecompile");
                    foreach (Process worker in proc)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }
                if (Process.GetProcessesByName("cheatengine-x86_64-SSE4-AVX2").Length > 0)
                {
                    Process[] proc = Process.GetProcessesByName("cheatengine-x86_64-SSE4-AVX2");
                    foreach (Process worker in proc)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }
                if (Process.GetProcessesByName("ida").Length > 0)
                {
                    Process[] proc = Process.GetProcessesByName("ida");
                    foreach (Process worker in proc)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }
                if (Process.GetProcessesByName("ida64").Length > 0)
                {
                    Process[] proc = Process.GetProcessesByName("ida64");
                    foreach (Process worker in proc)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }
                if (Process.GetProcessesByName("de4dot-x64").Length > 0)
                {
                    Process[] proc = Process.GetProcessesByName("ida64");
                    foreach (Process worker in proc)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                //if you are using this function if an error happens its most likely someone tampering with the application so its best to close it
                Environment.Exit(0);
            }
        }

        //this will start a timer to run the CheckProcesses function every 1 second
        public static void SetTimer()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 1000; //every second
            t.Tick += new EventHandler(timer_Tick);
            t.Start();
        }

        public static void timer_Tick(object sender, EventArgs e)
        {
            CheckProcesses();
        }

    }
}
