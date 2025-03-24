namespace EmailSneder
{
    partial class Mailbody
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
            this.mailMessageGridView = new System.Windows.Forms.DataGridView();
            this.m_deletebtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mailMessageGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mailMessageGridView
            // 
            this.mailMessageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mailMessageGridView.Location = new System.Drawing.Point(33, 58);
            this.mailMessageGridView.Name = "mailMessageGridView";
            this.mailMessageGridView.Size = new System.Drawing.Size(507, 379);
            this.mailMessageGridView.TabIndex = 0;
            this.mailMessageGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mailMessageGridView_CellContentClick);
            // 
            // m_deletebtn
            // 
            this.m_deletebtn.Location = new System.Drawing.Point(465, 29);
            this.m_deletebtn.Name = "m_deletebtn";
            this.m_deletebtn.Size = new System.Drawing.Size(75, 23);
            this.m_deletebtn.TabIndex = 1;
            this.m_deletebtn.Text = "Delete";
            this.m_deletebtn.UseVisualStyleBackColor = true;
            this.m_deletebtn.Click += new System.EventHandler(this.m_deletebtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(338, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add Mail Message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Mailbody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_deletebtn);
            this.Controls.Add(this.mailMessageGridView);
            this.Name = "Mailbody";
            this.Size = new System.Drawing.Size(585, 489);
            ((System.ComponentModel.ISupportInitialize)(this.mailMessageGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mailMessageGridView;
        private System.Windows.Forms.Button m_deletebtn;
        private System.Windows.Forms.Button button1;
    }
}
