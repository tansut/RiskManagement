using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kalitte.BI.Analytics.UserInterface;

namespace Kalitte.BI.Analytics
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            if (FmLogin.Execute())

                Application.Run(new MainForm());
            else Application.Exit();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            FmUnhandled.Execute(e.Exception);
        }
    }
}
