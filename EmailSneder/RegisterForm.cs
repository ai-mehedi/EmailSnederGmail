using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailSender;

namespace EmailSneder
{
    public partial class RegisterForm : Form
    {
        private MyDatabase db;
        public RegisterForm()
        {
            InitializeComponent();
            db = new MyDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            r_pass.UseSystemPasswordChar = r_showpass.Checked;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = r_email.Text.Trim();
            string password = r_pass.Text.Trim();
            try {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    db.InsertUser(email, password);
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();

                    MessageBox.Show("User Registered Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

         
        }

    }
}
