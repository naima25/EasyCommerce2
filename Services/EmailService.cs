using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

// The EmailService class handles sending emails using SMTP. It is configured with email settings such as 
// the SMTP server, port, username, and password, which are injected through the EmailSettings class
// The main functionality is provided by the SendEmail method, which constructs an email with a subject 
// and body, and sends it to a specified recipient using MailKit's SmtpClient

public class EmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    // Method to send an email to a specified recipient with a subject and body
    public void SendEmail(string toEmail, string subject, string body)
    {
        var message = new MimeMessage(); // Create a new MimeMessage (email message)
        message.From.Add(new MailboxAddress("Support Student App", _emailSettings.SmtpUsername)); // Set the sender's email address and name (from the configuration)
        message.To.Add(new MailboxAddress("Receiver Name", toEmail)); // Set the recipient's email address and name 
        message.Subject = subject; // Set the subject of the email

        var textPart = new TextPart("plain") // Create a new plain text part for the body of the email
        {
            Text = body
        }; 

        message.Body = textPart;

        using (var client = new SmtpClient()) // Use an SmtpClient to connect and send the email
        {
            client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls); // Connect to the SMTP server using the settings from EmailSettings
            client.Authenticate(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword); // Authenticate with the SMTP server using the username and password from EmailSettings
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
