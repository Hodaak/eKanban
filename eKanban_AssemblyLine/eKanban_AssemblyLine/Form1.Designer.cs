namespace eKanban_AssemblyLine
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.totalYieldForAllWorkstations = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.orderAmount = new System.Windows.Forms.Label();
            this.processAmount = new System.Windows.Forms.Label();
            this.numberProduced = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numberOfRunningWorkstations = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order amount: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process amount:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number produced: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Yield for each workstations: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Yield for total workstations: ";
            // 
            // totalYieldForAllWorkstations
            // 
            this.totalYieldForAllWorkstations.AutoSize = true;
            this.totalYieldForAllWorkstations.Location = new System.Drawing.Point(215, 179);
            this.totalYieldForAllWorkstations.Name = "totalYieldForAllWorkstations";
            this.totalYieldForAllWorkstations.Size = new System.Drawing.Size(31, 15);
            this.totalYieldForAllWorkstations.TabIndex = 5;
            this.totalYieldForAllWorkstations.Text = "N/A";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.9899F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.0101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 250);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(6, 28);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // orderAmount
            // 
            this.orderAmount.AutoSize = true;
            this.orderAmount.Location = new System.Drawing.Point(132, 134);
            this.orderAmount.Name = "orderAmount";
            this.orderAmount.Size = new System.Drawing.Size(31, 15);
            this.orderAmount.TabIndex = 8;
            this.orderAmount.Text = "N/A";
            // 
            // processAmount
            // 
            this.processAmount.AutoSize = true;
            this.processAmount.Location = new System.Drawing.Point(541, 134);
            this.processAmount.Name = "processAmount";
            this.processAmount.Size = new System.Drawing.Size(31, 15);
            this.processAmount.TabIndex = 9;
            this.processAmount.Text = "N/A";
            // 
            // numberProduced
            // 
            this.numberProduced.AutoSize = true;
            this.numberProduced.Location = new System.Drawing.Point(552, 179);
            this.numberProduced.Name = "numberProduced";
            this.numberProduced.Size = new System.Drawing.Size(31, 15);
            this.numberProduced.TabIndex = 10;
            this.numberProduced.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Gulim", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(212, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(395, 34);
            this.label6.TabIndex = 12;
            this.label6.Text = "Assembly Line Kanban";
            // 
            // numberOfRunningWorkstations
            // 
            this.numberOfRunningWorkstations.AutoSize = true;
            this.numberOfRunningWorkstations.Location = new System.Drawing.Point(258, 91);
            this.numberOfRunningWorkstations.Name = "numberOfRunningWorkstations";
            this.numberOfRunningWorkstations.Size = new System.Drawing.Size(31, 15);
            this.numberOfRunningWorkstations.TabIndex = 14;
            this.numberOfRunningWorkstations.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(232, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Number of Running Workstations: ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numberOfRunningWorkstations);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numberProduced);
            this.Controls.Add(this.processAmount);
            this.Controls.Add(this.orderAmount);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.totalYieldForAllWorkstations);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Assembly Line Kanban";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label totalYieldForAllWorkstations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label orderAmount;
        private System.Windows.Forms.Label processAmount;
        private System.Windows.Forms.Label numberProduced;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label numberOfRunningWorkstations;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
    }
}

