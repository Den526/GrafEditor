﻿namespace GrafEditor
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbMainGrafWin = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCoordPB1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.gbColor = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbShapes = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGrafWin)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.gbColor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbShapes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMainGrafWin
            // 
            this.pbMainGrafWin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMainGrafWin.BackColor = System.Drawing.Color.White;
            this.pbMainGrafWin.Location = new System.Drawing.Point(172, 12);
            this.pbMainGrafWin.Name = "pbMainGrafWin";
            this.pbMainGrafWin.Size = new System.Drawing.Size(1148, 459);
            this.pbMainGrafWin.TabIndex = 0;
            this.pbMainGrafWin.TabStop = false;
            this.pbMainGrafWin.Click += new System.EventHandler(this.pbMainGrafWin_Click);
            this.pbMainGrafWin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMainGrafWin_MouseClick);
            this.pbMainGrafWin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMainGrafWin_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCoordPB1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 474);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1320, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCoordPB1
            // 
            this.lblCoordPB1.Name = "lblCoordPB1";
            this.lblCoordPB1.Size = new System.Drawing.Size(28, 17);
            this.lblCoordPB1.Text = "0 : 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "   ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.MinimumSize = new System.Drawing.Size(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "  ";
            this.label1.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // gbColor
            // 
            this.gbColor.Controls.Add(this.label11);
            this.gbColor.Controls.Add(this.label10);
            this.gbColor.Controls.Add(this.label9);
            this.gbColor.Controls.Add(this.label8);
            this.gbColor.Controls.Add(this.label7);
            this.gbColor.Controls.Add(this.label6);
            this.gbColor.Controls.Add(this.label5);
            this.gbColor.Controls.Add(this.label4);
            this.gbColor.Controls.Add(this.label3);
            this.gbColor.Controls.Add(this.label2);
            this.gbColor.Controls.Add(this.label1);
            this.gbColor.Location = new System.Drawing.Point(8, 141);
            this.gbColor.Name = "gbColor";
            this.gbColor.Size = new System.Drawing.Size(158, 68);
            this.gbColor.TabIndex = 3;
            this.gbColor.TabStop = false;
            this.gbColor.Text = "Выбор цвета";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(44, 40);
            this.label11.MinimumSize = new System.Drawing.Size(13, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "  ";
            this.label11.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(25, 40);
            this.label10.MinimumSize = new System.Drawing.Size(13, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "  ";
            this.label10.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(6, 40);
            this.label9.MinimumSize = new System.Drawing.Size(13, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "  ";
            this.label9.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Olive;
            this.label8.Location = new System.Drawing.Point(139, 16);
            this.label8.MinimumSize = new System.Drawing.Size(13, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "  ";
            this.label8.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Fuchsia;
            this.label7.Location = new System.Drawing.Point(120, 16);
            this.label7.MinimumSize = new System.Drawing.Size(13, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "  ";
            this.label7.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(101, 16);
            this.label6.MinimumSize = new System.Drawing.Size(13, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "  ";
            this.label6.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Aqua;
            this.label5.Location = new System.Drawing.Point(82, 16);
            this.label5.MinimumSize = new System.Drawing.Size(13, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "  ";
            this.label5.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Lime;
            this.label4.Location = new System.Drawing.Point(63, 16);
            this.label4.MinimumSize = new System.Drawing.Size(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "  ";
            this.label4.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(44, 16);
            this.label3.MinimumSize = new System.Drawing.Size(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "  ";
            this.label3.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(25, 16);
            this.label2.MinimumSize = new System.Drawing.Size(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "  ";
            this.label2.Click += new System.EventHandler(this.SelectColor_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(4, 14);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(150, 238);
            this.listBox1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(8, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 256);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список объектов";
            // 
            // gbShapes
            // 
            this.gbShapes.Controls.Add(this.button1);
            this.gbShapes.Location = new System.Drawing.Point(8, 12);
            this.gbShapes.Name = "gbShapes";
            this.gbShapes.Size = new System.Drawing.Size(158, 123);
            this.gbShapes.TabIndex = 6;
            this.gbShapes.TabStop = false;
            this.gbShapes.Text = "Фигуры";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "L";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1320, 496);
            this.Controls.Add(this.gbShapes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbColor);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pbMainGrafWin);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGrafWin)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbColor.ResumeLayout(false);
            this.gbColor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbShapes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainGrafWin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCoordPB1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbShapes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
