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
            message.From = $"\"{query.Cart.User.Email}\" <onboarding@resend.dev>";
            message.To.Add("rucameushop@gmail.com");
            message.Subject = "Consulta de compra";
            var itemsHtml = string.Join("", query.Cart.Items.Select(item =>
                $@"<li>
        {item.ProductDTO.Name} — Cantidad: {item.Quantity} — Subtotal: ${item.Subtotal:F2}
       </li>"
            ));

            message.HtmlBody = $@"
<p><strong>{query.Cart.User.Email}</strong> te ha enviado una consulta:</p>

<p style='font-size: 15px; color: #333;'>{query.Message}</p>

<hr/>

<h3>🛒 Detalles del carrito</h3>
<ul>
    {itemsHtml}
</ul>

<p><strong>Total del carrito:</strong> ${query.Cart.TotalPrice:F2}</p>
<p><strong>ID del carrito:</strong> {query.Cart.Id}</p>
";

            await _resend.EmailSendAsync(message);
        }
    }
}
