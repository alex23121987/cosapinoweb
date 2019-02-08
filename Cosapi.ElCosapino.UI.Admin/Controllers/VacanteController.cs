using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.CategoriaService;
using Cosapi.ElCosapino.Aplicacion.Security.EspecialidadService;
using Cosapi.ElCosapino.Aplicacion.Security.VacanteService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class VacanteController : BaseController
    {
        readonly ICategoriaAppService _CategoriaService = new CategoriaAppService();
        readonly IEspecialidadAppService _EspecialidadService = new EspecialidadAppService();
        readonly IVacanteAppService _VacanteService = new VacanteAppService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<Vacante>>(arg);

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

            modelData.Data = _VacanteService.List_Vacante_Paginate(paginateParams);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult Insert(VMVacante model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Vacante.Requisitos))
                {
                    return View(model);
                }
                model.Vacante.UsuarioCreacion = VMDatosUsuario.GetUUserId().ToString();
                model.Vacante.Estado = "ACT";
                var Resultado = _VacanteService.Insert(model.Vacante);
                result.Codigo = Resultado.IDVacante;
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
        public ActionResult Update(VMVacante model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Vacante.Requisitos))
                {
                    return View(model);
                }

                model.Vacante.UsuarioModificacion = VMDatosUsuario.GetUserAlias();
                model.Vacante.Estado = model.Vacante.Estado.Equals("True") ? "ACT" : "INA";
                var Resultado = _VacanteService.Update(model.Vacante);
                result.Codigo = Resultado.IDVacante;
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
                var VacanteDelete = new Vacante
                {
                    IDVacante = CodParameter,
                    UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _VacanteService.Delete(VacanteDelete);
                result.Codigo = Resultado.IDVacante;
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
        public JsonResult List_Categoria()
        {
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "" };
            var resultado = _CategoriaService.List_Categoria_Paginate(paginateParams);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Especialidad()
        {
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "" };
            var resultado = _EspecialidadService.List_Especialidad_Paginate(paginateParams);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List_Especialidad_Por_Categoria(int IDCategoria)
        {
            Especialidad filtro = new Especialidad
            {
                IDCategoria = IDCategoria
            };
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "" };
            var resultado = _EspecialidadService.List_Especialidad_Paginate_Filtro(paginateParams, filtro);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GenerarPlantillaReporte()
        {
            var buffer = DescargarReporteProcesado();
            string handle = "Reporte_Vacantes";
            TempData[handle] = buffer;

            return new JsonResult()
            {
                Data = new { FileGuid = handle, FileName = $"{handle}.xlsx" }
            };
        }

        [HttpGet]
        public virtual ActionResult DescargarReporte(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }

        public byte[] DescargarReporteProcesado()
        {
            byte[] resultadoProceso;
            Dictionary<string, List<string>> listHeader = new Dictionary<string, List<string>>();

            DataTable dt = _VacanteService.DT_Vacante();


            List<string> HojaDatos = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                HojaDatos.Add(ObtenerNombre(col.ColumnName));
            }
            listHeader.Add("HojaDatos", HojaDatos);

            //Datos para los registros               
            resultadoProceso = CrearArchivoExcel(dt, listHeader);

            return resultadoProceso;
        }

        public static byte[] CrearArchivoExcel(DataTable dt, Dictionary<string, List<string>> listHeader = null)
        {
            byte[] bytes;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet wsDatos = pck.Workbook.Worksheets.Add("Datos");

                ImprimirHojaDatos(dt, wsDatos, listHeader);

                bytes = pck.GetAsByteArray();
            }
            return bytes;
        }

        public static int dataRow = 1;
        public static int dataColumn = 1;

        private static void ImprimirHojaDatos(DataTable dt, ExcelWorksheet ws, Dictionary<string, List<string>> listHeader)
        {
            List<string> HojaDatos = listHeader["HojaDatos"];

            //ws.Cells[1, 1].Value = "Desde:";
            //ws.Cells[1, 2].Value = S_Desde;
            //ws.Cells[2, 1].Value = "Hasta:";
            //ws.Cells[2, 2].Value = S_Hasta;

            dataColumn = 1;
            dataRow = 1;
            #region 'Escritura de Datos'

            #region 'Cabecera'
            foreach (var subitem in HojaDatos)
            {
                ws.Cells[dataRow, dataColumn].Value = subitem;
                dataColumn++;
            }
            #endregion

            dataRow++;

            #region 'Resultados'       
            foreach (DataRow row in dt.Rows)
            {
                var i = 0;
                while (i < dt.Columns.Count)
                {
                    ws.Cells[dataRow, i + 1].Value = row[i];
                    i++;
                }
                dataRow++;
            }
            #endregion

            #endregion

            #region 'Formateo de Celdas'           
            using (ExcelRange subrng = ws.Cells[1, 1, 1, HojaDatos.Count])
            {
                subrng.Style.Font.Bold = true;
                subrng.Style.Font.Size = 10;
                subrng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                subrng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                subrng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                subrng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                subrng.AutoFitColumns();
                subrng.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#009FE0"));
                subrng.Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
            }
            using (ExcelRange subrng = ws.Cells[2, 1, dt.Rows.Count + 1, HojaDatos.Count])
            {
                subrng.Style.Font.Size = 10;
                subrng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                subrng.AutoFitColumns();
            }
            #endregion

            // ws.Column(1).Width = 55;
        }

        public string ObtenerNombre(string S_nombreoriginal)
        {
            string valor = "";
            switch (S_nombreoriginal)
            {
                case "N_IDVacante":
                    valor = "ID";
                    break;
                case "S_Categoria":
                    valor = "Categoría";
                    break;
                case "S_Especialidad":
                    valor = "Especialidad";
                    break;
                case "S_PostulaEn":
                    valor = "Vacante";
                    break;
                case "S_FechaRegistro":
                    valor = "Fecha Registro";
                    break;
                case "S_NivelAcademico":
                    valor = "Nivel Académico";
                    break;
                case "S_Estado":
                    valor = "Estado";
                    break;
            }

            if (string.IsNullOrEmpty(valor))
            {
                valor = S_nombreoriginal;
            }
            return valor;
        }
    }
}