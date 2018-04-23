namespace Informatyka
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.addForces = new System.Windows.Forms.Button();
            this.wsuw = new System.Windows.Forms.RadioButton();
            this.podpar = new System.Windows.Forms.RadioButton();
            this.calcBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.reactions_list = new System.Windows.Forms.Label();
            this.removeBtn = new System.Windows.Forms.Button();
            this.vertical = new System.Windows.Forms.RadioButton();
            this.horizontal = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // addForces
            // 
            this.addForces.Location = new System.Drawing.Point(228, 74);
            this.addForces.Name = "addForces";
            this.addForces.Size = new System.Drawing.Size(75, 39);
            this.addForces.TabIndex = 0;
            this.addForces.Text = "Add Forces";
            this.addForces.UseVisualStyleBackColor = true;
            this.addForces.Click += new System.EventHandler(this.button1_Click);
            // 
            // wsuw
            // 
            this.wsuw.AutoSize = true;
            this.wsuw.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.wsuw.Location = new System.Drawing.Point(6, 39);
            this.wsuw.Name = "wsuw";
            this.wsuw.Size = new System.Drawing.Size(150, 17);
            this.wsuw.TabIndex = 3;
            this.wsuw.TabStop = true;
            this.wsuw.Text = "Wspornikowa utwierdzona";
            this.wsuw.UseVisualStyleBackColor = true;
            this.wsuw.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // podpar
            // 
            this.podpar.AutoSize = true;
            this.podpar.Location = new System.Drawing.Point(6, 16);
            this.podpar.Name = "podpar";
            this.podpar.Size = new System.Drawing.Size(68, 17);
            this.podpar.TabIndex = 4;
            this.podpar.TabStop = true;
            this.podpar.Text = "Podparta";
            this.podpar.UseVisualStyleBackColor = true;
            this.podpar.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // calcBtn
            // 
            this.calcBtn.Location = new System.Drawing.Point(228, 148);
            this.calcBtn.Name = "calcBtn";
            this.calcBtn.Size = new System.Drawing.Size(75, 23);
            this.calcBtn.TabIndex = 5;
            this.calcBtn.Text = "Oblicz";
            this.calcBtn.UseVisualStyleBackColor = true;
            this.calcBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 36);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(56, 110);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(73, 36);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(65, 110);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(228, 48);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(75, 20);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "100";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Beam length [m]";
            this.label3.Click += new System.EventHandler(this.button3_Click);
            // 
            // reactions_list
            // 
            this.reactions_list.AutoSize = true;
            this.reactions_list.Location = new System.Drawing.Point(228, 190);
            this.reactions_list.MaximumSize = new System.Drawing.Size(244, 0);
            this.reactions_list.Name = "reactions_list";
            this.reactions_list.Size = new System.Drawing.Size(73, 13);
            this.reactions_list.TabIndex = 10;
            this.reactions_list.Text = "Reactions list:";
            this.reactions_list.MouseEnter += new System.EventHandler(this.label4_MouseEnter);
            this.reactions_list.MouseLeave += new System.EventHandler(this.label4_MouseLeave);
            this.reactions_list.MouseHover += new System.EventHandler(this.label4_MouseHover);
            // 
            // removeBtn
            // 
            this.removeBtn.Location = new System.Drawing.Point(228, 119);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(75, 23);
            this.removeBtn.TabIndex = 11;
            this.removeBtn.Text = "Remove Forces";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // vertical
            // 
            this.vertical.AutoSize = true;
            this.vertical.Location = new System.Drawing.Point(6, 37);
            this.vertical.Name = "vertical";
            this.vertical.Size = new System.Drawing.Size(60, 17);
            this.vertical.TabIndex = 12;
            this.vertical.TabStop = true;
            this.vertical.Text = "Vertical";
            this.vertical.UseVisualStyleBackColor = true;
            this.vertical.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // horizontal
            // 
            this.horizontal.AutoSize = true;
            this.horizontal.Location = new System.Drawing.Point(6, 14);
            this.horizontal.Name = "horizontal";
            this.horizontal.Size = new System.Drawing.Size(72, 17);
            this.horizontal.TabIndex = 13;
            this.horizontal.TabStop = true;
            this.horizontal.Text = "Horizontal";
            this.horizontal.UseVisualStyleBackColor = true;
            this.horizontal.CheckedChanged += new System.EventHandler(this.poziome_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wsuw);
            this.groupBox1.Controls.Add(this.podpar);
            this.groupBox1.Location = new System.Drawing.Point(319, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 63);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rodzaj mocowania belki";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.vertical);
            this.groupBox2.Controls.Add(this.horizontal);
            this.groupBox2.Location = new System.Drawing.Point(319, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 70);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rodzaj dzialajacych sil";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(68, 36);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(65, 110);
            this.textBox4.TabIndex = 19;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(6, 36);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(56, 110);
            this.textBox5.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 16);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "x [m]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Value [Nm]";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(19, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 162);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Moments";
            this.groupBox3.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Location = new System.Drawing.Point(19, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(154, 162);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Forces";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Value [N]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 16);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "x [m]";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(12, 375);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(161, 116);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 22;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseLeave += new System.EventHandler(this.pictureBox3_MouseLeave);
            this.pictureBox3.MouseHover += new System.EventHandler(this.pictureBox3_MouseHover);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 72);
            this.label1.TabIndex = 23;
            this.label1.Text = "Patrycja Ziomek\r\nMichał Wilk\r\nTomasz Taranek\r\nKrzysztof Szewczyk\r\n©2017";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1012, 611);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.reactions_list);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.calcBtn);
            this.Controls.Add(this.addForces);
            this.Name = "Form1";
            this.Text = "Belka";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addForces;
        private System.Windows.Forms.RadioButton wsuw;
        private System.Windows.Forms.RadioButton podpar;
        private System.Windows.Forms.Button calcBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label reactions_list;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.RadioButton vertical;
        private System.Windows.Forms.RadioButton horizontal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
    }
}

