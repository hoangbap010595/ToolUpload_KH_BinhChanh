namespace TCProShirts.UControls
{
    partial class UCItemProduct
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
            this.productTitleName = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.productPrice = new DevExpress.XtraEditors.LabelControl();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // productTitleName
            // 
            this.productTitleName.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.productTitleName.Appearance.Options.UseFont = true;
            this.productTitleName.Location = new System.Drawing.Point(137, 10);
            this.productTitleName.Name = "productTitleName";
            this.productTitleName.Size = new System.Drawing.Size(108, 23);
            this.productTitleName.TabIndex = 0;
            this.productTitleName.Text = "labelControl1";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl1.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl1.Location = new System.Drawing.Point(112, 32);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(694, 18);
            this.separatorControl1.TabIndex = 1;
            // 
            // productPrice
            // 
            this.productPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.productPrice.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.productPrice.Appearance.ForeColor = System.Drawing.Color.Green;
            this.productPrice.Appearance.Options.UseFont = true;
            this.productPrice.Appearance.Options.UseForeColor = true;
            this.productPrice.Appearance.Options.UseTextOptions = true;
            this.productPrice.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.productPrice.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.productPrice.Location = new System.Drawing.Point(689, 8);
            this.productPrice.Name = "productPrice";
            this.productPrice.Size = new System.Drawing.Size(117, 20);
            this.productPrice.TabIndex = 2;
            this.productPrice.Text = "$10.00";
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxImage.InitialImage = null;
            this.pictureBoxImage.Location = new System.Drawing.Point(13, 3);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(93, 118);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 3;
            this.pictureBoxImage.TabStop = false;
            // 
            // UCItemProduct
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.productPrice);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.productTitleName);
            this.Name = "UCItemProduct";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(819, 126);
            this.Load += new System.EventHandler(this.UCItemProduct_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UCItemProduct_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl productTitleName;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl productPrice;
        private System.Windows.Forms.PictureBox pictureBoxImage;
    }
}
