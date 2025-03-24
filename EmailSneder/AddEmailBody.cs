using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailSender;

namespace EmailSneder
{
    public partial class AddEmailBody : Form
    {
        private MyDatabase db;
        public AddEmailBody()
        {
            InitializeComponent();
            db = new MyDatabase();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String subject = subJect.Text.Trim();
            String body=mstHtmlEditor1.BodyHTML;

            try
            {
                if (body == "" || subject == "")
                {
                    MessageBox.Show("Please enter both subject and body.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                else
                {
                    db.InsertMessage(subject, body, "pending");
                    MessageBox.Show("Email Body Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        

          
        }

        private void AddEmailBody_Load(object sender, EventArgs e)
        {

        }
    }
}
