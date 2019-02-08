using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Cosapi.ElCosapino.UI.Web.Util
{
    public class EmailLogic
    {
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        private String emailFrom;
        private String emailFromName;
        private String emailUserName;
        private String emailPassword;
        private NetworkCredential credentials;

        public EmailLogic()
        {
            this.emailFrom = ConfigurationManager.AppSettings["SendGird.From"].ToString();
            this.emailFromName = ConfigurationManager.AppSettings["SendGird.FromName"].ToString();
            this.emailUserName = ConfigurationManager.AppSettings["SendGrid.UserName"].ToString();
            this.emailPassword = ConfigurationManager.AppSettings["SendGrid.Password"].ToString();
            this.credentials = new NetworkCredential(emailUserName, emailPassword);
        }

        #region SendEmail
        public void SendEmail(String destinatario, String asunto, String body)
        {
            SendEmail(new List<String> { destinatario }, asunto, body);
        }

        public void SendEmail(String destinatario, String asunto, String template, object model)
        {
            var templateReder = new TemplateRender();
            var body = templateReder.Render(template, model);

            SendEmail(new List<String> { destinatario }, asunto, body);
        }
        public void SendEmail(List<String> destinatarios, String asunto, String template, object model)
        {
            var templateReder = new TemplateRender();
            var body = templateReder.Render(template, model);

            SendEmail(destinatarios, asunto, body);
        }

        public void SendEmail(List<String> destinatarios, String asunto, String body)
        {
            try
            {
                var templateReder = new TemplateRender();
                var myMessage = new SendGridMessage();

                myMessage.AddTo(destinatarios);
                myMessage.From = new MailAddress(emailFrom, emailFromName);
                myMessage.Subject = asunto;
                myMessage.Html = body;

                var transporWeb = new SendGrid.Web(credentials);
                if (transporWeb != null)
                    transporWeb.DeliverAsync(myMessage);
            }
            catch
            {
            }
        }
        #endregion

        public void SendEmailVerificacionCorreo(Int32 usuarioId)
        {
            var usuario = _UsuarioService.UsuarioPorID(usuarioId);  //context.Usuario.Find(usuarioId);
            var token = new Encriptacion().Encriptar(usuario.UsuarioId.ToString(), ConfigurationManager.AppSettings["IV"].ToString());
            var model = new { Usuario = usuario, Token = token };
            SendEmail(usuario.CorreoElectronico, "COSAPI", "EmailVerificacionCorreo", model);
        }

        public void SendEmailResetPassword(Int32 usuarioId)
        {
            var usuario = _UsuarioService.UsuarioPorID(usuarioId);  //context.Usuario.Find(usuarioId);
            var model = new { Usuario = usuario };
            SendEmail(usuario.CorreoElectronico, "COSAPI", "EmailResetPassword", model);
        }

        public void SendNotificaion(List<String> destinatarios, String titulo, String descripcion)
        {
            var model = new { Titulo = titulo, Descripcion = descripcion };
            foreach (var correo in destinatarios)
            {
                SendEmail(correo, "COSAPI - Notificación", "Notificacion", model);
            }
        }
    }
}