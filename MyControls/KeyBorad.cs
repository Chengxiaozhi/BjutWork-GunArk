using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Media;
namespace MyControls
{
    public partial class KeyBorad : UserControl
    {
        SoundPlayer sp = new SoundPlayer(Properties.Resources.click);
        TextBox t = new TextBox();
        string temp = "";

        public KeyBorad()
        {
            InitializeComponent();
        }

        public KeyBorad(TextBox text)
        {
            InitializeComponent();
            this.t = text;
        }

        private void KeyBorad_Load(object sender, EventArgs e)
        {
            num_0.Click += new EventHandler(NumClick);
            num_1.Click += new EventHandler(NumClick);
            num_2.Click += new EventHandler(NumClick);
            num_3.Click += new EventHandler(NumClick);
            num_4.Click += new EventHandler(NumClick);
            num_5.Click += new EventHandler(NumClick);
            num_6.Click += new EventHandler(NumClick);
            num_7.Click += new EventHandler(NumClick);
            num_8.Click += new EventHandler(NumClick);
            num_9.Click += new EventHandler(NumClick);
        }
        /// <summary>
        /// 数字键点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumClick(object sender, EventArgs e)
        {
            sp.Play();
            PictureBox p = (PictureBox)sender;
            temp = p.Name.Substring(4, 1);
        
            t.Text += temp;
            t.SelectionStart = t.Text.Length;
        
        }

        private void num_1_MouseDown(object sender, MouseEventArgs e)
        {
            num_1.Image = imageList1.Images[0];
        }

        private void num_1_MouseUp(object sender, MouseEventArgs e)
        {
            num_1.Image = Properties.Resources.n_1;
        }

        private void num_2_MouseDown(object sender, MouseEventArgs e)
        {
            num_2.Image = imageList1.Images[1];
        }

        private void num_2_MouseUp(object sender, MouseEventArgs e)
        {
            num_2.Image = Properties.Resources.n_2;
        }

        private void num_3_MouseDown(object sender, MouseEventArgs e)
        {
            num_3.Image = imageList1.Images[2];
        }

        private void num_3_MouseUp(object sender, MouseEventArgs e)
        {
            num_3.Image = Properties.Resources.n_3;
        }

        private void num_4_MouseDown(object sender, MouseEventArgs e)
        {
            num_4.Image = imageList1.Images[3];
        }

        private void num_4_MouseUp(object sender, MouseEventArgs e)
        {
            num_4.Image = Properties.Resources.n_4;
        }

        private void num_5_MouseDown(object sender, MouseEventArgs e)
        {
            num_5.Image = imageList1.Images[4];
        }

        private void num_5_MouseUp(object sender, MouseEventArgs e)
        {
            num_5.Image = Properties.Resources.n_5;
        }

        private void num_6_MouseDown(object sender, MouseEventArgs e)
        {
            num_6.Image = imageList1.Images[5];
        }

        private void num_6_MouseUp(object sender, MouseEventArgs e)
        {
            num_6.Image = Properties.Resources.n_6;
        }

        private void num_7_MouseDown(object sender, MouseEventArgs e)
        {
            num_7.Image = imageList1.Images[6];
        }

        private void num_7_MouseUp(object sender, MouseEventArgs e)
        {
            num_7.Image = Properties.Resources.n_7;
        }

        private void num_8_MouseDown(object sender, MouseEventArgs e)
        {
            num_8.Image = imageList1.Images[7];
        }

        private void num_8_MouseUp(object sender, MouseEventArgs e)
        {
            num_8.Image = Properties.Resources.n_8;
        }

        private void num_9_MouseDown(object sender, MouseEventArgs e)
        {
            num_9.Image = imageList1.Images[8];
        }

        private void num_9_MouseUp(object sender, MouseEventArgs e)
        {
            num_9.Image = Properties.Resources.n_9;
        }

        private void num_0_MouseDown(object sender, MouseEventArgs e)
        {
            num_0.Image = imageList1.Images[9];
        }

        private void num_0_MouseUp(object sender, MouseEventArgs e)
        {
            num_0.Image = Properties.Resources.n_0;
        }

        private void back_MouseDown(object sender, MouseEventArgs e)
        {
            back.Image = imageList1.Images[10];
        }

        private void back_MouseUp(object sender, MouseEventArgs e)
        {
            back.Image = Properties.Resources.backspace;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void back_Click(object sender, EventArgs e)
        {
            sp.Play();
            if (t.Text.Length >= 1)
            {
                t.Text = t.Text.Substring(0, t.Text.Length - 1);
                t.SelectionStart = t.Text.Length;
            }
        }

        private void close_MouseDown(object sender, MouseEventArgs e)
        {
            close.Image = imageList1.Images[11];
        }

        private void close_MouseUp(object sender, MouseEventArgs e)
        {
            close.Image = Properties.Resources.esc;
        }

        private void point_MouseDown(object sender, MouseEventArgs e)
        {
            point.Image = imageList1.Images[12];
        }

        private void point_MouseUp(object sender, MouseEventArgs e)
        {
            point.Image = Properties.Resources.point;
        }

        private void maohao_MouseDown(object sender, MouseEventArgs e)
        {
            maohao.Image = imageList1.Images[13];
        }

        private void maohao_MouseUp(object sender, MouseEventArgs e)
        {
            maohao.Image = Properties.Resources.maohao;
        }

        private void point_Click(object sender, EventArgs e)
        {
            sp.Play();
            PictureBox p = (PictureBox)sender;
            temp = ".";

            t.Text += temp;
            t.SelectionStart = t.Text.Length;
        }

        private void maohao_Click(object sender, EventArgs e)
        {
            sp.Play();
            PictureBox p = (PictureBox)sender;
            temp = ":";

            t.Text += temp;
            t.SelectionStart = t.Text.Length;
        }

    }
}
