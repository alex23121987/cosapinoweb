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
    public class ProyectosEmblematicosController : Controller
    {
        readonly InterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel model = new PageViewModel();

        public ActionResult Index()
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "", Activo = "A" };
            var paginateParams_lt = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_Titulo", SortDirection = "Ascending", Activo = "A" };

            //Obtenemos los valores
            InterfaceWeb iweb_SeccionSuperior = new InterfaceWeb { IDModulo = 1, IDSeccion = 201 };
            iweb_SeccionSuperior = _InterfaceWebService.List_InterfaceWeb_Unique(iweb_SeccionSuperior);

            List<InterfaceWeb> lst_iweb_SeccionProyectosEmblematicos = new List<InterfaceWeb>();
            lst_iweb_SeccionProyectosEmblematicos = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.ProyectosEmblematicos, Constantes.ModulosSeccionWeb.ProyectosEmblematicos_ProyectosEmblematicos);

            List<InterfaceWeb> lst_iweb_SeccionImagenesSuperiores = new List<InterfaceWeb>();
            lst_iweb_SeccionImagenesSuperiores = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.ProyectosEmblematicos, Constantes.ModulosSeccionWeb.ProyectosEmblematicos_ImagenesSuperiores);

            List<InterfaceWeb> lst_iweb_SeccionLineaTiempo = new List<InterfaceWeb>();
            lst_iweb_SeccionLineaTiempo = _InterfaceWebService.List_InterfaceWebLineaTiempo_Paginate(paginateParams_lt, Constantes.ModulosWeb.ProyectosEmblematicos, Constantes.ModulosSeccionWeb.ProyectosEmblematicos_EventosLineaTiempo,0);

            List<InterfaceWeb> lst_iweb_SeccionLineaTiempoProyecto = new List<InterfaceWeb>();
            lst_iweb_SeccionLineaTiempoProyecto = _InterfaceWebService.List_InterfaceWebLineaTiempoProyecto_Paginate(paginateParams, Constantes.ModulosWeb.ProyectosEmblematicos, Constantes.ModulosSeccionWeb.ProyectosEmblematicos_EventosLineaTiempoProyecto,0);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            //Mapeo de valores en el modelo
            model.ProyectosEmblematicos_SeccionMedio = iweb_SeccionSuperior.Titulo;
            model.ProyectosEmblematicos_SeccionProyectosEmblematicos = lst_iweb_SeccionProyectosEmblematicos;
            model.ProyectosEmblematicos_SeccionImagenesSuperiores = lst_iweb_SeccionImagenesSuperiores;
            model.ProyectosEmblematicos_SeccionLineaTiempo = lst_iweb_SeccionLineaTiempo;
            model.ProyectosEmblematicos_SeccionLineaTiempoProyecto = lst_iweb_SeccionLineaTiempoProyecto;
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);
        }        
    }
}