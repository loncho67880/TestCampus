using Common.EmailHelper;
using Common.ReadTemplateHelper;
using Core.Models;
using Microsoft.Extensions.Configuration;
using static Core.Models.EmailModel;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IReadTemplateHelper _readTemplateHelper;
        private readonly IConfiguration _configuration;
        private readonly IEmailHelper _emailHelper;

        public UserService(IReadTemplateHelper readTemplateHelper, IConfiguration configuration, IEmailHelper emailHelper)
        {
            _readTemplateHelper = readTemplateHelper;
            _configuration = configuration;
            _emailHelper = emailHelper;
        }

        public void Registrar(User user)
        {
            //Enviar Correo de notificación Asincrono
            string templateHtmlBaja = _readTemplateHelper.ReadTemplate(_configuration["MailRegistro"]);
            templateHtmlBaja = templateHtmlBaja.Replace("{nombre}", user.name);
            templateHtmlBaja = templateHtmlBaja.Replace("{email}", user.email);
            templateHtmlBaja = templateHtmlBaja.Replace("{fecha}", user.date.ToString("dd/MM/yyyy"));
            templateHtmlBaja = templateHtmlBaja.Replace("{ciudad}", user.city);


            var emailAnulacion = new EmailModel()
            {
                Subject = "Registro Green Leaves",
                FromDisplayName = "Green Leaves",
                Body = new BodyMail()
                {
                    Title = "",
                    HtmlContent = templateHtmlBaja,
                },
                Type = EmailModel.MailType.Alerta,
                Addressee = new DestinationMail()
                {
                    To = _configuration["mailNotificacion"].Split(";").ToList()
                }
            };

            //Add email persona registrada
            emailAnulacion.Addressee.To.Add(user.email);

            _emailHelper.SendEmail(emailAnulacion);
        }
    }
}
