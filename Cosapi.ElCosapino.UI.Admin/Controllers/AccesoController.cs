using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.AccesoService;
using Cosapi.ElCosapino.Aplicacion.Security.RolService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class AccesoController : BaseController
    {
        readonly IRolAppService _RolService = new RolAppService();
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        readonly IAccesoAppService _AccesoService = new AccesoAppService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<Administrador>>(arg);

            var paginateParams = new PaginateParams
            {
                IsPaginate = true,
                PageIndex = modelData.CurrentPageIndex,
                RowsPerPage = modelData.RowsPerPage,
                SortColumn = modelData.OrderBy,
                SortDirection = modelData.DirectionOrder
            };
            if (modelData.Filters != null)
                paginateParams.Filters = Converter.ListaToDatatable<CanvasFilter>(modelData.Filters.ToList());

            modelData.Data = _AccesoService.List_Acceso_Paginate(paginateParams);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult Insert(VMUsuario model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Administrador.Codigo))
                {
                    return View(model);
                }
                //model.Administrador.UsuarioCreacion = VMDatosUsuario.GetUserAlias();
                model.Administrador.Estado = "ACT";
                var Resultado = _AccesoService.Insert(model.Administrador);
                result.Codigo = Resultado.AdministradorId;
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
        public ActionResult Update(VMUsuario model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Administrador.Codigo))
                {
                    return View(model);
                }

                //model.Administrador.UsuarioModificacion = VMDatosUsuario.GetUserAlias();
                model.Administrador.Estado = model.Administrador.Estado.Equals("True") ? "ACT" : "INA";
                var Resultado = _AccesoService.Update(model.Administrador);
                result.Codigo = Resultado.AdministradorId;
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
        public ActionResult Delete(int CodParameter)
        {
            var result = new Resultado();
            try
            {
                var AccesoDelete = new Administrador
                {
                    AdministradorId = CodParameter //,
                    //UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _AccesoService.Delete(AccesoDelete);
                result.Codigo = Resultado.AdministradorId;
            }
            catch (System.Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
                return View();
            }
            return Json(result);
        }

        [HttpGet]
        public JsonResult List_Rol()
        {
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "" };
            var resultado = _RolService.List_Rol_Paginate(paginateParams);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}