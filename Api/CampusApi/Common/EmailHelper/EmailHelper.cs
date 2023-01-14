using Core.Models;
using static Core.Models.EmailModel;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Common.EmailHelper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly string notificationServer;
        private readonly int notificationPort;
        private readonly string notificationMail;
        private readonly string notificationPassword;
        private readonly bool enableSsl;
        // Color del texto del titulo
        private readonly string titleColor = "#211e1c";
        private readonly string fontFamilyTitles = "Arial,sans-serif";
        private readonly IConfiguration _configuration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            
            notificationServer = _configuration["EmailSettings:NotificationServer"];
            notificationPort = Convert.ToInt32(_configuration["EmailSettings:notificationPort"]);
            notificationMail = _configuration["EmailSettings:NotificationMail"];
            notificationPassword = _configuration["EmailSettings:NotificationPassword"];
            enableSsl = Convert.ToBoolean(_configuration["EmailSettings:EnableSsl"]);
        }

        public dynamic SendEmail(EmailModel emailDto)
        {
            // Crear Mail
            MailMessage netMail = new MailMessage();
            var email = notificationMail;
            var passwd = notificationPassword;
            var server = notificationServer;
            var port = notificationPort;

            netMail.From = new MailAddress(email, emailDto.FromDisplayName);

            emailDto.Addressee.To.ForEach(fe => netMail.To.Add(new MailAddress(fe)));

            // Agregar CC
            if (emailDto.Addressee.CC.Any())
                foreach (string CC in emailDto.Addressee.CC)
                    if (!string.IsNullOrEmpty(CC))
                        netMail.CC.Add(new MailAddress(CC));

            // Agregar CCO
            if (emailDto.Addressee.CCO.Any())
                foreach (string CCO in emailDto.Addressee.CCO)
                    if (!string.IsNullOrEmpty(CCO))
                        netMail.Bcc.Add(new MailAddress(CCO));

            if (!string.IsNullOrEmpty(Environment.MachineName) && !Environment.MachineName.ToLower().StartsWith("pro"))
            {
                emailDto.Subject = $"{emailDto.Subject}";
            }
            netMail.Subject = emailDto.Subject;
            netMail.Body = CreateBasicMail(emailDto);

            if (emailDto.Attachements.Any())
            {
                foreach (var att in emailDto.Attachements)
                {
                    netMail.Attachments.Add(att);
                }
            }

            netMail.IsBodyHtml = true;

            try
            {
                // Crear el cliente y enviar el correo
                using (SmtpClient client = new SmtpClient(server, port))
                {
                    client.EnableSsl = enableSsl;
                    client.Credentials = new System.Net.NetworkCredential(email, passwd);

                    client.Send(netMail);
                    return new 
                    {
                        Type = "Notificacion",
                        Message = "Success"
                    };
                }
            }
            catch (Exception e)
            {
                return new 
                {
                    Type = "Error",
                    Message = "Error"
                };
            }
        }

        public string CreateBasicMail(EmailModel mail)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                if (!string.IsNullOrEmpty(mail.Body.Title))
                    CreateTitle(stringWriter, mail.Body.Title);

                if (!string.IsNullOrEmpty(mail.Body.HtmlContent))
                    CreateSubTitle(stringWriter, mail.Body.HtmlContent);

                if (mail.Body.HeaderMessages.Any())
                    CreateHeaderMessages(stringWriter, mail.Body.HeaderMessages);

                if (mail.Body.FooterMessages.Any())
                    CreateFooterMessages(stringWriter, mail.Body.FooterMessages);

                return stringWriter.ToString();
            }
        }

        private void CreateSubTitle(StringWriter writer, string subTitle)
        {
            writer.Write($"{subTitle}");
        }

        private void CreateTitle(StringWriter writer, string title)
        {
            writer.Write($"<H1 style=\"color: {titleColor}; font-family: {fontFamilyTitles};fontSize: 40px;font-weight:bold;letter-spacing:-1px;line-height:115%;margin:0;padding:0;text-align:center;\">{title}</H1>");
        }

        private void CreateHeaderMessages(StringWriter writer, List<string> headerMessages)
        {

            writer.Write($"<div id=\"HeaderMessagesDiv\" style=\"margin-top:15px;padding:1px 5px;\">");
            foreach (var headerMessage in headerMessages)
            {
                writer.Write($"<h3 style=\"color: {titleColor};fontSize: 1.2em;\">{headerMessage}</h3>");
            }
            // End #1
            writer.Write($"</div>");
        }

        private void CreateFooterMessages(StringWriter writer, List<string> footerMessages)
        {
            writer.Write($"<div id=\"FooterMessagesDiv\" style=\"margin-top:15px;padding:1px 5px;\">");
            foreach (var footerMessage in footerMessages)
            {
                writer.Write($"<h3 style=\"color: {titleColor};fontSize: 1.2em;\">{footerMessage}</h3>");
            }
            writer.Write($"</div>");
        }
    }
}
