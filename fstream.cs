using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockCs
{
    internal class fstream
    {
        public static bool FolderExists;
        public static bool FileExists;

        public static void CheckFolderExist(string path)
        {
            try
            {
                bool exists = Directory.Exists(path);
                if (!exists)
                {
                    //the user could now do a check like
                    /*
                     * if (FolderExists)
                     * {
                     *   code here
                     *  }
                     *  else
                     *  {
                     *  
                     *  }
                    */
                    FolderExists = false;
                }
                else if (exists)
                {
                    FolderExists = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void CheckFileExist(string path)
        {
            bool exists = File.Exists(path);
            if (!exists)
            {
                FileExists = false;
            }
            else if (exists)
            {
                FileExists = true;
            }
        }
    }
}