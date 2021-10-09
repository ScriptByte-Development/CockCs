using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockCs
{
    public class fstream
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void RunFile(string file)
        {
            try
            {
                Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        //return value of 0 indicates that the contents of the files are the same.
        //return a value of something else if the files are not the same.
        public static bool FileCompare(string file1, string file2)
        {
            try
            {
                int file1byte;
                int file2byte;
                FileStream fs1;
                FileStream fs2;

                //check if the files were referenced
                if (file1 == file2)
                {
                    //return true to indicate that the files are the same
                    return true;
                }

                //open the two files
                fs1 = new FileStream(file1, FileMode.Open);
                fs2 = new FileStream(file2, FileMode.Open);

                // Check the file sizes
                if (fs1.Length != fs2.Length)
                {
                    // Close the file
                    fs1.Close();
                    fs2.Close();

                    //return false to indicate files are different
                    return false;
                }

                //read and compare a byte from each file until either a non matching set of bytes is found or until the end of file1 is reached
                do
                {
                    //read one byte from each file.
                    file1byte = fs1.ReadByte();
                    file2byte = fs2.ReadByte();
                }
                while ((file1byte == file2byte) && (file1byte != -1));
                //close files
                fs1.Close();
                fs2.Close();

                //return success of the comparison. file1byte if it is equal to file2byte
                return ((file1byte - file2byte) == 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        //example of how to use this
        //file1 and file2 would want to be the path to the files
        public static void CheckFileSimilaritie(string file1, string file2)
        {
            try
            {
                if (FileCompare(file1, file2))
                {
                    MessageBox.Show("files are equal");
                }
                else
                {
                    MessageBox.Show("files are not equal");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void RenameFile(string currentname, string newname)
        {
            try
            {
                System.IO.File.Move($"{currentname}", $"{newname}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }
    }
}