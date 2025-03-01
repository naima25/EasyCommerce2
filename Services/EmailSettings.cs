public class EmailSettings
{
    //The EmailSettings class holds the configuration settings required for connecting to an SMTP server
    //An SMTP server is a server that handles the process of sending emails over the internet
    public string? SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string? SmtpUsername { get; set; }
    public string? SmtpPassword { get; set; }
}
