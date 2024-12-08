namespace AspNetCorePasswordless.Services
{
    public class SendGridOptions
    {
        public const string SectionName = "SendGrid";

        public string FromEmail { get; set; }

        public string FromName { get; set; }

        public string ApiKey { get; set; }
    }
}
