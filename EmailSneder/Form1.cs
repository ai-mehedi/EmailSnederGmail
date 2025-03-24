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
    public partial class Form1 : Form
    {
        private MyDatabase db;
        public Form1()
        {
            InitializeComponent();
            db = new MyDatabase(); // Assuming Database class has a parameterless constructor
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = login_email.Text.Trim();
            string password = login_pass.Text.Trim();





            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            login_pass.UseSystemPasswordChar = ShowPass.Checked;
        }
    }
}
