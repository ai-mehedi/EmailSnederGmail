using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailSneder
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //String email=g_Gmail.Text.Trim();
            //String password = g_Password.Text;

            //if(email== "" || password == "")
            //{
            //    MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else
            //{

            //    MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DashboardPanel.Controls.Clear();

            // Create an instance of the AddGmailSmtp UserControl
            AddGmailSmtp addGmailSmtpControl = new AddGmailSmtp();

            // Optionally, you can set the size of the UserControl if needed
            addGmailSmtpControl.Dock = DockStyle.Fill;  // Optional: Fills the panel

            // Add the AddGmailSmtp UserControl to the DashboardPanel
            DashboardPanel.Controls.Add(addGmailSmtpControl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashboardPanel.Controls.Clear();

            // Create an instance of the AddGmailSmtp UserControl
            Mailbody mailbody = new Mailbody();
            // Optionally, you can set the size of the UserControl if needed
            mailbody.Dock = DockStyle.Fill;  // Optional: Fills the panel

            // Add the AddGmailSmtp UserControl to the DashboardPanel
            DashboardPanel.Controls.Add(mailbody);
        }

        private void DashboardPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DashboardPanel.Controls.Clear();


            // Create an instance of the AddGmailSmtp UserControl
            SendEmail senemailpanel = new SendEmail();
            // Optionally, you can set the size of the UserControl if needed
            senemailpanel.Dock = DockStyle.Fill;  // Optional: Fills the panel

            // Add the AddGmailSmtp UserControl to the DashboardPanel
            DashboardPanel.Controls.Add(senemailpanel);
        }
    }
}
