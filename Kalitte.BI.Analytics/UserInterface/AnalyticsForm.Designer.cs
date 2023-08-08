namespace Kalitte.BI.Analytics.UserInterface
{
    partial class AnalyticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyticsForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctlNewWindow = new System.Windows.Forms.ToolStripButton();
            this.ctlSaveReport = new System.Windows.Forms.ToolStripButton();
            this.ctlShowReport = new System.Windows.Forms.ToolStripButton();
            this.ctlExportExcel = new System.Windows.Forms.ToolStripButton();
            this.btnAddtoDashboard = new System.Windows.Forms.ToolStripButton();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(796, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(389, 17);
            this.toolStripStatusLabel1.Text = "© 2011 T.C Bayındırlık Bakanlığı Risk Değerlendirme ve Analiz Sistemi";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolBar
            // 
            this.toolBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctlNewWindow,
            this.toolStripSeparator1,
            this.ctlSaveReport,
            this.ctlShowReport,
            this.ctlExportExcel});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(796, 39);
            this.toolBar.TabIndex = 2;
            this.toolBar.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // ctlNewWindow
            // 
            this.ctlNewWindow.Image = ((System.Drawing.Image)(resources.GetObject("ctlNewWindow.Image")));
            this.ctlNewWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlNewWindow.Name = "ctlNewWindow";
            this.ctlNewWindow.Size = new System.Drawing.Size(111, 36);
            this.ctlNewWindow.Text = "Yeni Pencere";
            this.ctlNewWindow.Click += new System.EventHandler(this.ctlNewWindow_Click_1);
            // 
            // ctlSaveReport
            // 
            this.ctlSaveReport.Image = ((System.Drawing.Image)(resources.GetObject("ctlSaveReport.Image")));
            this.ctlSaveReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlSaveReport.Name = "ctlSaveReport";
            this.ctlSaveReport.Size = new System.Drawing.Size(138, 36);
            this.ctlSaveReport.Text = "Raporumu Kaydet";
            this.ctlSaveReport.Click += new System.EventHandler(this.ctlSaveReport_Click);
            // 
            // ctlShowReport
            // 
            this.ctlShowReport.Image = ((System.Drawing.Image)(resources.GetObject("ctlShowReport.Image")));
            this.ctlShowReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlShowReport.Name = "ctlShowReport";
            this.ctlShowReport.Size = new System.Drawing.Size(141, 36);
            this.ctlShowReport.Text = "Raporlarımı Göster";
            this.ctlShowReport.Click += new System.EventHandler(this.ctlShowReport_Click);
            // 
            // ctlExportExcel
            // 
            this.ctlExportExcel.Image = global::Kalitte.BI.Analytics.Properties.Resources.excel8;
            this.ctlExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctlExportExcel.Name = "ctlExportExcel";
            this.ctlExportExcel.Size = new System.Drawing.Size(109, 36);
            this.ctlExportExcel.Text = "Excel\'e Aktar";
            this.ctlExportExcel.Click += new System.EventHandler(this.ctlExportExcel_Click);
            // 
            // btnAddtoDashboard
            // 
            this.btnAddtoDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnAddtoDashboard.Image")));
            this.btnAddtoDashboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddtoDashboard.Name = "btnAddtoDashboard";
            this.btnAddtoDashboard.Size = new System.Drawing.Size(162, 22);
            this.btnAddtoDashboard.Text = "Gösterge Panelime Ekle ...";
            this.btnAddtoDashboard.Click += new System.EventHandler(this.SendToUserDahsboardBtn_Click);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(107, 22);
            this.newToolStripButton.Text = "Yeni Pencere ...";
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(796, 437);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusStrip1);
            this.Name = "AnalyticsForm";
            this.Activated += new System.EventHandler(this.AnalyticsForm_Activated);
            this.Load += new System.EventHandler(this.AnalyticsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAddtoDashboard;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton ctlNewWindow;
        private System.Windows.Forms.ToolStripButton ctlSaveReport;
        private System.Windows.Forms.ToolStripButton ctlShowReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ctlExportExcel;

    }
}
