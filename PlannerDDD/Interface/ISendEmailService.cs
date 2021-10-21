using System;
using System.Threading.Tasks;

namespace Interface
{
    public interface ISendEmailService
    {
        // The function to send email
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
