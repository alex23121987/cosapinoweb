using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class NoticiasDetalleController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel Model = new PageViewModel();

        public ActionResult Index(string IDNoticia)
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            //Obtenemos los valores
            InterfaceWeb filtro = new InterfaceWeb
            {
                IDModulo = Constantes.ModulosWeb.Noticias,
                IDSeccion = Constantes.ModulosSeccionWeb.Noticias_Noticias,
                IDInterfaceWeb=Convert.ToInt32(IDNoticia)
            };

            InterfaceWeb lst_iweb_SeccionNoticiaDetalle = new InterfaceWeb();
            lst_iweb_SeccionNoticiaDetalle = _InterfaceWebService.List_InterfaceWebNoticia_Unique(filtro);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            List<InterfaceWeb> lst_iweb_Trimestre = new List<InterfaceWeb>();
            lst_iweb_Trimestre = _InterfaceWebService.List_InterfaceWebNoticia_Trimestre();

            //Mapeo de valores en el modelo
            Model.Noticias_SeccionDetalle = lst_iweb_SeccionNoticiaDetalle;
            Model.Footer_SeccionIconos = lst_iweb_Footer;
            Model.Noticias_Trimestre = lst_iweb_Trimestre;

            return View(Model);
        }
    }
}