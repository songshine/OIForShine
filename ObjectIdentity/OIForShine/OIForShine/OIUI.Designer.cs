namespace OIForShine
{
    partial class OIUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbKMax = new System.Windows.Forms.TextBox();
            this.tbKMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTestPath = new System.Windows.Forms.Button();
            this.tbTestPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTrainPath = new System.Windows.Forms.Button();
            this.tbTrainPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbOutputLog = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnGO = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbKMax);
            this.groupBox5.Controls.Add(this.tbKMin);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(22, 132);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(611, 46);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // tbKMax
            // 
            this.tbKMax.Location = new System.Drawing.Point(148, 18);
            this.tbKMax.Name = "tbKMax";
            this.tbKMax.Size = new System.Drawing.Size(47, 21);
            this.tbKMax.TabIndex = 4;
            // 
            // tbKMin
            // 
            this.tbKMin.Location = new System.Drawing.Point(72, 18);
            this.tbKMin.Name = "tbKMin";
            this.tbKMin.Size = new System.Drawing.Size(47, 21);
            this.tbKMin.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "K Range";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTestPath);
            this.groupBox4.Controls.Add(this.tbTestPath);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(22, 79);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(611, 47);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // btnTestPath
            // 
            this.btnTestPath.Location = new System.Drawing.Point(500, 20);
            this.btnTestPath.Name = "btnTestPath";
            this.btnTestPath.Size = new System.Drawing.Size(105, 23);
            this.btnTestPath.TabIndex = 2;
            this.btnTestPath.Text = "Browser...";
            this.btnTestPath.UseVisualStyleBackColor = true;
            this.btnTestPath.Click += new System.EventHandler(this.btnTestPath_Click);
            // 
            // tbTestPath
            // 
            this.tbTestPath.Location = new System.Drawing.Point(72, 20);
            this.tbTestPath.Name = "tbTestPath";
            this.tbTestPath.Size = new System.Drawing.Size(399, 21);
            this.tbTestPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Test Path";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTrainPath);
            this.groupBox3.Controls.Add(this.tbTrainPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(22, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(611, 52);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // btnTrainPath
            // 
            this.btnTrainPath.Location = new System.Drawing.Point(500, 23);
            this.btnTrainPath.Name = "btnTrainPath";
            this.btnTrainPath.Size = new System.Drawing.Size(105, 23);
            this.btnTrainPath.TabIndex = 2;
            this.btnTrainPath.Text = "Browser...";
            this.btnTrainPath.UseVisualStyleBackColor = true;
            this.btnTrainPath.Click += new System.EventHandler(this.btnTrainPath_Click);
            // 
            // tbTrainPath
            // 
            this.tbTrainPath.Location = new System.Drawing.Point(72, 23);
            this.tbTrainPath.Name = "tbTrainPath";
            this.tbTrainPath.Size = new System.Drawing.Size(399, 21);
            this.tbTrainPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Train Path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbOutputLog);
            this.groupBox2.Location = new System.Drawing.Point(13, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(639, 297);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // tbOutputLog
            // 
            this.tbOutputLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbOutputLog.Location = new System.Drawing.Point(7, 21);
            this.tbOutputLog.Multiline = true;
            this.tbOutputLog.Name = "tbOutputLog";
            this.tbOutputLog.ReadOnly = true;
            this.tbOutputLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutputLog.Size = new System.Drawing.Size(626, 270);
            this.tbOutputLog.TabIndex = 0;
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(538, 506);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(114, 35);
            this.btnGO.TabIndex = 1;
            this.btnGO.Text = "GO";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(372, 506);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(114, 35);
            this.btnClearLog.TabIndex = 2;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // OIUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 553);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "OIUI";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnTestPath;
        private System.Windows.Forms.TextBox tbTestPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTrainPath;
        private System.Windows.Forms.TextBox tbTrainPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbOutputLog;
        private System.Windows.Forms.TextBox tbKMax;
        private System.Windows.Forms.TextBox tbKMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Button btnClearLog;
    }
}

