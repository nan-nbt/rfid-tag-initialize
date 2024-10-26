using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RFIDTagInitialize
{
    static class Program
    {
        // Global Variable
        public static string compiledTime = "Compiled: 2024-10-01 09:00";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmOAuth2Login oFrm = new FrmOAuth2Login(true);
            //FrmLogin oFrm = new FrmLogin();
            oFrm.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(oFrm);
        }
    }
}
