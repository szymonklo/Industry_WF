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
        public static double Money { get; set; } = 1000;
        public static double Income { get; set; }

        public static double Cost { get; set; }

        public static double Profit { get; set; }

        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Console.SetWindowPosition(1, 1);
            //Console.WindowTop = 0;
            //Console.WriteLine(Console.WindowLeft);

            //przygotowanie delegata
            //public delegate void OnTransactionDoneDelegate(Facility c, EventArgs e);

            //przygotowaæ deklaracjê zdarzenia na podstawie powy¿szego delagata:
            //public event OnTransactionDoneDelegate OnTransactionDone;

            World world = new World();
            Form1 form1 = new Form1();

            form1.Text = "Round: "+Round.RoundNumber;
            Application.Run(form1);

            //Round.Go();


        }
    }
}
