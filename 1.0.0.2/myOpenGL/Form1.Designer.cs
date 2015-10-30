namespace myOpenGL
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.hScrollBar13 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar12 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar11 = new System.Windows.Forms.HScrollBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton0 = new System.Windows.Forms.RadioButton();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.weaponGroupBox = new System.Windows.Forms.GroupBox();
            this.sourceOfLightPositionGroupBox = new System.Windows.Forms.GroupBox();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar8 = new System.Windows.Forms.HScrollBar();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.toolbarPanel = new System.Windows.Forms.Panel();
            this.weaponGroupBox.SuspendLayout();
            this.sourceOfLightPositionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolbarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 500);
            this.panel1.TabIndex = 6;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 15;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // hScrollBar13
            // 
            this.hScrollBar13.Location = new System.Drawing.Point(29, 108);
            this.hScrollBar13.Maximum = 200;
            this.hScrollBar13.Name = "hScrollBar13";
            this.hScrollBar13.Size = new System.Drawing.Size(119, 17);
            this.hScrollBar13.TabIndex = 34;
            this.hScrollBar13.Value = 100;
            this.hScrollBar13.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar12
            // 
            this.hScrollBar12.Location = new System.Drawing.Point(28, 76);
            this.hScrollBar12.Maximum = 200;
            this.hScrollBar12.Name = "hScrollBar12";
            this.hScrollBar12.Size = new System.Drawing.Size(119, 17);
            this.hScrollBar12.TabIndex = 33;
            this.hScrollBar12.Value = 100;
            this.hScrollBar12.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar11
            // 
            this.hScrollBar11.Location = new System.Drawing.Point(29, 39);
            this.hScrollBar11.Maximum = 200;
            this.hScrollBar11.Name = "hScrollBar11";
            this.hScrollBar11.Size = new System.Drawing.Size(119, 17);
            this.hScrollBar11.TabIndex = 32;
            this.hScrollBar11.Value = 100;
            this.hScrollBar11.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(84, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "z";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(84, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "y";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(85, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(12, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "x";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(20, 47);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 17);
            this.radioButton1.TabIndex = 101;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Mauser";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(20, 70);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 17);
            this.radioButton2.TabIndex = 102;
            this.radioButton2.Text = "MP7";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(20, 93);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(55, 17);
            this.radioButton3.TabIndex = 103;
            this.radioButton3.Text = "Sword";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton0
            // 
            this.radioButton0.AutoSize = true;
            this.radioButton0.Location = new System.Drawing.Point(20, 24);
            this.radioButton0.Name = "radioButton0";
            this.radioButton0.Size = new System.Drawing.Size(81, 17);
            this.radioButton0.TabIndex = 104;
            this.radioButton0.Text = "Bare Hands";
            this.radioButton0.UseVisualStyleBackColor = true;
            this.radioButton0.CheckedChanged += new System.EventHandler(this.radioButton0_CheckedChanged);
            // 
            // timer4
            // 
            this.timer4.Interval = 1;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // weaponGroupBox
            // 
            this.weaponGroupBox.Controls.Add(this.radioButton0);
            this.weaponGroupBox.Controls.Add(this.radioButton3);
            this.weaponGroupBox.Controls.Add(this.radioButton2);
            this.weaponGroupBox.Controls.Add(this.radioButton1);
            this.weaponGroupBox.Location = new System.Drawing.Point(4, 0);
            this.weaponGroupBox.Name = "weaponGroupBox";
            this.weaponGroupBox.Size = new System.Drawing.Size(109, 127);
            this.weaponGroupBox.TabIndex = 105;
            this.weaponGroupBox.TabStop = false;
            this.weaponGroupBox.Text = "Weapon";
            // 
            // sourceOfLightPositionGroupBox
            // 
            this.sourceOfLightPositionGroupBox.Controls.Add(this.label13);
            this.sourceOfLightPositionGroupBox.Controls.Add(this.label12);
            this.sourceOfLightPositionGroupBox.Controls.Add(this.label14);
            this.sourceOfLightPositionGroupBox.Controls.Add(this.hScrollBar13);
            this.sourceOfLightPositionGroupBox.Controls.Add(this.hScrollBar12);
            this.sourceOfLightPositionGroupBox.Controls.Add(this.hScrollBar11);
            this.sourceOfLightPositionGroupBox.Location = new System.Drawing.Point(4, 133);
            this.sourceOfLightPositionGroupBox.Name = "sourceOfLightPositionGroupBox";
            this.sourceOfLightPositionGroupBox.Size = new System.Drawing.Size(173, 139);
            this.sourceOfLightPositionGroupBox.TabIndex = 106;
            this.sourceOfLightPositionGroupBox.TabStop = false;
            this.sourceOfLightPositionGroupBox.Text = "Source Of Light Position ";
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Enabled = false;
            this.hScrollBar2.Location = new System.Drawing.Point(939, 462);
            this.hScrollBar2.Maximum = 200;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(119, 17);
            this.hScrollBar2.TabIndex = 8;
            this.hScrollBar2.Value = 130;
            this.hScrollBar2.Visible = false;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.Enabled = false;
            this.hScrollBar3.Location = new System.Drawing.Point(939, 483);
            this.hScrollBar3.Maximum = 200;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(119, 17);
            this.hScrollBar3.TabIndex = 9;
            this.hScrollBar3.Value = 190;
            this.hScrollBar3.Visible = false;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // hScrollBar8
            // 
            this.hScrollBar8.Enabled = false;
            this.hScrollBar8.Location = new System.Drawing.Point(940, 435);
            this.hScrollBar8.Maximum = 200;
            this.hScrollBar8.Name = "hScrollBar8";
            this.hScrollBar8.Size = new System.Drawing.Size(119, 17);
            this.hScrollBar8.TabIndex = 12;
            this.hScrollBar8.Value = 110;
            this.hScrollBar8.Visible = false;
            this.hScrollBar8.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(1370, 12);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown1.TabIndex = 0;
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.instructionsLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.instructionsLabel.Location = new System.Drawing.Point(3, 275);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(160, 227);
            this.instructionsLabel.TabIndex = 107;
            this.instructionsLabel.Text = resources.GetString("instructionsLabel.Text");
            // 
            // toolbarPanel
            // 
            this.toolbarPanel.Controls.Add(this.weaponGroupBox);
            this.toolbarPanel.Controls.Add(this.instructionsLabel);
            this.toolbarPanel.Controls.Add(this.sourceOfLightPositionGroupBox);
            this.toolbarPanel.Location = new System.Drawing.Point(745, 5);
            this.toolbarPanel.Name = "toolbarPanel";
            this.toolbarPanel.Size = new System.Drawing.Size(189, 513);
            this.toolbarPanel.TabIndex = 108;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 523);
            this.Controls.Add(this.toolbarPanel);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.hScrollBar8);
            this.Controls.Add(this.hScrollBar3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.hScrollBar2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.weaponGroupBox.ResumeLayout(false);
            this.weaponGroupBox.PerformLayout();
            this.sourceOfLightPositionGroupBox.ResumeLayout(false);
            this.sourceOfLightPositionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolbarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.HScrollBar hScrollBar13;
        private System.Windows.Forms.HScrollBar hScrollBar12;
        private System.Windows.Forms.HScrollBar hScrollBar11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton0;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.GroupBox weaponGroupBox;
        private System.Windows.Forms.GroupBox sourceOfLightPositionGroupBox;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.HScrollBar hScrollBar8;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label instructionsLabel;
        private System.Windows.Forms.Panel toolbarPanel;
    }
}

