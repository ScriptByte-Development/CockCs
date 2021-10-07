using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockCs
{
    public class Program
    {
        public static void Message(string text, string label)
        {
            try
            {
                MessageBox.Show(text, label);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void Close()
        {
            Environment.Exit(0);
        }
    }
}
