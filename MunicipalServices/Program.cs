using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServices
{
    internal static class Program
    {
        /// <summary>
        /// Main entry point for the Windows Forms application.
        /// Initializes the application UI and launches the main form (Form1)
        /// Enables visual styles and sets up the rendering defaults
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
