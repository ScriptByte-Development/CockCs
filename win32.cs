using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CockCs
{
    public class win32
    {
        /*This class basically lets you add a console to a winform project
         *This is normally not an option under the project settings but CockCs is just built different man.
         *Git gud skrub
         */


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        //allocate console
        public static extern bool AllocConsole();

        //use this function when your application starts + when you want to update the console :)
        public static void Update(string title, string message)
        {
            Console.Title = title;
            Console.WriteLine(message);
        }
    }
}
