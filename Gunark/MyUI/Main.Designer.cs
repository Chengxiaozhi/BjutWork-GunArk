namespace MyUI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.SYS_TIME = new System.Windows.Forms.Timer(this.components);
            this.WebService_Status = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.service_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.store = new System.Windows.Forms.ToolStripStatusLabel();
            this.degree = new System.Windows.Forms.ToolStripStatusLabel();
            this.power = new System.Windows.Forms.ToolStripStatusLabel();
            this.alert = new System.Windows.Forms.ToolStripStatusLabel();
            this.time = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.return_gun = new System.Windows.Forms.Button();
            this.another_task = new System.Windows.Forms.Button();
            this.get_gun = new System.Windows.Forms.Button();
            this.gunark_management = new System.Windows.Forms.Button();
            this.quick_get_gun = new System.Windows.Forms.Button();
            this.emergency = new System.Windows.Forms.Button();
            this.gunark_status = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // SYS_TIME
            // 
            this.SYS_TIME.Tick += new System.EventHandler(this.SYS_TIME_Tick);
            // 
            // WebService_Status
            // 
            this.WebService_Status.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Web_Service_Status_DoWork);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackgroundImage = global::MyUI.Properties.Resources.bg_2;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.service_status,
            this.store,
            this.degree,
            this.power,
            this.alert,
            this.time});
            this.statusStrip1.Location = new System.Drawing.Point(0, 547);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 37);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // service_status
            // 
            this.service_status.Image = global::MyUI.Properties.Resources.unconnected;
            this.service_status.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.service_status.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.service_status.Name = "service_status";
            this.service_status.Size = new System.Drawing.Size(184, 32);
            this.service_status.Spring = true;
            this.service_status.Text = "【未联网】";
            // 
            // store
            // 
            this.store.Image = global::MyUI.Properties.Resources.store;
            this.store.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.store.Name = "store";
            this.store.Size = new System.Drawing.Size(162, 32);
            this.store.Text = "【应存：50 现存：10】";
            // 
            // degree
            // 
            this.degree.Image = global::MyUI.Properties.Resources.degred;
            this.degree.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.degree.Name = "degree";
            this.degree.Size = new System.Drawing.Size(80, 32);
            this.degree.Text = "【20°】";
            // 
            // power
            // 
            this.power.Image = global::MyUI.Properties.Resources.power;
            this.power.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(111, 32);
            this.power.Text = "【外部电源】";
            // 
            // alert
            // 
            this.alert.Image = global::MyUI.Properties.Resources.alert;
            this.alert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.alert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.alert.Name = "alert";
            this.alert.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.alert.Size = new System.Drawing.Size(184, 32);
            this.alert.Spring = true;
            this.alert.Text = "【安全】";
            // 
            // time
            // 
            this.time.Image = global::MyUI.Properties.Resources.sys_time;
            this.time.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(63, 32);
            this.time.Text = "time";
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::MyUI.Properties.Resources.main_bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.return_gun);
            this.panel1.Controls.Add(this.another_task);
            this.panel1.Controls.Add(this.get_gun);
            this.panel1.Controls.Add(this.gunark_management);
            this.panel1.Controls.Add(this.quick_get_gun);
            this.panel1.Controls.Add(this.emergency);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 584);
            this.panel1.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 55);
            this.button1.TabIndex = 21;
            this.button1.Text = "测试同步";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // return_gun
            // 
            this.return_gun.Location = new System.Drawing.Point(619, 128);
            this.return_gun.Name = "return_gun";
            this.return_gun.Size = new System.Drawing.Size(120, 50);
            this.return_gun.TabIndex = 16;
            this.return_gun.Text = "归还枪弹";
            this.return_gun.UseVisualStyleBackColor = true;
            this.return_gun.Click += new System.EventHandler(this.return_gun_Click);
            // 
            // another_task
            // 
            this.another_task.Enabled = false;
            this.another_task.Location = new System.Drawing.Point(619, 242);
            this.another_task.Name = "another_task";
            this.another_task.Size = new System.Drawing.Size(120, 50);
            this.another_task.TabIndex = 20;
            this.another_task.Text = "其他任务";
            this.another_task.UseVisualStyleBackColor = true;
            this.another_task.Click += new System.EventHandler(this.another_task_Click);
            // 
            // get_gun
            // 
            this.get_gun.Enabled = false;
            this.get_gun.Location = new System.Drawing.Point(430, 128);
            this.get_gun.Name = "get_gun";
            this.get_gun.Size = new System.Drawing.Size(120, 50);
            this.get_gun.TabIndex = 15;
            this.get_gun.Text = "领取枪弹";
            this.get_gun.UseVisualStyleBackColor = true;
            this.get_gun.Click += new System.EventHandler(this.get_gun_Click);
            // 
            // gunark_management
            // 
            this.gunark_management.Location = new System.Drawing.Point(430, 242);
            this.gunark_management.Name = "gunark_management";
            this.gunark_management.Size = new System.Drawing.Size(120, 50);
            this.gunark_management.TabIndex = 19;
            this.gunark_management.Text = "枪弹库管理";
            this.gunark_management.UseVisualStyleBackColor = true;
            this.gunark_management.Click += new System.EventHandler(this.gunark_management_Click);
            // 
            // quick_get_gun
            // 
            this.quick_get_gun.Location = new System.Drawing.Point(430, 361);
            this.quick_get_gun.Name = "quick_get_gun";
            this.quick_get_gun.Size = new System.Drawing.Size(120, 50);
            this.quick_get_gun.TabIndex = 17;
            this.quick_get_gun.Text = "快速申领";
            this.quick_get_gun.UseVisualStyleBackColor = true;
            this.quick_get_gun.Click += new System.EventHandler(this.quick_get_gun_Click);
            // 
            // emergency
            // 
            this.emergency.Location = new System.Drawing.Point(619, 361);
            this.emergency.Name = "emergency";
            this.emergency.Size = new System.Drawing.Size(120, 50);
            this.emergency.TabIndex = 18;
            this.emergency.Text = "紧急取枪";
            this.emergency.UseVisualStyleBackColor = true;
            this.emergency.Click += new System.EventHandler(this.emergency_Click);
            // 
            // gunark_status
            // 
            this.gunark_status.Interval = 5000;
            this.gunark_status.Tick += new System.EventHandler(this.gunark_status_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 584);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能枪弹柜管理系统";
            this.Load += new System.EventHandler(this.Main_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.Timer SYS_TIME;
        private System.ComponentModel.BackgroundWorker WebService_Status;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel time;
        private System.Windows.Forms.ToolStripStatusLabel service_status;
        private System.Windows.Forms.ToolStripStatusLabel store;
        private System.Windows.Forms.ToolStripStatusLabel degree;
        private System.Windows.Forms.ToolStripStatusLabel power;
        private System.Windows.Forms.ToolStripStatusLabel alert;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button return_gun;
        private System.Windows.Forms.Button another_task;
        private System.Windows.Forms.Button get_gun;
        private System.Windows.Forms.Button gunark_management;
        private System.Windows.Forms.Button quick_get_gun;
        private System.Windows.Forms.Button emergency;
        private System.Windows.Forms.Timer gunark_status;
        private System.Windows.Forms.Button button1;
    }
}