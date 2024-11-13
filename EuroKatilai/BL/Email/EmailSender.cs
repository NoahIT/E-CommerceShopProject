using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Email;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Email
{
    public class EmailSender : IEmailSender
    {
        public readonly IEmailHostinger hostinger;

        public EmailSender(IEmailHostinger hostinger)
        {
            this.hostinger = hostinger;
        }

        public async Task SendEmail(ChatBotModel model)
        {
            await hostinger.SendNoReply(model);
        }
    }
}
