namespace Core.Models
{
    public class MailServiceConfiguration
    {
        public string NotificationMail { get; set; }
        public string NotificationPassword { get; set; }
        public string NotificationServer { get; set; }
        public int NotificationPort { get; set; }
        public string AlertMail { get; set; }
        public string AlertPassword { get; set; }
        public string AlertServer { get; set; }
        public int AlertPort { get; set; }
        public bool EnableSsl { get; set; }
        public string DefaultEmail { get; set; }
    }
}
