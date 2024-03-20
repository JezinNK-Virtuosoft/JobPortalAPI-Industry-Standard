using System.Net.Mail;
using System.Net;

namespace JobPortalAPI_1.Repository
{
    public class EmailSender:IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task<bool> SendEmail(Dictionary<string, string> MailContent)
        {
            var mail = "demoJobPortal@outlook.com";
            var pwd = _configuration["Credentials:Password"];
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pwd)
            };
            try
            {
                client.SendMailAsync(
                    new MailMessage(
                        from: mail, to: MailContent["Email"], MailContent["Subject"], MailContent["Message"]));
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<Dictionary<string, string>> SendMessage(Dictionary<string, string> MailContent)
        {
            Dictionary<string, string> EmailContent = new Dictionary<string, string>();
            await Task.Run(() =>
            {
                string Subject = "Welcome Aboard! Verify Your Email Address for Full Access to Job Portal";
                string Message = $"Dear {MailContent["Name"]},\r\n\r\nWelcome to JobPortal! We're thrilled to have you on board. To ensure the security of your account, we've generated a temporary password for you to sign in. Here are your temporary login details:\r\n\r\nUsername/Email:{MailContent["Email"]} \r\nTemporary Password: {MailContent[MailContent["Email"]]}" +
                     "\r\nPlease sign in using the provided credentials to access your account. Once signed in, we highly recommend that you update your password to something more memorable and secure. You can change your password by following these simple steps:\r\n\r\n" +
                     "1.Sign in to your account using the temporary credentials provided above.\r\n" +
                     "2.Navigate to your account settings or profile settings.\r\n" +
                     "3.Select the option to change your password.\r\n" +
                     "4.Enter your current temporary password and create a new, secure password.\r\n" +
                     "5.Save your changes.\r\nRemember, choosing a strong password is crucial for the security of your account. " +
                     "Ensure your new password is unique, includes a mix of letters, numbers, and special characters, and is not easily guessable." +
                     "Thank you for choosing Job Portal. We look forward to assisting you in your job search journey.\r\n\r\nBest regards,";
                EmailContent.Add("Subject", Subject);
                EmailContent.Add("Message", Message);
                EmailContent.Add("Email", MailContent["Email"]);

            });
            return EmailContent;
        }

    }
}
