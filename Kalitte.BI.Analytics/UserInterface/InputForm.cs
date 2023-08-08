using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kalitte.BI.Shared.ServiceManagement;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class InputForm : Form
    {

        public string Input { get; private set; }

        public InputForm(string formTitle)
        {
            InitializeComponent();
            InitUI(formTitle);
        }

        private void InitUI(string formTitle)
        {
            this.Text = formTitle;
            //todo roles
        }


        private void ctlCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ctlOk_Click(object sender, EventArgs e)
        {
            Input = ctlReportName.Text.Trim();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ctlReportName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ctlOk_Click(sender, new EventArgs());
        }
    }
}
