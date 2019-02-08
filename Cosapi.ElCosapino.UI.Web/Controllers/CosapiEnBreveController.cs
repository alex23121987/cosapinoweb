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
    public class CosapiEnBreveController : Controller
    {
        readonly InterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel model = new PageViewModel();

        public ActionResult Index()
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams{IsPaginate = false,PageIndex = 0,RowsPerPage = 0,SortColumn = "",SortDirection = "", Activo = "A" };

            //Obtenemos los valores
            InterfaceWeb iweb_SeccionSuperior = new InterfaceWeb { IDModulo = Constantes.ModulosWeb.CosapiEnBreve, IDSeccion = Constantes.ModulosSeccionWeb.CosapiEnBreve_Superior };
            iweb_SeccionSuperior = _InterfaceWebService.List_InterfaceWeb_Unique(iweb_SeccionSuperior);

            List<InterfaceWeb> lst_iweb_SeccionLogo = new List<InterfaceWeb>();            
            lst_iweb_SeccionLogo = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.CosapiEnBreve, Constantes.ModulosSeccionWeb.CosapiEnBreve_Logo);

            List<InterfaceWeb> lst_iweb_SeccionInferior = new List<InterfaceWeb>();
            lst_iweb_SeccionInferior = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.CosapiEnBreve, Constantes.ModulosSeccionWeb.CosapiEnBreve_Inferior);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            //Mapeo de valores en el modelo
            model.CosapiBreve_SeccionSuperior = iweb_SeccionSuperior.Titulo;
            model.CosapiBreve_SeccionLogo = lst_iweb_SeccionLogo;
            model.CosapiBreve_SeccionInferior= lst_iweb_SeccionInferior;
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);
        }
    }
}