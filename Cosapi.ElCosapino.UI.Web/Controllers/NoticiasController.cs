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
    public class NoticiasController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel Model = new PageViewModel();

        public ActionResult Index(string i,string f)
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams {
                IsPaginate = false,
                PageIndex = 0,
                RowsPerPage = 0,
                Desde = string.IsNullOrEmpty(i) ? "" : i,
                Hasta = string.IsNullOrEmpty(f) ? "" : f,
                SortColumn = "S_FechaPublicacion",
                SortDirection = "Descending",
                Activo = "A" };

            //Obtenemos los valores
            List<InterfaceWeb> lst_iweb_SeccionNoticias = new List<InterfaceWeb>();
            lst_iweb_SeccionNoticias = _InterfaceWebService.List_InterfaceWebNoticiaFiltro_Paginate(paginateParams, Constantes.ModulosWeb.Noticias, Constantes.ModulosSeccionWeb.Noticias_Noticias);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            List<InterfaceWeb> lst_iweb_Trimestre = new List<InterfaceWeb>();
            lst_iweb_Trimestre = _InterfaceWebService.List_InterfaceWebNoticia_Trimestre();

            //Mapeo de valores en el modelo
            Model.Noticias_SeccionNoticias = lst_iweb_SeccionNoticias;
            Model.Footer_SeccionIconos = lst_iweb_Footer;
            Model.Noticias_Trimestre = lst_iweb_Trimestre;

            return View(Model);
        }
    }
}