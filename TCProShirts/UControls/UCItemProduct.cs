using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCProShirts.Models;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Net;

namespace TCProShirts.UControls
{
    public partial class UCItemProduct : XtraUserControl
    {
        private Rectangle r;

        public Product Product;
        public UCItemProduct(Product product)
        {
            InitializeComponent();
            Product = product;
        }
        public UCItemProduct()
        {
            InitializeComponent();
        }

        private void UCItemProduct_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Product.Colors.Count; i++)
            {
                if (i < 11)
                {
                    SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#" + Product.Colors[i].Hex));
                    r = new Rectangle(140 + (i * 45) + 10, 50, 30, 30);
                    e.Graphics.DrawEllipse(Pens.WhiteSmoke, r);
                    e.Graphics.FillEllipse(brush, r);
                    brush.Dispose();
                }
            }
            for (int i = 11; i < Product.Colors.Count; i++)
            {
                SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#" + Product.Colors[i].Hex));
                r = new Rectangle(140 + ((i - 11) * 45) + 10, 90, 30, 30);
                e.Graphics.DrawEllipse(Pens.WhiteSmoke, r);
                e.Graphics.FillEllipse(brush, r);
                brush.Dispose();
            }
            e.Graphics.Dispose();
        }
        private void UCItemProduct_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    productTitleName.Text = Product.Name;
                    productPrice.Text = "$" + Product.Price;
                });
                if (Product.Colors.Count > 0 && Product.Colors[0].Image == "")
                    return;
                try
                {
                    pictureBoxImage.ImageLocation = Product.Colors[0].Image;
                }
                catch { }
            }));

            t.Start();
        }
    }
}
