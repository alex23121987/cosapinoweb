using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.ParametroGeneralService;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class ParametroGeneralController : BaseController
    {
        // GET: ParametroGeneral
        readonly IParametroGeneralAppService _ParametroGeneralService = new ParametroGeneralAppService();        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<ParametroGeneral>>(arg);

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

            var IDUserUnidad = VMDatosUsuario.GetUUserIdUnidad();
            modelData.Data = _ParametroGeneralService.List_ParametroGeneral_Paginate(paginateParams, IDUserUnidad);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult Insert(VMParametroGeneral model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.ParametroGeneral.S_Descripcion))
                {
                    return View(model);
                }
                model.ParametroGeneral.N_IDUnidad = VMDatosUsuario.GetUUserIdUnidad();
                model.ParametroGeneral.S_Tipo = "U";
                model.ParametroGeneral.S_UsuarioCreacion = VMDatosUsuario.GetUserAlias();
                model.ParametroGeneral.S_Estado = "A";
                var Resultado = _ParametroGeneralService.Insert(model.ParametroGeneral);
                result.Codigo = Resultado.N_IDParametroGeneral;
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
        public ActionResult Update(VMParametroGeneral model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.ParametroGeneral.S_Descripcion))
                {
                    return View(model);
                }

                model.ParametroGeneral.S_UsuarioModificacion =VMDatosUsuario.GetUserAlias();
                model.ParametroGeneral.S_Estado = model.ParametroGeneral.S_Estado.Equals("True") ? "A" : "I";
                var Resultado = _ParametroGeneralService.Update(model.ParametroGeneral);
                result.Codigo = Resultado.N_IDParametroGeneral;
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
                var ParametroGeneralDelete = new ParametroGeneral
                {
                    N_IDParametroGeneral = CodParameter,
                    N_IDUnidad = VMDatosUsuario.GetUUserIdUnidad(),
                    S_UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                //var Resultado = _ParametroGeneralService.Delete(ParametroGeneralDelete);
                //result.Codigo = Resultado.N_IDParametroGeneral;
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
        public ActionResult Activar(int CodParameter)
        {
            var result = new Resultado();
            try
            {
                var ParametroGeneralActivar = new ParametroGeneral
                {
                    N_IDParametroGeneral = CodParameter,
                    S_UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
               // var Resultado = _ParametroGeneralService.Activar(ParametroGeneralActivar);
                //result.Codigo = Resultado.N_IDParametroGeneral;
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
        public JsonResult CityList()
        {
            List<DDLOptionsText> obj = new List<DDLOptionsText>()
            {
                new DDLOptionsText {S_ID="CARGO", S_Descripcion="Cargo" },
                //new DDLOptionsText {S_ID="ESTADO_PERIODO", S_Descripcion="Estado Periodo" },
                //new DDLOptionsText {S_ID="ESTADO_REGISTRO_HH", S_Descripcion="Estado Registro HH"},
                //new DDLOptionsText {S_ID="ESTADO_TRABAJO", S_Descripcion="Estado Trabajo" },
                new DDLOptionsText {S_ID="ETAPA", S_Descripcion="Etapa" },
                //new DDLOptionsText {S_ID="GRUPO_ACTIVIDAD", S_Descripcion="Grupo Actividad" },
                //new DDLOptionsText {S_ID="MES", S_Descripcion="Mes" },
                new DDLOptionsText {S_ID="TIPO_ENTREGABLE", S_Descripcion="Tipo Entregable" },
                //new DDLOptionsText {S_ID="TIPO_MENSAJE", S_Descripcion="Tipo Mensaje" },
                //new DDLOptionsText {S_ID="TIPO_OPCION_SISTEMA", S_Descripcion="Tipo Opcion Sistema" },
                new DDLOptionsText {S_ID="TIPO_TIEMPO_TRABAJO", S_Descripcion="Tipo Tiempo Trabajo" },
                //new DDLOptionsText {S_ID="TIPO_TRABAJO", S_Descripcion="Tipo Trabajo" },
                new DDLOptionsText {S_ID="ACTIVIDAD_GESTION", S_Descripcion="Actividades de Gestión" },
                new DDLOptionsText {S_ID="ACTIVIDAD_OTROS", S_Descripcion="Actividades de Otros" }
            }.ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}