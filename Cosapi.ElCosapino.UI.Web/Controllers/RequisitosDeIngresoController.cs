using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class RequisitosDeIngresoController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModel model = new PageViewModel();

        public ActionResult Index()
        {
            //Seteo de variables
            ViewBag.UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "", Activo = "A" };

            //Obtenemos los valores
          
            List<InterfaceWeb> lst_iweb_SeccionImagen = new List<InterfaceWeb>();
            lst_iweb_SeccionImagen = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.RequisitosIngreso ,Constantes.ModulosSeccionWeb.RequisitosIngreso_ImagenesLaterales);

            List<InterfaceWeb> lst_iweb_SeccionRequisitos = new List<InterfaceWeb>();
            lst_iweb_SeccionRequisitos = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.RequisitosIngreso, Constantes.ModulosSeccionWeb.RequisitosIngreso_RequisitosIngreso);

            List<InterfaceWeb> lst_iweb_Footer = new List<InterfaceWeb>();
            lst_iweb_Footer = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, Constantes.ModulosWeb.Footer, Constantes.ModulosSeccionWeb.Footer_RedesSociales);

            //Mapeo de valores en el modelo            
            model.RequisitosDeIngreso_SeccionImagen = lst_iweb_SeccionImagen;
            model.RequisitosDeIngreso_SeccionRequisitos = lst_iweb_SeccionRequisitos;
            model.Footer_SeccionIconos = lst_iweb_Footer;

            return View(model);
        }
    }
}