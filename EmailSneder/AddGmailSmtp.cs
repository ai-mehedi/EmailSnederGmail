using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using EmailSender;  // Assuming this namespace has MyDatabase class and other necessary components

namespace EmailSneder
{
    public partial class AddGmailSmtp : UserControl
    {
        private MyDatabase db;  // Database instance to interact with the database

        private void LoadSMTPs()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.DataSource = db.GetSMTPsData();  // Bind the DataTable to DataGridView
        }
        public int TestGmailSMTP(String semail, String spassword)
        {
            string smtpServer = "smtp.gmail.com";
            int port = 587; // TLS
            string username = semail;  // Replace with your Gmail address
            string password = spassword;  // Use your Gmail App Password (not your Gmail login password)

            try
            {
                // Create the SMTP client
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, port))
                {
                    // Set SMTP client properties
                    smtpClient.EnableSsl = true;  // Use SSL
                    smtpClient.Credentials = new NetworkCredential(username, password);  // Set credentials

                    // Attempt to connect to the SMTP server by sending a dummy email (no actual email is sent)
                    smtpClient.Send(new MailMessage(username, username));  // Dummy send to test the connection

                    Console.WriteLine("SMTP connection test completed successfully.");
                    return 0;  // Return 0 for valid connection
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;  // Return 1 for invalid connection
            }
        }
        public AddGmailSmtp()
        {
            InitializeComponent();
            db = new MyDatabase();
            LoadSMTPs();// Initialize database connection or ORM
        }

        // This event is triggered when a cell in the DataGridView is clicked.
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure a valid row was clicked, not the header row.
            if (e.RowIndex >= 0)
            {
                // Get the selected row's ID, assuming the ID is in the first column.
                int selectedId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                // Optionally, display this ID on the Delete button or elsewhere for confirmation.
                // button2.Tag = selectedId; // This will store the selected ID in the button's Tag property.
            }
        }

        // This method will be called when the delete button is clicked.
        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure that a valid ID is selected. 
            // You can use a confirmation dialog before actually deleting the record.
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // Assuming ID is in the first column.

                // Call the method to delete the email from the database.
                bool success = DeleteSMTP(selectedId);

                if (success)
                {
                    // Optionally, refresh the DataGridView after deletion to reflect changes.
                    db.GetSMTPs();  // Assuming this method re-fetches the data and updates the DataGridView.
                    MessageBox.Show("Email deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete the email.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        // Method to delete the email from the database by its ID.
        private bool DeleteSMTP(int id)
        {
            try
            {
                // Assuming MyDatabase has a method to delete an email by ID.
                db.DeleteSMTP(id);
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during deletion.
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = s_Gmail.Text.Trim();
            string password = s_Password.Text.Trim();

            if (email == "" || password == "")
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                db.InsertSMTP(email, password, "active");
                MessageBox.Show("SMTP added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string email = s_Gmail.Text.Trim();
            string password = s_Password.Text.Trim();
            if (email == "" || password == "")
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var test = TestGmailSMTP(email, password);
                if (test == 0)
                {
                    db.InsertSMTP(email, password, "active");
                    MessageBox.Show("SMTP connection test successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("SMTP connection test failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
