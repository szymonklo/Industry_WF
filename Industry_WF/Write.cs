using System;
using System.Collections.Generic;
using System.Text;

namespace Industry_WF
{
    class Write
    {
        private ConsoleColor _defColour = Console.ForegroundColor;
        private ConsoleColor _cityColour = ConsoleColor.Cyan;

        public void HandleProductSold(Facility sender, EventArgs args)
        {
            Console.Write("City:\t");
            Console.ForegroundColor = _cityColour;
            Console.Write(sender.Name);
            Console.ForegroundColor = _defColour;
            Console.Write("\t bought \t");
            //Console.WriteLine(sender.product.);
        }
    }
}
