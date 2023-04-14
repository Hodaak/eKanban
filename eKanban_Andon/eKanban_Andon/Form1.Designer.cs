namespace eKanban_Andon
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
            this.harnessBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.reflectorBar = new System.Windows.Forms.ProgressBar();
            this.housingBar = new System.Windows.Forms.ProgressBar();
            this.lensBar = new System.Windows.Forms.ProgressBar();
            this.bulbBar = new System.Windows.Forms.ProgressBar();
            this.bezelBar = new System.Windows.Forms.ProgressBar();
            this.yieldName = new System.Windows.Forms.Label();
            this.yieldValue = new System.Windows.Forms.Label();
            this.harnessCurrentValue = new System.Windows.Forms.Label();
            this.reflectorCurrentValue = new System.Windows.Forms.Label();
            this.housingCurrentValue = new System.Windows.Forms.Label();
            this.lensCurrentValue = new System.Windows.Forms.Label();
            this.bulbCurrentValue = new System.Windows.Forms.Label();
            this.bazelCurrentValue = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelProductNum = new System.Windows.Forms.Label();
            this.labelProductsValue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.signal = new eKanban_Andon.CustomSignal();
            this.SuspendLayout();
            // 
            // harnessBar
            // 
            this.harnessBar.Location = new System.Drawing.Point(106, 88);
            this.harnessBar.Name = "harnessBar";
            this.harnessBar.Size = new System.Drawing.Size(309, 23);
            this.harnessBar.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(579, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Harness";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Reflector";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Housing";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Lens";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Bulb";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Bezel";
            // 
            // reflectorBar
            // 
            this.reflectorBar.Location = new System.Drawing.Point(106, 129);
            this.reflectorBar.Name = "reflectorBar";
            this.reflectorBar.Size = new System.Drawing.Size(309, 23);
            this.reflectorBar.TabIndex = 8;
            // 
            // housingBar
            // 
            this.housingBar.Location = new System.Drawing.Point(106, 167);
            this.housingBar.Name = "housingBar";
            this.housingBar.Size = new System.Drawing.Size(309, 23);
            this.housingBar.TabIndex = 9;
            // 
            // lensBar
            // 
            this.lensBar.Location = new System.Drawing.Point(106, 207);
            this.lensBar.Name = "lensBar";
            this.lensBar.Size = new System.Drawing.Size(309, 23);
            this.lensBar.TabIndex = 10;
            // 
            // bulbBar
            // 
            this.bulbBar.Location = new System.Drawing.Point(106, 246);
            this.bulbBar.Name = "bulbBar";
            this.bulbBar.Size = new System.Drawing.Size(309, 23);
            this.bulbBar.TabIndex = 11;
            // 
            // bezelBar
            // 
            this.bezelBar.Location = new System.Drawing.Point(106, 284);
            this.bezelBar.Name = "bezelBar";
            this.bezelBar.Size = new System.Drawing.Size(309, 23);
            this.bezelBar.TabIndex = 12;
            // 
            // yieldName
            // 
            this.yieldName.AutoSize = true;
            this.yieldName.Location = new System.Drawing.Point(564, 189);
            this.yieldName.Name = "yieldName";
            this.yieldName.Size = new System.Drawing.Size(38, 16);
            this.yieldName.TabIndex = 13;
            this.yieldName.Text = "Yield";
            // 
            // yieldValue
            // 
            this.yieldValue.AutoSize = true;
            this.yieldValue.Location = new System.Drawing.Point(636, 189);
            this.yieldValue.Name = "yieldValue";
            this.yieldValue.Size = new System.Drawing.Size(14, 16);
            this.yieldValue.TabIndex = 14;
            this.yieldValue.Text = "0";
            // 
            // harnessCurrentValue
            // 
            this.harnessCurrentValue.AutoSize = true;
            this.harnessCurrentValue.Location = new System.Drawing.Point(421, 95);
            this.harnessCurrentValue.Name = "harnessCurrentValue";
            this.harnessCurrentValue.Size = new System.Drawing.Size(21, 16);
            this.harnessCurrentValue.TabIndex = 15;
            this.harnessCurrentValue.Text = "55";
            // 
            // reflectorCurrentValue
            // 
            this.reflectorCurrentValue.AutoSize = true;
            this.reflectorCurrentValue.Location = new System.Drawing.Point(421, 136);
            this.reflectorCurrentValue.Name = "reflectorCurrentValue";
            this.reflectorCurrentValue.Size = new System.Drawing.Size(21, 16);
            this.reflectorCurrentValue.TabIndex = 16;
            this.reflectorCurrentValue.Text = "55";
            // 
            // housingCurrentValue
            // 
            this.housingCurrentValue.AutoSize = true;
            this.housingCurrentValue.Location = new System.Drawing.Point(421, 174);
            this.housingCurrentValue.Name = "housingCurrentValue";
            this.housingCurrentValue.Size = new System.Drawing.Size(21, 16);
            this.housingCurrentValue.TabIndex = 17;
            this.housingCurrentValue.Text = "55";
            // 
            // lensCurrentValue
            // 
            this.lensCurrentValue.AutoSize = true;
            this.lensCurrentValue.Location = new System.Drawing.Point(421, 214);
            this.lensCurrentValue.Name = "lensCurrentValue";
            this.lensCurrentValue.Size = new System.Drawing.Size(21, 16);
            this.lensCurrentValue.TabIndex = 18;
            this.lensCurrentValue.Text = "55";
            // 
            // bulbCurrentValue
            // 
            this.bulbCurrentValue.AutoSize = true;
            this.bulbCurrentValue.Location = new System.Drawing.Point(421, 253);
            this.bulbCurrentValue.Name = "bulbCurrentValue";
            this.bulbCurrentValue.Size = new System.Drawing.Size(21, 16);
            this.bulbCurrentValue.TabIndex = 19;
            this.bulbCurrentValue.Text = "55";
            // 
            // bazelCurrentValue
            // 
            this.bazelCurrentValue.AutoSize = true;
            this.bazelCurrentValue.Location = new System.Drawing.Point(421, 291);
            this.bazelCurrentValue.Name = "bazelCurrentValue";
            this.bazelCurrentValue.Size = new System.Drawing.Size(21, 16);
            this.bazelCurrentValue.TabIndex = 20;
            this.bazelCurrentValue.Text = "55";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelProductNum
            // 
            this.labelProductNum.AutoSize = true;
            this.labelProductNum.Location = new System.Drawing.Point(492, 218);
            this.labelProductNum.Name = "labelProductNum";
            this.labelProductNum.Size = new System.Drawing.Size(122, 16);
            this.labelProductNum.TabIndex = 23;
            this.labelProductNum.Text = "Products Produced";
            // 
            // labelProductsValue
            // 
            this.labelProductsValue.AutoSize = true;
            this.labelProductsValue.Location = new System.Drawing.Point(636, 218);
            this.labelProductsValue.Name = "labelProductsValue";
            this.labelProductsValue.Size = new System.Drawing.Size(14, 16);
            this.labelProductsValue.TabIndex = 24;
            this.labelProductsValue.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(592, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 18);
            this.label7.TabIndex = 25;
            this.label7.Text = "Replenish";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(33, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(380, 32);
            this.labelTitle.TabIndex = 26;
            this.labelTitle.Text = "Workstation Andon Display";
            // 
            // signal
            // 
            this.signal.CheckedColor = System.Drawing.Color.Crimson;
            this.signal.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signal.Location = new System.Drawing.Point(690, 1);
            this.signal.MinimumSize = new System.Drawing.Size(0, 21);
            this.signal.Name = "signal";
            this.signal.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.signal.Size = new System.Drawing.Size(40, 40);
            this.signal.TabIndex = 22;
            this.signal.TabStop = true;
            this.signal.UnCheckedColor = System.Drawing.Color.Gray;
            this.signal.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 342);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelProductsValue);
            this.Controls.Add(this.labelProductNum);
            this.Controls.Add(this.signal);
            this.Controls.Add(this.bazelCurrentValue);
            this.Controls.Add(this.bulbCurrentValue);
            this.Controls.Add(this.lensCurrentValue);
            this.Controls.Add(this.housingCurrentValue);
            this.Controls.Add(this.reflectorCurrentValue);
            this.Controls.Add(this.harnessCurrentValue);
            this.Controls.Add(this.yieldValue);
            this.Controls.Add(this.yieldName);
            this.Controls.Add(this.bezelBar);
            this.Controls.Add(this.bulbBar);
            this.Controls.Add(this.lensBar);
            this.Controls.Add(this.housingBar);
            this.Controls.Add(this.reflectorBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.harnessBar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar harnessBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar reflectorBar;
        private System.Windows.Forms.ProgressBar housingBar;
        private System.Windows.Forms.ProgressBar lensBar;
        private System.Windows.Forms.ProgressBar bulbBar;
        private System.Windows.Forms.ProgressBar bezelBar;
        private System.Windows.Forms.Label yieldName;
        private System.Windows.Forms.Label yieldValue;
        private System.Windows.Forms.Label harnessCurrentValue;
        private System.Windows.Forms.Label reflectorCurrentValue;
        private System.Windows.Forms.Label housingCurrentValue;
        private System.Windows.Forms.Label lensCurrentValue;
        private System.Windows.Forms.Label bulbCurrentValue;
        private System.Windows.Forms.Label bazelCurrentValue;
        private CustomSignal signal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelProductNum;
        private System.Windows.Forms.Label labelProductsValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelTitle;
    }
}

