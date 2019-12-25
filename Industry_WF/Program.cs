using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Industry_WF;

namespace Industry_WF
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Console.SetWindowPosition(1, 1);
            
            Console.WindowTop = 0;
            Console.WriteLine("Day 0\n");
            Console.WriteLine(Console.WindowLeft);

            World world = new World();

            Round.Go();

            Application.Run(new Form1());

        }
    }
}
