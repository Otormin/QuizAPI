using QuizWebApiProject.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace QuizWebApiProject.Repository
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            try { 
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("toby25537@gmail.com", "Jesse Classroom");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
                //if (response.IsSuccessStatusCode == false)
                //{
                //    throw new Exception();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
