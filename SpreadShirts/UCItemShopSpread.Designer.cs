namespace SpreadShirts
{
    partial class UCItemShopSpread
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblShopID = new DevExpress.XtraEditors.LabelControl();
            this.toggShopSelected = new DevExpress.XtraEditors.ToggleSwitch();
            this.lblShopName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggShopSelected.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl1.Location = new System.Drawing.Point(4, 32);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(14, 14, 14, 14);
            this.separatorControl1.Size = new System.Drawing.Size(450, 30);
            this.separatorControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 11);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(55, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Shop ID:";
            // 
            // lblShopID
            // 
            this.lblShopID.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblShopID.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.lblShopID.Appearance.Options.UseFont = true;
            this.lblShopID.Appearance.Options.UseForeColor = true;
            this.lblShopID.Location = new System.Drawing.Point(70, 11);
            this.lblShopID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblShopID.Name = "lblShopID";
            this.lblShopID.Size = new System.Drawing.Size(63, 16);
            this.lblShopID.TabIndex = 0;
            this.lblShopID.Text = "100002020";
            // 
            // toggShopSelected
            // 
            this.toggShopSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggShopSelected.Location = new System.Drawing.Point(382, 6);
            this.toggShopSelected.Name = "toggShopSelected";
            this.toggShopSelected.Properties.AutoWidth = true;
            this.toggShopSelected.Properties.OffText = "Off";
            this.toggShopSelected.Properties.OnText = "On";
            this.toggShopSelected.Properties.ShowText = false;
            this.toggShopSelected.Size = new System.Drawing.Size(70, 24);
            this.toggShopSelected.TabIndex = 0;
            this.toggShopSelected.Toggled += new System.EventHandler(this.toggShopSelected_Toggled);
            // 
            // lblShopName
            // 
            this.lblShopName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShopName.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblShopName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblShopName.Appearance.Options.UseFont = true;
            this.lblShopName.Appearance.Options.UseForeColor = true;
            this.lblShopName.Appearance.Options.UseTextOptions = true;
            this.lblShopName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblShopName.AutoEllipsis = true;
            this.lblShopName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblShopName.Location = new System.Drawing.Point(4, 65);
            this.lblShopName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(454, 30);
            this.lblShopName.TabIndex = 0;
            this.lblShopName.Text = "SHOP-VIP1";
            // 
            // UCItemShopSpread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.toggShopSelected);
            this.Controls.Add(this.lblShopID);
            this.Controls.Add(this.lblShopName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.separatorControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UCItemShopSpread";
            this.Size = new System.Drawing.Size(460, 125);
            this.Load += new System.EventHandler(this.UCItemShopSpread_Load);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggShopSelected.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblShopID;
        private DevExpress.XtraEditors.ToggleSwitch toggShopSelected;
        private DevExpress.XtraEditors.LabelControl lblShopName;
    }
}
