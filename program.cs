using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace _2020_07_26_Console_Grapher
{
    class program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            string type = String.Empty;
            if(args.Length > 0)
            {
                Application.Run(new grapher(args[0]));
            }
            else
            {
                Application.Run(new grapher());
            }
        }
    }
}
