using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model = Gunark.Model;
using Bll = Gunark.BLL;
using System.Diagnostics;

namespace MyUI
{
    public partial class inputBulletQty : Form
    {
        private int bulletQty = 0;
        private string mii = "";
        private Process myProcess;
        public int BulletQty
        {
            get { return bulletQty; }
            set { bulletQty = value; }
        }
        public inputBulletQty(string mii)
        {
            this.mii = mii;
            InitializeComponent();
        }

        private void inputBulletQty_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Focus();
            myProcess = Process.Start("osk.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bulletQty = int.Parse(this.maskedTextBox1.Text.Replace(" ", ""));
            int stockQty = (int)new Bll.magazine_info().GetModel(mii).STOCK_QTY;
            if (bulletQty > stockQty)
            {
                label1.Text = "取弹数量大于子弹库存量！";
                label1.ForeColor = Color.Red;
                maskedTextBox1.Text = "";
                maskedTextBox1.Focus();
                return;
            }
           
            this.Close();
        }

        private void inputBulletQty_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                myProcess.CloseMainWindow();
            }
            catch { }
        }

    }
}
