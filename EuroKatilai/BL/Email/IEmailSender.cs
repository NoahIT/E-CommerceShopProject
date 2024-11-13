﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Email
{
    public interface IEmailSender
    {
        Task SendEmail(ChatBotModel model);
    }
}