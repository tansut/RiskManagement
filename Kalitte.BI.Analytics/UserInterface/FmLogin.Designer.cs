namespace Kalitte.BI.Analytics.UserInterface
{
    partial class FmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlUsername = new System.Windows.Forms.TextBox();
            this.ctlPassword = new System.Windows.Forms.TextBox();
            this.ctlOk = new System.Windows.Forms.Button();
            this.ctlCancel = new System.Windows.Forms.Button();
            this.ctlForgatPassword = new System.Windows.Forms.LinkLabel();
            this.ctlNewUser = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kullanıcı Adı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Şifre:";
            // 
            // ctlUsername
            // 
            this.ctlUsername.Location = new System.Drawing.Point(118, 11);
            this.ctlUsername.Name = "ctlUsername";
            this.ctlUsername.Size = new System.Drawing.Size(164, 25);
            this.ctlUsername.TabIndex = 0;
            this.ctlUsername.Validating += new System.ComponentModel.CancelEventHandler(this.ctlUsername_Validating);
            // 
            // ctlPassword
            // 
            this.ctlPassword.Location = new System.Drawing.Point(118, 48);
            this.ctlPassword.Name = "ctlPassword";
            this.ctlPassword.PasswordChar = '*';
            this.ctlPassword.Size = new System.Drawing.Size(164, 25);
            this.ctlPassword.TabIndex = 1;
            // 
            // ctlOk
            // 
            this.ctlOk.Location = new System.Drawing.Point(15, 116);
            this.ctlOk.Name = "ctlOk";
            this.ctlOk.Size = new System.Drawing.Size(96, 37);
            this.ctlOk.TabIndex = 2;
            this.ctlOk.Text = "Giriş";
            this.ctlOk.UseVisualStyleBackColor = true;
            this.ctlOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctlCancel
            // 
            this.ctlCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlCancel.Location = new System.Drawing.Point(116, 116);
            this.ctlCancel.Name = "ctlCancel";
            this.ctlCancel.Size = new System.Drawing.Size(96, 37);
            this.ctlCancel.TabIndex = 3;
            this.ctlCancel.Text = "Vazgeç";
            this.ctlCancel.UseVisualStyleBackColor = true;
            this.ctlCancel.Click += new System.EventHandler(this.ctlCancel_Click);
            // 
            // ctlForgatPassword
            // 
            this.ctlForgatPassword.AutoSize = true;
            this.ctlForgatPassword.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ctlForgatPassword.Location = new System.Drawing.Point(115, 86);
            this.ctlForgatPassword.Name = "ctlForgatPassword";
            this.ctlForgatPassword.Size = new System.Drawing.Size(107, 16);
            this.ctlForgatPassword.TabIndex = 7;
            this.ctlForgatPassword.TabStop = true;
            this.ctlForgatPassword.Text = "Şifremi Unuttum";
            this.ctlForgatPassword.Visible = false;
            this.ctlForgatPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ctlForgatPassword_LinkClicked);
            // 
            // ctlNewUser
            // 
            this.ctlNewUser.AutoSize = true;
            this.ctlNewUser.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ctlNewUser.Location = new System.Drawing.Point(14, 86);
            this.ctlNewUser.Name = "ctlNewUser";
            this.ctlNewUser.Size = new System.Drawing.Size(91, 16);
            this.ctlNewUser.TabIndex = 4;
            this.ctlNewUser.TabStop = true;
            this.ctlNewUser.Text = "Yeni Kullanıcı";
            this.ctlNewUser.Visible = false;
            this.ctlNewUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(305, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(269, 81);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 164);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(586, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.toolStripStatusLabel1.LinkColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusLabel1.Text = "analitics.kalitte.com.tr";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(411, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "© 2011 Türkiye Cumhuriyeti Devlet Çevre ve Şehircilik Bakanlığı";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FmLogin
            // 
            this.AcceptButton = this.ctlOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.ctlCancel;
            this.ClientSize = new System.Drawing.Size(586, 186);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ctlNewUser);
            this.Controls.Add(this.ctlForgatPassword);
            this.Controls.Add(this.ctlCancel);
            this.Controls.Add(this.ctlOk);
            this.Controls.Add(this.ctlPassword);
            this.Controls.Add(this.ctlUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmLogin";
            this.Text = "Kullanıcı Girişi";
            this.Load += new System.EventHandler(this.FmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ctlUsername;
        private System.Windows.Forms.TextBox ctlPassword;
        private System.Windows.Forms.Button ctlOk;
        private System.Windows.Forms.Button ctlCancel;
        private System.Windows.Forms.LinkLabel ctlForgatPassword;
        private System.Windows.Forms.LinkLabel ctlNewUser;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}
