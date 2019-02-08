using ASPSnippets.FaceBookAPI;
using Cosapi.ElCosapino.Aplicacion.Security.LogService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using Cosapi.ElCosapino.UI.Web.Models.ViewModels;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class LoginFacebookController : Controller
    {
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        readonly ILogAppService _LogService = new LogAppService();

        public ActionResult Index()
        {           
            FaceBookUser faceBookUser = new FaceBookUser();
            if (Request.QueryString["error"] == "access_denied")
            {
                ViewBag.Message = "User has denied access.";
            }
            else
            {
                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    try
                    {
                        string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                        faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                        //faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);

                        Usuario entidad = new Usuario
                        {
                            Nombres = faceBookUser.Name.Split(' ')[0],
                            CorreoElectronico = faceBookUser.Email,
                            UsuarioIDExterno = faceBookUser.Id,
                            Estado = "ACT",
                            Origen = "FB",
                            DeviceToken = ""
                        };

                        var Resultado = _UsuarioService.Insert_RedesSociales(entidad);
                        if (Resultado.UsuarioId != 0)
                        {
                            Usuario objUsuario = _UsuarioService.UsuarioPorID(Resultado.UsuarioId);

                            VMDatosUsuarioExterno.SetValueLogin(faceBookUser.Name.Split(' ')[0], faceBookUser.Email, "Externo", null, Resultado.UsuarioId.ToString(), "ACT", objUsuario.DNI);
                            FormsAuthentication.SetAuthCookie(VMDatosUsuarioExterno.GetUserAlias(), false);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log _log = new Log
                        {
                            IDCategoria = 1,
                            UsuarioCreacion = "",
                            Mensaje = "Origen:WEB - Método:Index(Controller LoginFacebookController) - Error:" + ex.GetBaseException().ToString()
                        };
                        _LogService.Insert(_log);
                    }
                }
            }
            return Redirect("~/TrabajaConNosotros");
        }

        [HttpPost]
        public EmptyResult Login_Facebook()
        {
            FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FacebookAPIKey"];
            FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FacebookAPISecret"];
            string URLCosapinoFB = ConfigurationManager.AppSettings["COSAPI.APP.AbsoluteUrl"].ToString() + "LoginFacebook/Index/";
            FaceBookConnect.Authorize("user_photos,email", URLCosapinoFB);
            return new EmptyResult();
        }
    }
}