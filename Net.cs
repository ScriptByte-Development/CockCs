using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockCs
{
    public class Net
    {
        public static string webstring;
        public static bool WebStringTrue;

        public static void ReadWebString(string website)
        {
            try
            {
                WebClient client = new WebClient();
                webstring = client.DownloadString(website);
                //if you want to print out the string you can ig
                //Messagebox.Show(webstring);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void CompareWebString(string website, string compare)
        {
            try
            {
                WebClient client = new WebClient();
                if (client.DownloadString(website).Contains(compare))
                {
                    //the user could now do a check like
                    /*
                     * if (WebStringTrue)
                     * {
                     *   code here
                     *  }
                     *  else
                     *  {
                     *  
                     *  }
                    */
                    WebStringTrue = true;
                }
                else
                {
                    WebStringTrue = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }
    }
}
