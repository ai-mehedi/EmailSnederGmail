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
    public partial class Mailbody : UserControl
    {
        private MyDatabase db;
        private void LoadSMTPs()
        {
            mailMessageGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            mailMessageGridView.DataSource = db.GetmailbodyData();  // Bind the DataTable to DataGridView
        }
        public Mailbody()
        {

            InitializeComponent();
            db = new MyDatabase();
            LoadSMTPs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmailBody addEmailBody = new AddEmailBody();
            addEmailBody.Show();

        }

        private void mailMessageGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row's ID, assuming the ID is in the first column.
                int selectedId = Convert.ToInt32(mailMessageGridView.Rows[e.RowIndex].Cells[0].Value);

                // Optionally, display this ID on the Delete button or elsewhere for confirmation.
                // button2.Tag = selectedId; // This will store the selected ID in the button's Tag property.
            }
        }

        private void m_deletebtn_Click(object sender, EventArgs e)
        {
            // Ensure that a valid ID is selected. 
            // You can use a confirmation dialog before actually deleting the record.
            if (mailMessageGridView.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(mailMessageGridView.SelectedRows[0].Cells[0].Value); // Assuming ID is in the first column.

                // Call the method to delete the email from the database.
                bool success = DeleteMainbody(selectedId);

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

        private bool DeleteMainbody(int id)
        {
            try
            {
                // Assuming MyDatabase has a method to delete an email by ID.
                db.DeleteMessage(id);
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during deletion.
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
