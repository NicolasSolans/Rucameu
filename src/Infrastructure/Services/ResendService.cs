using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ResendService : IResendService
    {
        private readonly IResend _resend;

        public ResendService(IResend resend)
        {
            _resend = resend;
        }

        public async Task Execute(QueryDTO query)
    {
        var message = new EmailMessage();
        message.From = query.Cart.User.Email;
        message.To.Add( "rucameushop@gmail.com" );
        message.Subject = "Consulta de compra";
        message.HtmlBody = query.Message + " " + query.Cart.ToString();

        await _resend.EmailSendAsync( message );
    }
    }
}
