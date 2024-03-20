namespace JobPortalAPI_1.Repository
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Dictionary<string, string> MailContent);
        Task<Dictionary<string, string>> SendMessage(Dictionary<string, string> MailContent);
    }
}
