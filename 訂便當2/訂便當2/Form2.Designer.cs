namespace 訂便當2
{
    partial class Form2
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
            ManiXButton.Office2010Publisher office2010Publisher1 = new ManiXButton.Office2010Publisher();
            ManiXButton.Office2010Red office2010Red1 = new ManiXButton.Office2010Red();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.管理員選單ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改學生資料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改廠商資料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改菜色資料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關閉並離開ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new ManiXButton.XButton();
            this.button4 = new ManiXButton.XButton();
            this.button2 = new ManiXButton.XButton();
            this.button1 = new ManiXButton.XButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(1, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "登入者";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::訂便當2.Properties.Resources.BG;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理員選單ToolStripMenuItem,
            this.關閉並離開ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(681, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 管理員選單ToolStripMenuItem
            // 
            this.管理員選單ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改學生資料ToolStripMenuItem,
            this.修改廠商資料ToolStripMenuItem,
            this.修改菜色資料ToolStripMenuItem});
            this.管理員選單ToolStripMenuItem.Name = "管理員選單ToolStripMenuItem";
            this.管理員選單ToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.管理員選單ToolStripMenuItem.Text = "資料維護";
            // 
            // 修改學生資料ToolStripMenuItem
            // 
            this.修改學生資料ToolStripMenuItem.Name = "修改學生資料ToolStripMenuItem";
            this.修改學生資料ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改學生資料ToolStripMenuItem.Text = "修改學生資料";
            this.修改學生資料ToolStripMenuItem.Click += new System.EventHandler(this.修改學生資料ToolStripMenuItem_Click);
            // 
            // 修改廠商資料ToolStripMenuItem
            // 
            this.修改廠商資料ToolStripMenuItem.Name = "修改廠商資料ToolStripMenuItem";
            this.修改廠商資料ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改廠商資料ToolStripMenuItem.Text = "修改廠商資料";
            this.修改廠商資料ToolStripMenuItem.Click += new System.EventHandler(this.修改廠商資料ToolStripMenuItem_Click);
            // 
            // 修改菜色資料ToolStripMenuItem
            // 
            this.修改菜色資料ToolStripMenuItem.Name = "修改菜色資料ToolStripMenuItem";
            this.修改菜色資料ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改菜色資料ToolStripMenuItem.Text = "修改菜色資料";
            this.修改菜色資料ToolStripMenuItem.Click += new System.EventHandler(this.修改菜色資料ToolStripMenuItem_Click);
            // 
            // 關閉並離開ToolStripMenuItem
            // 
            this.關閉並離開ToolStripMenuItem.Name = "關閉並離開ToolStripMenuItem";
            this.關閉並離開ToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.關閉並離開ToolStripMenuItem.Text = "關閉並離開";
            this.關閉並離開ToolStripMenuItem.Click += new System.EventHandler(this.關閉並離開ToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(3, 97);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(678, 114);
            this.listBox1.TabIndex = 2;
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(348, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "目前總計";
            // 
            // button3
            // 
            office2010Publisher1.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Publisher1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Publisher1.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Publisher1.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Publisher1.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Publisher1.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Publisher1.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(126)))));
            office2010Publisher1.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(173)))), ((int)(((byte)(167)))));
            office2010Publisher1.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(126)))));
            office2010Publisher1.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(173)))), ((int)(((byte)(167)))));
            office2010Publisher1.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Publisher1.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Publisher1.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Publisher1.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Publisher1.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Publisher1.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Publisher1.TextColor = System.Drawing.Color.White;
            this.button3.ColorTable = office2010Publisher1;
            this.button3.Location = new System.Drawing.Point(3, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 64);
            this.button3.TabIndex = 11;
            this.button3.Text = "選取這次要訂的店家";
            this.button3.Theme = ManiXButton.Theme.MSOffice2010_Publisher;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            office2010Red1.BorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(161)))));
            office2010Red1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(135)))), ((int)(((byte)(228)))));
            office2010Red1.ButtonMouseOverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Red1.ButtonMouseOverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Red1.ButtonMouseOverColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(137)))));
            office2010Red1.ButtonMouseOverColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(224)))));
            office2010Red1.ButtonNormalColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            office2010Red1.ButtonNormalColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(148)))), ((int)(((byte)(64)))));
            office2010Red1.ButtonNormalColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(77)))), ((int)(((byte)(45)))));
            office2010Red1.ButtonNormalColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(148)))), ((int)(((byte)(64)))));
            office2010Red1.ButtonSelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(199)))), ((int)(((byte)(87)))));
            office2010Red1.ButtonSelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(243)))), ((int)(((byte)(215)))));
            office2010Red1.ButtonSelectedColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            office2010Red1.ButtonSelectedColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(107)))));
            office2010Red1.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Red1.SelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            office2010Red1.TextColor = System.Drawing.Color.White;
            this.button4.ColorTable = office2010Red1;
            this.button4.Location = new System.Drawing.Point(524, 258);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(145, 40);
            this.button4.TabIndex = 10;
            this.button4.Text = "結束訂購交付櫃台";
            this.button4.Theme = ManiXButton.Theme.MSOffice2010_RED;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button2
            // 
            this.button2.ColorTable = office2010Red1;
            this.button2.Location = new System.Drawing.Point(170, 258);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 40);
            this.button2.TabIndex = 9;
            this.button2.Text = "將選取的資料刪除";
            this.button2.Theme = ManiXButton.Theme.MSOffice2010_RED;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.ColorTable = office2010Red1;
            this.button1.Location = new System.Drawing.Point(0, 258);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 40);
            this.button1.TabIndex = 8;
            this.button1.Text = "重整訂單明細";
            this.button1.Theme = ManiXButton.Theme.MSOffice2010_RED;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = global::訂便當2.Properties.Resources.BG1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(681, 331);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 管理員選單ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改學生資料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改廠商資料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改菜色資料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關閉並離開ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private ManiXButton.XButton button1;
        private ManiXButton.XButton button2;
        private ManiXButton.XButton button4;
        private ManiXButton.XButton button3;
    }
}