using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class FmUnhandled : Kalitte.BI.Analytics.UserInterface.BaseForm
    {
        public FmUnhandled()
        {
            InitializeComponent();
        }

        public static void Execute(Exception exc)
        {
            FmUnhandled f = new FmUnhandled();
            f.msgTextBox.Text = exc.Message;
            f.ShowDialog();

        }

    }
}
