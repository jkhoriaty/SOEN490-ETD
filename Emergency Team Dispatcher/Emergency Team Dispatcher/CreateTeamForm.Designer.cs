namespace Emergency_Team_Dispatcher
{
    partial class CreateTeamForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.teamName = new System.Windows.Forms.TextBox();
			this.radioName = new System.Windows.Forms.TextBox();
			this.radioDeparturehh = new System.Windows.Forms.TextBox();
			this.radioLevelOfTraining = new System.Windows.Forms.ComboBox();
			this.firstAidLevelOfTraining = new System.Windows.Forms.ComboBox();
			this.firstAidName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.radioDeparturemm = new System.Windows.Forms.TextBox();
			this.fAidmm = new System.Windows.Forms.TextBox();
			this.fAidhh = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Enabled = false;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(25, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 20);
			this.label1.TabIndex = 19;
			this.label1.Text = "Team Name";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new System.Drawing.Point(55, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 18;
			this.label2.Text = "Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Location = new System.Drawing.Point(55, 123);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(151, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "Time of Departure (24h format)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(25, 54);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(167, 20);
			this.label4.TabIndex = 16;
			this.label4.Text = "Team Member (Radio)";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Enabled = false;
			this.label5.Location = new System.Drawing.Point(55, 155);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "Level Of Training";
			// 
			// teamName
			// 
			this.teamName.Location = new System.Drawing.Point(253, 19);
			this.teamName.Name = "teamName";
			this.teamName.Size = new System.Drawing.Size(100, 20);
			this.teamName.TabIndex = 0;
			// 
			// radioName
			// 
			this.radioName.Location = new System.Drawing.Point(253, 86);
			this.radioName.Name = "radioName";
			this.radioName.Size = new System.Drawing.Size(125, 20);
			this.radioName.TabIndex = 1;
			// 
			// radioDeparturehh
			// 
			this.radioDeparturehh.Location = new System.Drawing.Point(253, 120);
			this.radioDeparturehh.MaxLength = 2;
			this.radioDeparturehh.Name = "radioDeparturehh";
			this.radioDeparturehh.Size = new System.Drawing.Size(25, 20);
			this.radioDeparturehh.TabIndex = 2;
			this.radioDeparturehh.Text = "hh";
			this.radioDeparturehh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.radioDeparturehh.Click += new System.EventHandler(this.time_Click);
			this.radioDeparturehh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.time_Click);
			this.radioDeparturehh.Leave += new System.EventHandler(this.restorehh_Leave);
			// 
			// radioLevelOfTraining
			// 
			this.radioLevelOfTraining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.radioLevelOfTraining.FormattingEnabled = true;
			this.radioLevelOfTraining.Items.AddRange(new object[] {
            "General First Aid",
            "First Responder",
            "Medicine"});
			this.radioLevelOfTraining.Location = new System.Drawing.Point(253, 152);
			this.radioLevelOfTraining.Name = "radioLevelOfTraining";
			this.radioLevelOfTraining.Size = new System.Drawing.Size(121, 21);
			this.radioLevelOfTraining.TabIndex = 4;
			// 
			// firstAidLevelOfTraining
			// 
			this.firstAidLevelOfTraining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.firstAidLevelOfTraining.FormattingEnabled = true;
			this.firstAidLevelOfTraining.Items.AddRange(new object[] {
            "General First Aid",
            "First Responder",
            "Medicine"});
			this.firstAidLevelOfTraining.Location = new System.Drawing.Point(253, 288);
			this.firstAidLevelOfTraining.Name = "firstAidLevelOfTraining";
			this.firstAidLevelOfTraining.Size = new System.Drawing.Size(121, 21);
			this.firstAidLevelOfTraining.TabIndex = 8;
			// 
			// firstAidName
			// 
			this.firstAidName.Location = new System.Drawing.Point(253, 222);
			this.firstAidName.Name = "firstAidName";
			this.firstAidName.Size = new System.Drawing.Size(125, 20);
			this.firstAidName.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Enabled = false;
			this.label6.Location = new System.Drawing.Point(55, 291);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Level Of Training";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Enabled = false;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(25, 190);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(205, 20);
			this.label7.TabIndex = 12;
			this.label7.Text = "Team Member (First Aid Kit)";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Enabled = false;
			this.label8.Location = new System.Drawing.Point(55, 259);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(151, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Time of Departure (24h format)";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Enabled = false;
			this.label9.Location = new System.Drawing.Point(55, 225);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(35, 13);
			this.label9.TabIndex = 14;
			this.label9.Text = "Name";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(117, 349);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 9;
			this.button1.Text = "Confirm";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(227, 349);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// radioDeparturemm
			// 
			this.radioDeparturemm.Location = new System.Drawing.Point(292, 120);
			this.radioDeparturemm.MaxLength = 2;
			this.radioDeparturemm.Name = "radioDeparturemm";
			this.radioDeparturemm.Size = new System.Drawing.Size(25, 20);
			this.radioDeparturemm.TabIndex = 3;
			this.radioDeparturemm.Text = "mm";
			this.radioDeparturemm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.radioDeparturemm.Click += new System.EventHandler(this.time_Click);
			this.radioDeparturemm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.time_Click);
			this.radioDeparturemm.Leave += new System.EventHandler(this.restoremm_Leave);
			// 
			// fAidmm
			// 
			this.fAidmm.Location = new System.Drawing.Point(293, 256);
			this.fAidmm.MaxLength = 2;
			this.fAidmm.Name = "fAidmm";
			this.fAidmm.Size = new System.Drawing.Size(25, 20);
			this.fAidmm.TabIndex = 7;
			this.fAidmm.Text = "mm";
			this.fAidmm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.fAidmm.Click += new System.EventHandler(this.time_Click);
			this.fAidmm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.time_Click);
			this.fAidmm.Leave += new System.EventHandler(this.restoremm_Leave);
			// 
			// fAidhh
			// 
			this.fAidhh.Location = new System.Drawing.Point(252, 256);
			this.fAidhh.MaxLength = 2;
			this.fAidhh.Name = "fAidhh";
			this.fAidhh.Size = new System.Drawing.Size(25, 20);
			this.fAidhh.TabIndex = 6;
			this.fAidhh.Text = "hh";
			this.fAidhh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.fAidhh.Click += new System.EventHandler(this.time_Click);
			this.fAidhh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.time_Click);
			this.fAidhh.Leave += new System.EventHandler(this.restorehh_Leave);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Enabled = false;
			this.label10.Location = new System.Drawing.Point(280, 123);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(10, 13);
			this.label10.TabIndex = 20;
			this.label10.Text = ":";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Enabled = false;
			this.label11.Location = new System.Drawing.Point(280, 259);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(10, 13);
			this.label11.TabIndex = 21;
			this.label11.Text = ":";
			// 
			// CreateTeamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(422, 394);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.fAidmm);
			this.Controls.Add(this.fAidhh);
			this.Controls.Add(this.radioDeparturemm);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.firstAidLevelOfTraining);
			this.Controls.Add(this.firstAidName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.radioLevelOfTraining);
			this.Controls.Add(this.radioDeparturehh);
			this.Controls.Add(this.radioName);
			this.Controls.Add(this.teamName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CreateTeamForm";
			this.Text = "CreateTeamForm";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox teamName;
        private System.Windows.Forms.TextBox radioName;
        private System.Windows.Forms.TextBox radioDeparturehh;
        private System.Windows.Forms.ComboBox radioLevelOfTraining;
        private System.Windows.Forms.ComboBox firstAidLevelOfTraining;
        private System.Windows.Forms.TextBox firstAidName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox radioDeparturemm;
        private System.Windows.Forms.TextBox fAidmm;
        private System.Windows.Forms.TextBox fAidhh;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
    }
}