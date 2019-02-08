using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.UI.Web.Helpers;
using Cosapi.ElCosapino.UI.Web.Models;
using Cosapi.ElCosapino.UI.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class HomeController : BaseController
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();

        private PageViewModel Model = new PageViewModel();

        public ActionResult Index()
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            //Obtenemos los valores
            List<InterfaceWeb> lst_iweb_SeccionNoticias = new List<InterfaceWeb>();
            lst_iweb_SeccionNoticias = _InterfaceWebService.List_InterfaceWebNoticiaDestacado_Paginate(paginateParams, Constantes.ModulosWeb.Noticias, Constantes.ModulosSeccionWeb.Noticias_Noticias);

            List<InterfaceWeb> lst_iweb_SeccionNoticiasUltimos = new List<InterfaceWeb>();
            lst_iweb_SeccionNoticiasUltimos = _InterfaceWebService.List_InterfaceWebNoticiaUltimos_Paginate(paginateParams, Constantes.ModulosWeb.Noticias, Constantes.ModulosSeccionWeb.Noticias_Noticias);

            List<InterfaceWeb> lst_iweb_PortadaImaganesSup = new List<InterfaceWeb>();
            lst_iweb_PortadaImaganesSup = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Portada, Constantes.ModulosSeccionWeb.Portada_ImagenesSuperiores);


            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);


            //Mapeo de valores en el modelo
            Model.Noticias_SeccionNoticiasDestacado = lst_iweb_SeccionNoticias;
            Model.Noticias_SeccionNoticiasUltimos = lst_iweb_SeccionNoticiasUltimos;
            Model.Portada_SeccionImagenesSuperiores = lst_iweb_PortadaImaganesSup;
            Model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(Model);
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