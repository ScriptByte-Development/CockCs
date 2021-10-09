using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CockCs;

namespace CockCsExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CockCs.win32.AllocConsole();
            CockCs.win32.Update(Application.ProductName, "Console started");
        }

        public void button1_Click(object sender, EventArgs e)
        {
            CockCs.Program.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void messageBtn_Click(object sender, EventArgs e)
        {
            CockCs.Program.Message("test", Application.ProductName);
        }

        private void compareFile_Click(object sender, EventArgs e)
        {
            if (CockCs.fstream.FileCompare(@"C:\Users\bossw\source\repos\CockCs\bin\x64\Release\CockCs.dll", @"C:\Users\bossw\source\repos\CockCs\bin\x64\Debug\CockCs.dll"))
            {
                MessageBox.Show("files are equal", Application.ProductName);
                win32.Update(Application.ProductName, "Files are equal");
            }
            else
            {
                MessageBox.Show("files are not equal", Application.ProductName);
                win32.Update(Application.ProductName, "Files are not equal");
            }
        }
    }
}
