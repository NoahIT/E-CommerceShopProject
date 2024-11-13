using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Email
{
    public class EmailHostinger : IEmailHostinger
    {
        public async Task SendNoReply(ChatBotModel x)
        {
            var client = EmailHelper.GetSmptClient();

            MailMessage messageToUser = new MailMessage
            {
                From = new MailAddress("noreply@eurokatilai.lt"),
                Subject = x.Subject,
                Body = x.Message
            };
            messageToUser.To.Add(x.Email);

            try
            {
                await client.SendMailAsync(messageToUser);

                Console.WriteLine("Сообщение отправлено успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при отправке сообщения: {ex.Message}");
            }
}
    }
}
