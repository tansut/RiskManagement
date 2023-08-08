namespace Kalitte.BI.Analytics.UserInterface
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.moduleListImgs = new System.Windows.Forms.ImageList(this.components);
            this.ModuleListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openModule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
            this.modulesList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.executeCurrentMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.lvBigIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.LvSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.LvList = new System.Windows.Forms.ToolStripMenuItem();
            this.LvDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.LvTiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.hakkındaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModuleListMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // moduleListImgs
            // 
            this.moduleListImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("moduleListImgs.ImageStream")));
            this.moduleListImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.moduleListImgs.Images.SetKeyName(0, "book_48.png");
            this.moduleListImgs.Images.SetKeyName(1, "seta.png");
            this.moduleListImgs.Images.SetKeyName(2, "User Accounts.png");
            this.moduleListImgs.Images.SetKeyName(3, "contact-48.png");
            this.moduleListImgs.Images.SetKeyName(4, "Gecmis.png");
            this.moduleListImgs.Images.SetKeyName(5, "Kontrol.png");
            this.moduleListImgs.Images.SetKeyName(6, "Risk.png");
            this.moduleListImgs.Images.SetKeyName(7, "Surec.png");
            // 
            // ModuleListMenu
            // 
            this.ModuleListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openModule});
            this.ModuleListMenu.Name = "ModuleListMenu";
            this.ModuleListMenu.Size = new System.Drawing.Size(124, 26);
            // 
            // openModule
            // 
            this.openModule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.openModule.Name = "openModule";
            this.openModule.Size = new System.Drawing.Size(123, 22);
            this.openModule.Text = "Çalıştır ...";
            this.openModule.Click += new System.EventHandler(this.openModule_Click);
            // 
            // toolBarImages
            // 
            this.toolBarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImages.ImageStream")));
            this.toolBarImages.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarImages.Images.SetKeyName(0, "pencil.png");
            // 
            // modulesList
            // 
            this.modulesList.BackColor = System.Drawing.Color.White;
            this.modulesList.BackgroundImage = global::Kalitte.BI.Analytics.Properties.Resources.cevrevesehircilik_logo;
            this.modulesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.modulesList.ContextMenuStrip = this.ModuleListMenu;
            this.modulesList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.modulesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modulesList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.modulesList.LargeImageList = this.moduleListImgs;
            this.modulesList.Location = new System.Drawing.Point(0, 31);
            this.modulesList.Margin = new System.Windows.Forms.Padding(5);
            this.modulesList.Name = "modulesList";
            this.modulesList.Size = new System.Drawing.Size(771, 526);
            this.modulesList.TabIndex = 3;
            this.modulesList.TileSize = new System.Drawing.Size(400, 45);
            this.modulesList.UseCompatibleStateImageBehavior = false;
            this.modulesList.View = System.Windows.Forms.View.Tile;
            this.modulesList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.modulesList_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Analiz Modülü";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Açıklama";
            this.columnHeader2.Width = 250;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 557);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.statusStrip1.Size = new System.Drawing.Size(771, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 18;
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
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(632, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "© 2011 Türkiye Cumhuriyeti Devlet Çevre ve Şehircilik Bakanlığı";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeCurrentMenuItem,
            this.toolStripSeparator3,
            this.toolStripSplitButton1,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(771, 31);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // executeCurrentMenuItem
            // 
            this.executeCurrentMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.executeCurrentMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("executeCurrentMenuItem.Image")));
            this.executeCurrentMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.executeCurrentMenuItem.Name = "executeCurrentMenuItem";
            this.executeCurrentMenuItem.Size = new System.Drawing.Size(28, 28);
            this.executeCurrentMenuItem.Text = "Seçili modülü çalıştır ...";
            this.executeCurrentMenuItem.Click += new System.EventHandler(this.executeCurrentMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lvBigIcons,
            this.LvSmallIcons,
            this.LvList,
            this.LvDetails,
            this.toolStripSeparator1,
            this.LvTiles});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(143, 28);
            this.toolStripSplitButton1.Text = "Modül Görünümü";
            // 
            // lvBigIcons
            // 
            this.lvBigIcons.Name = "lvBigIcons";
            this.lvBigIcons.Size = new System.Drawing.Size(159, 22);
            this.lvBigIcons.Text = "Büyük Resimler";
            this.lvBigIcons.Click += new System.EventHandler(this.LvViewclick);
            // 
            // LvSmallIcons
            // 
            this.LvSmallIcons.Name = "LvSmallIcons";
            this.LvSmallIcons.Size = new System.Drawing.Size(159, 22);
            this.LvSmallIcons.Text = "Küçük Resimler";
            this.LvSmallIcons.Click += new System.EventHandler(this.LvViewclick);
            // 
            // LvList
            // 
            this.LvList.Name = "LvList";
            this.LvList.Size = new System.Drawing.Size(159, 22);
            this.LvList.Text = "Liste Görünümü";
            this.LvList.Click += new System.EventHandler(this.LvViewclick);
            // 
            // LvDetails
            // 
            this.LvDetails.Name = "LvDetails";
            this.LvDetails.Size = new System.Drawing.Size(159, 22);
            this.LvDetails.Text = "Detaylar";
            this.LvDetails.Click += new System.EventHandler(this.LvViewclick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // LvTiles
            // 
            this.LvTiles.Checked = true;
            this.LvTiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LvTiles.Name = "LvTiles";
            this.LvTiles.Size = new System.Drawing.Size(159, 22);
            this.LvTiles.Text = "Karo Görünümü";
            this.LvTiles.Click += new System.EventHandler(this.LvViewclick);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenuItem,
            this.toolStripSeparator2,
            this.hakkındaToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(82, 28);
            this.toolStripDropDownButton1.Text = "Yardım";
            this.toolStripDropDownButton1.Visible = false;
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(159, 22);
            this.HelpMenuItem.Text = "Yardım Konuları";
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // hakkındaToolStripMenuItem
            // 
            this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            this.hakkındaToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.hakkındaToolStripMenuItem.Text = "Hakkında";
            this.hakkındaToolStripMenuItem.Click += new System.EventHandler(this.hakkındaToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(771, 579);
            this.Controls.Add(this.modulesList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "T.C. Çevre ve Şehircilik Bakanlığı Risk Sense Dinamik Karar Destek Aracı";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ModuleListMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList moduleListImgs;
        private System.Windows.Forms.ListView modulesList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList toolBarImages;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem lvBigIcons;
        private System.Windows.Forms.ToolStripMenuItem LvList;
        private System.Windows.Forms.ToolStripMenuItem LvSmallIcons;
        private System.Windows.Forms.ToolStripMenuItem LvDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem LvTiles;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem hakkındaToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ModuleListMenu;
        private System.Windows.Forms.ToolStripMenuItem openModule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton executeCurrentMenuItem;

    }
}
