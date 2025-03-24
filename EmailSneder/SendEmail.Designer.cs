namespace EmailSneder
{
    partial class SendEmail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EmailListShow = new System.Windows.Forms.DataGridView();
            this.Startbtn = new System.Windows.Forms.Button();
            this.delatTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.EmailListShow)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(460, 65);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(75, 23);
            this.SelectFile.TabIndex = 0;
            this.SelectFile.Text = "Open File";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sent Email";
            // 
            // EmailListShow
            // 
            this.EmailListShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmailListShow.Location = new System.Drawing.Point(23, 94);
            this.EmailListShow.Name = "EmailListShow";
            this.EmailListShow.Size = new System.Drawing.Size(512, 258);
            this.EmailListShow.TabIndex = 3;
            this.EmailListShow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmailListShow_CellContentClick);
            // 
            // Startbtn
            // 
            this.Startbtn.Location = new System.Drawing.Point(427, 384);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(108, 23);
            this.Startbtn.TabIndex = 4;
            this.Startbtn.Text = "Start";
            this.Startbtn.UseVisualStyleBackColor = true;
            this.Startbtn.Click += new System.EventHandler(this.Startbtn_Click);
            // 
            // delatTime
            // 
            this.delatTime.Location = new System.Drawing.Point(333, 384);
            this.delatTime.Name = "delatTime";
            this.delatTime.Size = new System.Drawing.Size(88, 20);
            this.delatTime.TabIndex = 5;
            this.delatTime.TextChanged += new System.EventHandler(this.delatTime_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Delay Time";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 384);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(304, 23);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // SendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delatTime);
            this.Controls.Add(this.Startbtn);
            this.Controls.Add(this.EmailListShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectFile);
            this.Name = "SendEmail";
            this.Size = new System.Drawing.Size(585, 489);
            this.Load += new System.EventHandler(this.SendEmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EmailListShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView EmailListShow;
        private System.Windows.Forms.Button Startbtn;
        private System.Windows.Forms.TextBox delatTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
