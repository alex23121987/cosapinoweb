using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{   
    public class ContactoController : Controller
    {
        readonly InterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel model = new PageViewModel();

        public ActionResult Index()
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "", Activo = "A" };

            //Obtenemos los valores
            List<InterfaceWeb> lst_iweb_SeccionContacto = new List<InterfaceWeb>();
            lst_iweb_SeccionContacto = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Contacto, Constantes.ModulosSeccionWeb.Contacto_MayorInformacion);

            List<InterfaceWeb> lst_iweb_SeccionDireccion = new List<InterfaceWeb>();
            lst_iweb_SeccionDireccion = _InterfaceWebService.List_InterfaceWebOficina_Paginate(paginateParams, Constantes.ModulosWeb.Contacto, Constantes.ModulosSeccionWeb.Contacto_DireccionesOficina);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            //Mapeo de valores en el modelo
            model.Contacto_SeccionContacto = lst_iweb_SeccionContacto;
            model.Contacto_SeccionDireccion = lst_iweb_SeccionDireccion;
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);
        }
    }
}