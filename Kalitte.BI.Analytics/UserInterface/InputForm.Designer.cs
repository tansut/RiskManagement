namespace Kalitte.BI.Analytics.UserInterface
{
    partial class InputForm
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
            this.ctlReportName = new System.Windows.Forms.TextBox();
            this.ctlCancel = new System.Windows.Forms.Button();
            this.ctlOk = new System.Windows.Forms.Button();
            this.ctlSaveAsTemplate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ctlReportName
            // 
            this.ctlReportName.Location = new System.Drawing.Point(12, 10);
            this.ctlReportName.Name = "ctlReportName";
            this.ctlReportName.Size = new System.Drawing.Size(398, 20);
            this.ctlReportName.TabIndex = 1;
            this.ctlReportName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlReportName_KeyDown);
            // 
            // ctlCancel
            // 
            this.ctlCancel.Location = new System.Drawing.Point(335, 36);
            this.ctlCancel.Name = "ctlCancel";
            this.ctlCancel.Size = new System.Drawing.Size(75, 23);
            this.ctlCancel.TabIndex = 2;
            this.ctlCancel.Text = "İptal";
            this.ctlCancel.UseVisualStyleBackColor = true;
            this.ctlCancel.Click += new System.EventHandler(this.ctlCancel_Click);
            // 
            // ctlOk
            // 
            this.ctlOk.Location = new System.Drawing.Point(252, 36);
            this.ctlOk.Name = "ctlOk";
            this.ctlOk.Size = new System.Drawing.Size(75, 23);
            this.ctlOk.TabIndex = 3;
            this.ctlOk.Text = "Tamam";
            this.ctlOk.UseVisualStyleBackColor = true;
            this.ctlOk.Click += new System.EventHandler(this.ctlOk_Click);
            // 
            // ctlSaveAsTemplate
            // 
            this.ctlSaveAsTemplate.AutoSize = true;
            this.ctlSaveAsTemplate.Location = new System.Drawing.Point(12, 37);
            this.ctlSaveAsTemplate.Name = "ctlSaveAsTemplate";
            this.ctlSaveAsTemplate.Size = new System.Drawing.Size(126, 17);
            this.ctlSaveAsTemplate.TabIndex = 4;
            this.ctlSaveAsTemplate.Text = "Şablon olarak kaydet";
            this.ctlSaveAsTemplate.UseVisualStyleBackColor = true;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 66);
            this.Controls.Add(this.ctlSaveAsTemplate);
            this.Controls.Add(this.ctlOk);
            this.Controls.Add(this.ctlCancel);
            this.Controls.Add(this.ctlReportName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lütfen Rapor Adını Giriniz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ctlReportName;
        private System.Windows.Forms.Button ctlCancel;
        private System.Windows.Forms.Button ctlOk;
        public System.Windows.Forms.CheckBox ctlSaveAsTemplate;
    }
}