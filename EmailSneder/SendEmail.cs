using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailSender;

namespace EmailSneder
{
    public partial class SendEmail : UserControl
    {
        private void LoadSMTPs()
        {
            EmailListShow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            EmailListShow.DataSource = db.GetEmailData();  // Bind the DataTable to DataGridView
        }

        private MyDatabase db;  // Database instance to interact with the database

        public SendEmail()
        {
            InitializeComponent();
            db = new MyDatabase();
            LoadSMTPs();
        }

        private void SendEmail_Load(object sender, EventArgs e)
        {

        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            // Open file dialog to select CSV file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get file path
                string filePath = openFileDialog.FileName;

                // Read CSV file and extract data
                List<Contact> contacts = ReadCsvFile(filePath);

                // Save contacts to the database with "draft" status
                foreach (var contact in contacts)
                {
                    db.InsertEmail(contact.Firstname,contact.Lastname,contact.Email ,"fail");
                }

                MessageBox.Show("Contacts saved successfully as drafts.");
            }

        }




        // Method to read CSV file and extract contacts
        private List<Contact> ReadCsvFile(string filePath)
        {
            List<Contact> contacts = new List<Contact>();

            // Read all lines from the CSV file
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1)) // Skip header row
            {
                var columns = line.Split(',');

                if (columns.Length >= 3)
                {
                    var contact = new Contact
                    {
                        Firstname = columns[0].Trim(),
                        Lastname = columns[1].Trim(),
                        Email = columns[2].Trim()
                    };

                    contacts.Add(contact);
                }
            }

            return contacts;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void delatTime_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Startbtn_Click(object sender, EventArgs e)
        {
            try
            {
                EmailService emailService = new EmailService();
                DataTable emailList = db.GetEmailData();

                int totalEmails = emailList.Rows.Count;
                if (totalEmails == 0)
                {
                    MessageBox.Show("No emails to send.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                progressBar1.Maximum = totalEmails; // Set progress bar max value
                progressBar1.Value = 0; // Reset progress bar

                int sentCount = 0;

                await Task.Run(() =>
                {
                    foreach (DataRow emailRow in emailList.Rows)
                    {
                        string recipientEmail = emailRow["email"].ToString();
                        string fullName = $"{emailRow["firstname"]} {emailRow["lastname"]}";

                        // Send the email and update database
                        emailService.SendEmailToRecipient(recipientEmail, fullName);

                        sentCount++;

                        // Update UI safely using Invoke
                        Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Value = sentCount;
                            LoadSMTPs(); // Refresh DataGridView after each email is sent
                        });

                        Task.Delay(500).Wait(); // Small delay to prevent UI freezing
                    }
                });

                MessageBox.Show("All emails sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmailListShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row's ID, assuming the ID is in the first column.
                int selectedId = Convert.ToInt32(EmailListShow.Rows[e.RowIndex].Cells[0].Value);

                // Optionally, display this ID on the Delete button or elsewhere for confirmation.
                // button2.Tag = selectedId; // This will store the selected ID in the button's Tag property.
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
    public class Contact
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
