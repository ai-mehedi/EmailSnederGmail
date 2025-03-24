using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class EmailService
    {
        private MyDatabase db;
        private Random random;

        public EmailService()
        {
            db = new MyDatabase(); // Initialize the database connection
            random = new Random(); // For random selection of SMTP
        }

        // Optimized SMTP Rotation Logic
        public void SendEmails()
        {
            // Get all active SMTP configurations from the database
            DataTable smtpConfigs = db.GetSMTPsData();
            List<DataRow> activeSmtpList = smtpConfigs.AsEnumerable()
                .Where(row => row["status"].ToString() == "active").ToList();

            if (!activeSmtpList.Any())
            {
                Console.WriteLine("No active SMTP configurations available.");
                return;
            }

            // Get pending messages from the message table
            DataTable messages = db.GetmailbodyData();
            DataTable emailList = db.GetEmailData();

            foreach (DataRow emailRow in emailList.Rows)
            {
                string recipientEmail = emailRow["email"].ToString();
                string fullname = $"{emailRow["firstname"]} {emailRow["lastname"]}"; // Construct full name

                foreach (DataRow message in messages.Rows)
                {
                    string subject = message["subject"].ToString();
                    string body = message["body"].ToString().Replace("{fullname}", fullname);

                    // Randomly select an SMTP configuration
                    DataRow smtpConfig = activeSmtpList[random.Next(activeSmtpList.Count)];
                    string smtpEmail = smtpConfig["email"].ToString();
                    string smtpPassword = smtpConfig["password"].ToString();

                    // Send the email
                    SendEmail(smtpEmail, smtpPassword, subject, body, recipientEmail);

                    // Update the message status in the database
                    int messageId = Convert.ToInt32(message["id"]);
                    db.UpdateMessage(messageId, subject, body, "sent");
                }
            }
        }
        public void SendEmailToRecipient(string recipientEmail, string fullName)
        {
            try
            {
                DataTable smtpConfigs = db.GetSMTPsData();
                List<DataRow> activeSmtpList = smtpConfigs.AsEnumerable()
                    .Where(row => row["status"].ToString() == "active").ToList();

                if (!activeSmtpList.Any())
                {
                    Console.WriteLine("No active SMTP configurations available.");
                    return;
                }

                Random random = new Random();
                DataRow smtpConfig = activeSmtpList[random.Next(activeSmtpList.Count)];
                string smtpEmail = smtpConfig["email"].ToString();
                string smtpPassword = smtpConfig["password"].ToString();

                DataTable messages = db.GetmailbodyData();
                foreach (DataRow message in messages.Rows)
                {
                    string subject = message["subject"].ToString();
                    string body = message["body"].ToString().Replace("{fullname}", fullName);

                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new NetworkCredential(smtpEmail, smtpPassword);
                        smtpClient.EnableSsl = true;

                        MailMessage mailMessage = new MailMessage
                        {
                            From = new MailAddress(smtpEmail),
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(recipientEmail);
                        smtpClient.Send(mailMessage);

                        Console.WriteLine($"Email sent to {recipientEmail} using {smtpEmail}");

                        // Update email status in database
                        db.UpdateEmailByEmailAndStatus(recipientEmail, "sent");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email to {recipientEmail}: {ex.Message}");
                db.UpdateEmailByEmailAndStatus(recipientEmail, "fail");
            }
        }

        private void SendEmail(string smtpEmail, string smtpPassword, string subject, string body, string recipientEmail)
        {
            try
            {
                // Set up SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com") // Use dynamic SMTP server if needed
                {
                    Port = 587,
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                    EnableSsl = true
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(recipientEmail);

                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine($"Email sent to: {recipientEmail} using {smtpEmail}");

                // Update email status in the database
                db.UpdateEmailByEmailAndStatus(recipientEmail, "sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email to {recipientEmail}: {ex.Message}");
            }
        }
    }
}
