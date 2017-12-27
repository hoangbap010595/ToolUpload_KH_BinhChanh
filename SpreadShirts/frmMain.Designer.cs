namespace SpreadShirts
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableShop = new DevExpress.XtraEditors.XtraScrollableControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.lsBoxLog = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControlFile = new DevExpress.XtraEditors.GroupControl();
            this.ckUsingFileUpload = new DevExpress.XtraEditors.CheckEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnWriteLog = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFileExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.panelControlAction = new DevExpress.XtraEditors.PanelControl();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.memoTag = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPrice = new DevExpress.XtraEditors.TextEdit();
            this.lsBoxImage = new DevExpress.XtraEditors.ListBoxControl();
            this.btnChooesImage = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsBoxLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlFile)).BeginInit();
            this.groupControlFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckUsingFileUpload.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAction)).BeginInit();
            this.panelControlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoTag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(7, 159);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 58);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(7, 73);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(85, 55);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "simpleButton2";
            this.simpleButton2.Visible = false;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.Khaki;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl1.CaptionImageOptions.Image")));
            this.groupControl1.CaptionImageOptions.ImageUri.Uri = "https://partner.spreadshirt.com/images/shop_illu.png";
            this.groupControl1.Controls.Add(this.xtraScrollableShop);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(757, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(327, 566);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Sell in all Shops";
            // 
            // xtraScrollableShop
            // 
            this.xtraScrollableShop.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraScrollableShop.Appearance.Options.UseBackColor = true;
            this.xtraScrollableShop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableShop.Location = new System.Drawing.Point(2, 39);
            this.xtraScrollableShop.Name = "xtraScrollableShop";
            this.xtraScrollableShop.Size = new System.Drawing.Size(323, 525);
            this.xtraScrollableShop.TabIndex = 0;
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.lsBoxLog);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl5.Location = new System.Drawing.Point(0, 376);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(757, 190);
            this.groupControl5.TabIndex = 0;
            this.groupControl5.Text = "Progress";
            // 
            // lsBoxLog
            // 
            this.lsBoxLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.lsBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsBoxLog.HorizontalScrollbar = true;
            this.lsBoxLog.ItemHeight = 22;
            this.lsBoxLog.Location = new System.Drawing.Point(2, 22);
            this.lsBoxLog.Name = "lsBoxLog";
            this.lsBoxLog.ShowFocusRect = false;
            this.lsBoxLog.Size = new System.Drawing.Size(753, 166);
            this.lsBoxLog.TabIndex = 0;
            // 
            // groupControlFile
            // 
            this.groupControlFile.Controls.Add(this.ckUsingFileUpload);
            this.groupControlFile.Controls.Add(this.btnClear);
            this.groupControlFile.Controls.Add(this.btnWriteLog);
            this.groupControlFile.Controls.Add(this.btnOpenFileExcel);
            this.groupControlFile.Controls.Add(this.txtPath);
            this.groupControlFile.Location = new System.Drawing.Point(2, 284);
            this.groupControlFile.Name = "groupControlFile";
            this.groupControlFile.Size = new System.Drawing.Size(560, 87);
            this.groupControlFile.TabIndex = 0;
            this.groupControlFile.Text = "Open File Excel (*.xlsx)";
            // 
            // ckUsingFileUpload
            // 
            this.ckUsingFileUpload.Location = new System.Drawing.Point(6, 55);
            this.ckUsingFileUpload.Name = "ckUsingFileUpload";
            this.ckUsingFileUpload.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ckUsingFileUpload.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.ckUsingFileUpload.Properties.Appearance.ForeColor = System.Drawing.Color.Olive;
            this.ckUsingFileUpload.Properties.Appearance.Options.UseBackColor = true;
            this.ckUsingFileUpload.Properties.Appearance.Options.UseFont = true;
            this.ckUsingFileUpload.Properties.Appearance.Options.UseForeColor = true;
            this.ckUsingFileUpload.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.ckUsingFileUpload.Properties.Caption = "Using upload from File";
            this.ckUsingFileUpload.Size = new System.Drawing.Size(282, 26);
            this.ckUsingFileUpload.TabIndex = 7;
            this.ckUsingFileUpload.CheckedChanged += new System.EventHandler(this.ckUsingFileUpload_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(470, 58);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 23);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear log";
            this.btnClear.ToolTip = "Clear Log";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnWriteLog
            // 
            this.btnWriteLog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnWriteLog.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnWriteLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWriteLog.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnWriteLog.ImageOptions.Image")));
            this.btnWriteLog.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnWriteLog.Location = new System.Drawing.Point(383, 58);
            this.btnWriteLog.Name = "btnWriteLog";
            this.btnWriteLog.Size = new System.Drawing.Size(84, 23);
            this.btnWriteLog.TabIndex = 8;
            this.btnWriteLog.Text = "View log";
            this.btnWriteLog.ToolTip = "Open File Data";
            this.btnWriteLog.Click += new System.EventHandler(this.btnWriteLog_Click);
            // 
            // btnOpenFileExcel
            // 
            this.btnOpenFileExcel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOpenFileExcel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnOpenFileExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenFileExcel.Enabled = false;
            this.btnOpenFileExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileExcel.ImageOptions.Image")));
            this.btnOpenFileExcel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOpenFileExcel.Location = new System.Drawing.Point(295, 58);
            this.btnOpenFileExcel.Name = "btnOpenFileExcel";
            this.btnOpenFileExcel.Size = new System.Drawing.Size(84, 23);
            this.btnOpenFileExcel.TabIndex = 9;
            this.btnOpenFileExcel.Text = "Open";
            this.btnOpenFileExcel.ToolTip = "Open File Data";
            this.btnOpenFileExcel.Click += new System.EventHandler(this.btnOpenFileExcel_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(5, 26);
            this.txtPath.Name = "txtPath";
            this.txtPath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPath.Properties.Appearance.Options.UseFont = true;
            this.txtPath.Size = new System.Drawing.Size(549, 26);
            this.txtPath.TabIndex = 4;
            // 
            // panelControlAction
            // 
            this.panelControlAction.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControlAction.Appearance.Options.UseBackColor = true;
            this.panelControlAction.Controls.Add(this.btnStart);
            this.panelControlAction.Location = new System.Drawing.Point(568, 284);
            this.panelControlAction.Name = "panelControlAction";
            this.panelControlAction.Size = new System.Drawing.Size(183, 87);
            this.panelControlAction.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(165)))));
            this.btnStart.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.btnStart.Appearance.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.btnStart.Appearance.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.Appearance.Options.UseBackColor = true;
            this.btnStart.Appearance.Options.UseBorderColor = true;
            this.btnStart.Appearance.Options.UseFont = true;
            this.btnStart.Appearance.Options.UseForeColor = true;
            this.btnStart.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Location = new System.Drawing.Point(8, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(167, 76);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Upload";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(112, 43);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.memoDescription.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.memoDescription.Properties.Appearance.Options.UseFont = true;
            this.memoDescription.Properties.Appearance.Options.UseForeColor = true;
            this.memoDescription.Size = new System.Drawing.Size(382, 88);
            this.memoDescription.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(7, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Description:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 12);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.Appearance.Options.UseForeColor = true;
            this.txtName.Size = new System.Drawing.Size(382, 26);
            this.txtName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(7, 134);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 19);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Tag:";
            // 
            // memoTag
            // 
            this.memoTag.Location = new System.Drawing.Point(112, 137);
            this.memoTag.Name = "memoTag";
            this.memoTag.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.memoTag.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.memoTag.Properties.Appearance.Options.UseFont = true;
            this.memoTag.Properties.Appearance.Options.UseForeColor = true;
            this.memoTag.Size = new System.Drawing.Size(382, 88);
            this.memoTag.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtPrice);
            this.panelControl1.Location = new System.Drawing.Point(2, 231);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(492, 47);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(10, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(96, 19);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Enter Price:";
            // 
            // txtPrice
            // 
            this.txtPrice.EditValue = "00.00";
            this.txtPrice.Location = new System.Drawing.Point(110, 10);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.txtPrice.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPrice.Properties.Appearance.Options.UseBackColor = true;
            this.txtPrice.Properties.Appearance.Options.UseFont = true;
            this.txtPrice.Properties.Appearance.Options.UseForeColor = true;
            this.txtPrice.Size = new System.Drawing.Size(377, 30);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // lsBoxImage
            // 
            this.lsBoxImage.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lsBoxImage.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lsBoxImage.Appearance.Options.UseFont = true;
            this.lsBoxImage.Appearance.Options.UseForeColor = true;
            this.lsBoxImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.lsBoxImage.Location = new System.Drawing.Point(500, 40);
            this.lsBoxImage.Name = "lsBoxImage";
            this.lsBoxImage.Size = new System.Drawing.Size(251, 238);
            this.lsBoxImage.TabIndex = 0;
            // 
            // btnChooesImage
            // 
            this.btnChooesImage.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnChooesImage.Location = new System.Drawing.Point(681, 17);
            this.btnChooesImage.Name = "btnChooesImage";
            this.btnChooesImage.Size = new System.Drawing.Size(70, 20);
            this.btnChooesImage.TabIndex = 5;
            this.btnChooesImage.Text = "...";
            this.btnChooesImage.Click += new System.EventHandler(this.btnChooesImage_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(503, 17);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(98, 19);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Image Design";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "McSkin";
            // 
            // frmMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 566);
            this.Controls.Add(this.lsBoxImage);
            this.Controls.Add(this.btnChooesImage);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.memoTag);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.memoDescription);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panelControlAction);
            this.Controls.Add(this.groupControlFile);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1100, 605);
            this.MinimumSize = new System.Drawing.Size(1100, 605);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Upload Spread Shirts 1.0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lsBoxLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlFile)).EndInit();
            this.groupControlFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckUsingFileUpload.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAction)).EndInit();
            this.panelControlAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoTag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lsBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.ListBoxControl lsBoxLog;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableShop;
        private DevExpress.XtraEditors.GroupControl groupControlFile;
        private DevExpress.XtraEditors.CheckEdit ckUsingFileUpload;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnWriteLog;
        private DevExpress.XtraEditors.SimpleButton btnOpenFileExcel;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraEditors.PanelControl panelControlAction;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit memoTag;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtPrice;
        private DevExpress.XtraEditors.ListBoxControl lsBoxImage;
        private DevExpress.XtraEditors.SimpleButton btnChooesImage;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}

