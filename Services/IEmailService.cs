using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
