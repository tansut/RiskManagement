namespace Kalitte.BI.Analytics.UserInterface
{
    partial class UserReportsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctlDataGrid = new System.Windows.Forms.DataGridView();
            this.ctlId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctlReportName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctlCreationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlOpenSelectedReport = new System.Windows.Forms.Button();
            this.ctlClose = new System.Windows.Forms.Button();
            this.ctlDeleteReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctlDataGrid
            // 
            this.ctlDataGrid.AllowUserToAddRows = false;
            this.ctlDataGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.ctlDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ctlDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ctlDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ctlDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctlDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ctlId,
            this.ctlReportName,
            this.ctlCreationDate});
            this.ctlDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid.MultiSelect = false;
            this.ctlDataGrid.Name = "ctlDataGrid";
            this.ctlDataGrid.ReadOnly = true;
            this.ctlDataGrid.RowHeadersVisible = false;
            this.ctlDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctlDataGrid.Size = new System.Drawing.Size(408, 310);
            this.ctlDataGrid.TabIndex = 0;
            this.ctlDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ctlDataGrid_CellDoubleClick);
            // 
            // ctlId
            // 
            this.ctlId.DataPropertyName = "ReportId";
            this.ctlId.HeaderText = "Id";
            this.ctlId.Name = "ctlId";
            this.ctlId.ReadOnly = true;
            this.ctlId.Visible = false;
            this.ctlId.Width = 22;
            // 
            // ctlReportName
            // 
            this.ctlReportName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ctlReportName.DataPropertyName = "ReportName";
            this.ctlReportName.HeaderText = "Rapor";
            this.ctlReportName.Name = "ctlReportName";
            this.ctlReportName.ReadOnly = true;
            // 
            // ctlCreationDate
            // 
            this.ctlCreationDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ctlCreationDate.DataPropertyName = "CreationDate";
            this.ctlCreationDate.HeaderText = "Tarih";
            this.ctlCreationDate.Name = "ctlCreationDate";
            this.ctlCreationDate.ReadOnly = true;
            this.ctlCreationDate.Width = 56;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctlDeleteReport);
            this.panel1.Controls.Add(this.ctlOpenSelectedReport);
            this.panel1.Controls.Add(this.ctlClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 279);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 31);
            this.panel1.TabIndex = 1;
            // 
            // ctlOpenSelectedReport
            // 
            this.ctlOpenSelectedReport.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctlOpenSelectedReport.Location = new System.Drawing.Point(229, 0);
            this.ctlOpenSelectedReport.Name = "ctlOpenSelectedReport";
            this.ctlOpenSelectedReport.Size = new System.Drawing.Size(110, 31);
            this.ctlOpenSelectedReport.TabIndex = 1;
            this.ctlOpenSelectedReport.Text = "Seçili Raporu Aç";
            this.ctlOpenSelectedReport.UseVisualStyleBackColor = true;
            this.ctlOpenSelectedReport.Click += new System.EventHandler(this.ctlOpenSelectedReport_Click);
            // 
            // ctlClose
            // 
            this.ctlClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctlClose.Location = new System.Drawing.Point(339, 0);
            this.ctlClose.Name = "ctlClose";
            this.ctlClose.Size = new System.Drawing.Size(69, 31);
            this.ctlClose.TabIndex = 0;
            this.ctlClose.Text = "Kapat";
            this.ctlClose.UseVisualStyleBackColor = true;
            this.ctlClose.Click += new System.EventHandler(this.ctlClose_Click);
            // 
            // ctlDeleteReport
            // 
            this.ctlDeleteReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlDeleteReport.Location = new System.Drawing.Point(0, 0);
            this.ctlDeleteReport.Name = "ctlDeleteReport";
            this.ctlDeleteReport.Size = new System.Drawing.Size(102, 31);
            this.ctlDeleteReport.TabIndex = 2;
            this.ctlDeleteReport.Text = "Seçili Raporu Sil";
            this.ctlDeleteReport.UseVisualStyleBackColor = true;
            this.ctlDeleteReport.Click += new System.EventHandler(this.ctlDeleteReport_Click);
            // 
            // UserReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(408, 310);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctlDataGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserReportsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raporlarım";
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ctlDataGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ctlOpenSelectedReport;
        private System.Windows.Forms.Button ctlClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctlId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctlReportName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctlCreationDate;
        private System.Windows.Forms.Button ctlDeleteReport;
    }
}