using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.Aplicacion.Security.LoginService;
using Cosapi.ElCosapino.Aplicacion.Security.PostulacionService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.Aplicacion.Security.VacanteService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using Cosapi.ElCosapino.UI.Web.Models.ViewModels;
using Cosapi.ElCosapino.UI.Web.TransferObject.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using static Cosapi.ElCosapino.CrossCutting.Util.Constantes;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    [RequireHttps]
    public class TrabajaConNosotrosController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();        
        readonly IVacanteAppService _VacanteService = new VacanteAppService();
        readonly ILoginAppService _LoginService = new LoginAppService();
        readonly IPostulacionAppService _PostulacionService = new PostulacionAppService();
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        private PageViewModel Model = new PageViewModel();

        public ActionResult Index()
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParamsf = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "", Activo = "A" };

            List<Vacante> lst_iweb_SeccionVacante = new List<Vacante>();

            if (VMDatosUsuarioExterno.GetUUserId() != Logueo.NoLogueado.ToString())
            {
                var usuario = _UsuarioService.UsuarioPorID(Convert.ToInt32(VMDatosUsuarioExterno.GetUUserId()));
                if (usuario.Estado == "ACT")
                {
                    VMDatosUsuarioExterno.SetUserEstado(usuario.Estado);
                }
            }

            /////// ---- LOGIN CON GOOGLE
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GoogleAPIKey"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GoogleAPISecret"];
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);

                Usuario entidad = new Usuario
                {
                    Nombres = profile.DisplayName.Split(' ')[0],
                    CorreoElectronico = profile.Emails.Find(email => email.Type == "account").Value,
                    UsuarioIDExterno = profile.Id,
                    Estado = "ACT",
                    Origen = "GO",
                    DeviceToken = ""
                };

                var Resultado = _UsuarioService.Insert_RedesSociales(entidad);
                if (Resultado.UsuarioId != 0)
                {
                    Usuario objUsuario = _UsuarioService.UsuarioPorID(Resultado.UsuarioId);

                    VMDatosUsuarioExterno.SetValueLogin(profile.DisplayName.Split(' ')[0], profile.Emails.Find(email => email.Type == "account").Value, "Google", null, Resultado.UsuarioId.ToString(), "ACT", objUsuario.DNI);
                    FormsAuthentication.SetAuthCookie(VMDatosUsuarioExterno.GetUserAlias(), false);
                    return Redirect("~/TrabajaConNosotros");
                }
            }
            if (Request.QueryString["error"] == "access_denied")
            {
                ViewBag.Message = "User has denied access.";
            }
            /////// ---- FIN LOGIN CON GOOGLE

            if (VMDatosUsuarioExterno.GetUUserId() != Logueo.NoLogueado.ToString() && VMDatosUsuarioExterno.GetUserEstado()=="ACT" && !string.IsNullOrEmpty(VMDatosUsuarioExterno.GetUserDNI()))                
            {                
                var paginateParams = new PaginateParams {
                    IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion",
                    SortDirection = "Descending", Activo = "A",UsuarioId=Convert.ToInt32(VMDatosUsuarioExterno.GetUUserId())
                };

                //Obtenemos los valores                
                lst_iweb_SeccionVacante = _VacanteService.List_Vacante_Paginate(paginateParams);
            }

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParamsf, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            //Mapeo de valores en el modelo
            Model.TrabajaConNosotros_SeccionVacante = lst_iweb_SeccionVacante;
            Model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(Model);
        }

        [HttpPost]
        public ActionResult Login_Usuario_Externo()
        {
            Usuario _usuario = new Usuario();
                          
            string Param_Usuario = Request.Params["Usuario"].ToString();
            string Param_Clave = Request.Params["Clave"].ToString();               
                
            Login entidad = new Login {
                S_Usuario = Param_Usuario,
                S_Password= Param_Clave
            };
            _usuario=_LoginService.Verificar_Usuario_Externo(entidad);
            if (_usuario.Resultado == Logueo.Logueado)
            {
                VMDatosUsuarioExterno.SetValueLogin(_usuario.NombreCompleto.Split(' ')[0], _usuario.CorreoElectronico, "Externo", null, _usuario.UsuarioId.ToString(), _usuario.Estado, _usuario.DNI);                
                FormsAuthentication.SetAuthCookie(VMDatosUsuarioExterno.GetUserAlias(), false);                                
            }
            return Json(_usuario);
        }       

        [HttpPost]
        public EmptyResult Login_Facebook()
        {
            FaceBookConnect.Authorize("user_photos,email", string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Authority, "TrabajaConNosotros/Index/"));
            return new EmptyResult();
        }

        [HttpPost]
        public EmptyResult Login_Google()
        {            
            GoogleConnect.Authorize("profile", "email");           
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Postular(VMPostulacion model)
        {
            var result = new Resultado();
            try
            {
                if (model.PostulacionBE.PerfilId == 0)
                {
                    return View(model);
                }
                model.PostulacionBE.UsuarioId = Convert.ToInt32(VMDatosUsuarioExterno.GetUUserId());
                var Resultado = _PostulacionService.Insert(model.PostulacionBE);
                result.Codigo = Resultado.UsuarioId;
            }
            catch (System.Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
                return View();
            }
            return Json(result);
        }
    }
}