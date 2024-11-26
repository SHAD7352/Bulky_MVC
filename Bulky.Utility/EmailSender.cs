using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Logic to send email
            //throw new NotImplementedException();
			// Temporary implementation: Log or just complete successfully
			Console.WriteLine($"Email to: {email}, Subject: {subject}, Message: {htmlMessage}");
			return Task.CompletedTask;
		}
    }
}
