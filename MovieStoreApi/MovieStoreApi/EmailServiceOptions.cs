namespace MovieStoreApi
{
    public class EmailServiceOptions
    {
        public static string SectionName = "EmailService";
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public int TimeInterval { get; set; }
    }
}
