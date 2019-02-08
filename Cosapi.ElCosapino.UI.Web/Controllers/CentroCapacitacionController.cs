using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using Cosapi.ElCosapino.UI.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using static Cosapi.ElCosapino.CrossCutting.Util.Constantes;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class CentroCapacitacionController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel model = new PageViewModel();

        public ActionResult Index()
        {            
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "",Activo="A"};

            //Obtenemos los valores
            InterfaceWeb iweb_SeccionSuperior = new InterfaceWeb { IDModulo = Constantes.ModulosWeb.CentroCapacitacion, IDSeccion = Constantes.ModulosSeccionWeb.CentroCapacitacion_TextoSuperior };
            iweb_SeccionSuperior = _InterfaceWebService.List_InterfaceWeb_Unique(iweb_SeccionSuperior);

            List<InterfaceWeb> lst_iweb_SeccionImagenesSuperiores = new List<InterfaceWeb>();
            lst_iweb_SeccionImagenesSuperiores = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.CentroCapacitacion, Constantes.ModulosSeccionWeb.CentroCapacitacion_ImagenesSuperiores);

            List<InterfaceWeb> lst_iweb_SeccionPrograma = new List<InterfaceWeb>();
            lst_iweb_SeccionPrograma = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.CentroCapacitacion, Constantes.ModulosSeccionWeb.CentroCapacitacion_Programas);

            List<InterfaceWeb> lst_iweb_SeccionGaleriaConFotos = new List<InterfaceWeb>();
            lst_iweb_SeccionGaleriaConFotos = _InterfaceWebService.List_InterfaceWebProgramasConFotos_Paginate(paginateParams, Constantes.ModulosWeb.CentroCapacitacion, Constantes.ModulosSeccionWeb.CentroCapacitacion_Galeria,0);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            //Mapeo de valores en el modelo
            model.CentroCapacitacion_SeccionSuperior = iweb_SeccionSuperior.Titulo;
            model.CentroCapacitacion_SeccionImagenesSuperiores = lst_iweb_SeccionImagenesSuperiores;
            model.CentroCapacitacion_SeccionPrograma = lst_iweb_SeccionPrograma;
            model.CentroCapacitacion_SeccionGaleriaConFotos = lst_iweb_SeccionGaleriaConFotos;
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);
        }

        public ActionResult List_FotosPrograma(int IDPrograma)
        {
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "", Activo = "A" };

            List<InterfaceWeb> lst_iweb_SeccionFotos = new List<InterfaceWeb>();
            lst_iweb_SeccionFotos = _InterfaceWebService.List_InterfaceWebProgramasFotosGaleria_Paginate(paginateParams, Constantes.ModulosWeb.CentroCapacitacion, Constantes.ModulosSeccionWeb.CentroCapacitacion_GaleriaFoto, IDPrograma);


            model.CentroCapacitacion_SeccionFotos = lst_iweb_SeccionFotos;

            return PartialView("_VistaGaleriaFoto", model);
        }
    }
}