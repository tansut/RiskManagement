using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class FmWait : Kalitte.BI.Analytics.UserInterface.BaseForm
    {
        public FmWait()
        {
            InitializeComponent();
        }

        private static FmWait instance;

        static FmWait()
        {
            instance = new FmWait();
        }

        internal static void Execute()
        {
            instance.Show();
            instance.Refresh();
        }

        internal static void Execute(string msg)
        {
            instance.ctlMsg.Text = msg;
            Execute();
        }

        internal static void Update(string msg)
        {
            instance.ctlMsg.Text = msg;
            instance.Refresh();
        }



        internal static void Done()
        {
            instance.Hide();
            instance.ctlMsg.Text = "İşlem devam ederken lütfen bekleyin ...";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
