using Cosapi.ElCosapino.Aplicacion.Security.CategoriaService;
using Cosapi.ElCosapino.Aplicacion.Security.DepartamentoService;
using Cosapi.ElCosapino.Aplicacion.Security.DistritoService;
using Cosapi.ElCosapino.Aplicacion.Security.EspecialidadService;
using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.Aplicacion.Security.PostulacionService;
using Cosapi.ElCosapino.Aplicacion.Security.ProvinciaService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.Aplicacion.Security.VacanteService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using Cosapi.ElCosapino.UI.Web.Models.ViewModels;
using Cosapi.ElCosapino.UI.Web.TransferObject.Json;
using Cosapi.ElCosapino.UI.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class PerfilController : Controller
    {
        readonly InterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        readonly ICategoriaAppService _CategoriaService = new CategoriaAppService();
        readonly IEspecialidadAppService _EspecialidadService = new EspecialidadAppService();
        readonly IDepartamentoAppService _DepartamentoService = new DepartamentoAppService();
        readonly IProvinciaAppService _ProvinciaService = new ProvinciaAppService();
        readonly IDistritoAppService _DistritoService = new DistritoAppService();
        readonly IVacanteAppService _VacanteService = new VacanteAppService();
        readonly IPostulacionAppService _PostulacionService = new PostulacionAppService();

        private PageViewModel model = new PageViewModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MisPostulaciones()
        {
            if (VMDatosUsuarioExterno.GetUUserId() == "-1")
            {
                return Redirect("~/Home");
            }

            List<PostulacionBE> lst_iweb_SeccionPostulacion= new List<PostulacionBE>();

            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            //Obtenemos los valores                
            lst_iweb_SeccionPostulacion = _PostulacionService.List_Postulacion_APP(Convert.ToInt32(VMDatosUsuarioExterno.GetUUserId()));

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);


            //Mapeo de valores en el modelo
            model.Perfil_Postulacion = lst_iweb_SeccionPostulacion;
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);           
        }

        public ActionResult Actualizar()
        {
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);


            if (string.IsNullOrEmpty(VMDatosUsuarioExterno.GetUUserId()) || VMDatosUsuarioExterno.GetUUserId()=="-1") {
                return Redirect(ConfigurationManager.AppSettings["UrlCosapino"]);
            }
            else
            {
                Usuario objUsuario = _UsuarioService.UsuarioPorID(Convert.ToInt32(VMDatosUsuarioExterno.GetUUserId()));
                
                model.Usuario = objUsuario;              
            }
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);
        }

        public ActionResult Nuevo()
        {
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            model.Footer_SeccionIconos = lst_iweb_Footer;
            return View(model);
        }

        public ActionResult ResetearClave()
        {
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            model.Footer_SeccionIconos = lst_iweb_Footer;
            return View(model);
        }

        [HttpGet]
        public JsonResult List_Categoria()
        {            
            var resultado = _CategoriaService.List_Categoria_APP();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Especialidad()
        {
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "" };
            var resultado = _EspecialidadService.List_Especialidad_Paginate(paginateParams);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Especialidad_Por_Categoria(int IDCategoria)
        {
            Especialidad filtro = new Especialidad
            {
                IDCategoria = IDCategoria
            };
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "" };
            var resultado = _EspecialidadService.List_Especialidad_Paginate_Filtro(paginateParams, filtro);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Departamento()
        {            
            var list = _DepartamentoService.List_Departamento_APP();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Provincia(int IDDepartamento)
        {
            var list = _ProvinciaService.List_Provincia_APP(IDDepartamento); // proxy.List_Provincia(IDDepartamento);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Distrito(int IDProvincia)
        {
            var list = _DistritoService.List_Distrito_APP(IDProvincia);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(VMUsuarioExterno model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Usuario.ApellidoPaterno)
                   || string.IsNullOrEmpty(model.Usuario.ApellidoMaterno)
                   || string.IsNullOrEmpty(model.Usuario.Nombres)
                   || string.IsNullOrEmpty(model.Usuario.DNI)
                   || string.IsNullOrEmpty(model.Usuario.NumeroContacto1)
                   || string.IsNullOrEmpty(model.Usuario.strFechaNacimiento))
                {
                    return View(model);
                }
                model.Usuario.Estado = "REG";
                var Resultado = _UsuarioService.Insert(model.Usuario);
                result.Codigo = Resultado.UsuarioId;

                try
                {
                    if (result.Codigo != 0)
                    {
                        var emailLogic = new EmailLogic();
                        emailLogic.SendEmailVerificacionCorreo(result.Codigo);
                    }
                }
                catch (Exception ex)
                {
                    result.EsExito = false;
                    result.Mensaje = ex.Message;
                    return View();
                }

                VMDatosUsuarioExterno.SetValueLogin(model.Usuario.Nombres, model.Usuario.CorreoElectronico, "", null, result.Codigo.ToString(), "REG", model.Usuario.DNI);
                FormsAuthentication.SetAuthCookie(VMDatosUsuarioExterno.GetUserAlias(), false);
            }
            catch (System.Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
                return View();
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update(VMUsuarioExterno model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Usuario.ApellidoPaterno)
                   || string.IsNullOrEmpty(model.Usuario.ApellidoMaterno) 
                   || string.IsNullOrEmpty(model.Usuario.Nombres)
                   || string.IsNullOrEmpty(model.Usuario.DNI)
                   || string.IsNullOrEmpty(model.Usuario.NumeroContacto1)
                   || string.IsNullOrEmpty(model.Usuario.strFechaNacimiento))
                {
                    return View(model);
                }
           
                var Resultado = _UsuarioService.Update(model.Usuario);
                result.Codigo = Resultado.UsuarioId;

                if (result.Codigo > 0)
                {
                    VMDatosUsuarioExterno.SetUserDNI(model.Usuario.DNI);
                }
            }
            catch (System.Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
                return View();
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult RecuperarClave(VMUsuarioExterno model)
        {
            var result = new Resultado();
            try
            {

                Usuario objUsuario = _UsuarioService.UsuarioPorCorreo_Sin_RedesSociales(model.Usuario.CorreoElectronico);

                if (objUsuario == null)
                {
                    result.Codigo = -1;               
                    return View();
                }                    

                Usuario objNewUsuario = new Usuario();
                objNewUsuario.UsuarioId = objUsuario.UsuarioId;
                objNewUsuario.Password = Guid.NewGuid().ToString().Replace("-", String.Empty).Substring(0, 8);
                _UsuarioService.UpdatePassword(objNewUsuario);

                try
                {
                    var emailLogic = new EmailLogic();
                    emailLogic.SendEmailResetPassword(objUsuario.UsuarioId);
                }
                catch (Exception ex)
                {
                    result.Codigo = 0;
                    result.EsExito = false;
                    result.Mensaje = ex.Message;
                    return View();
                }
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