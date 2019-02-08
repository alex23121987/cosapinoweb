using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Servicio.Helpers;
using Cosapi.ElCosapino.Servicio.Util;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.Servicio.Controllers
{
    public class HomeController : BaseController
    {
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ValidarEmail(String Token)
        {
            Encriptacion objEncriptacion = new Encriptacion();
            int UsuarioId = Convert.ToInt32(objEncriptacion.Desencriptar(Token, ConfigurationManager.AppSettings["IV"]));         
            Usuario objUsuario = _UsuarioService.UsuarioPorID(UsuarioId); 

            if (objUsuario == null)
            {
                PostMessage(MessageType.Error, "El usuario no existe.");
            }
            else if (objUsuario.Estado != "REG")
            {
                PostMessage(MessageType.Error, "El usuario ya se encuentra activo.");
            }
            else
            {
                objUsuario.Estado = "ACT";
                _UsuarioService.UpdateEstado(objUsuario);                
                PostMessage(MessageType.Success, "El usuario se activó correctamente. Por favor, ingrese a la aplicación.");
            }

            return View();
        }
    }
}
