using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.PostulacionService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class PostulacionController : BaseController
    {                
        readonly IPostulacionAppService _PostulacionService = new PostulacionAppService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<PostulacionBE>>(arg);

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

            modelData.Data = _PostulacionService.List_Postulacion_Paginate(paginateParams);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult GenerarPlantillaReporte(string S_Desde, string S_Hasta)
        {
            var buffer = DescargarReporteProcesado(S_Desde, S_Hasta);
            string handle = "Reporte_Postulación";
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

        public byte[] DescargarReporteProcesado(string S_Desde, string S_Hasta)
        {
            byte[] resultadoProceso;
            Dictionary<string, List<string>> listHeader = new Dictionary<string, List<string>>();

           
            DataTable dt = _PostulacionService.DT_Postulacion(S_Desde, S_Hasta);
            //var listaReporteUsuarioHH = _ReporteEstadoHHService.List_ReporteEstadoUsuario_HH(S_Desde, S_Hasta);


            List<string> HojaDatos = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                HojaDatos.Add(ObtenerNombre(col.ColumnName));
            }
            listHeader.Add("HojaDatos", HojaDatos);

            //Datos para los registros               
            resultadoProceso = CrearArchivoExcel(dt, S_Desde, S_Hasta, listHeader);

            return resultadoProceso;
        }

        public static byte[] CrearArchivoExcel(DataTable dt, string S_Desde, string S_Hasta, Dictionary<string, List<string>> listHeader = null)
        {
            byte[] bytes;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet wsDatos = pck.Workbook.Worksheets.Add("Datos");

                ImprimirHojaDatos(dt, wsDatos, S_Desde, S_Hasta, listHeader);

                bytes = pck.GetAsByteArray();
            }
            return bytes;
        }

        public static int dataRow = 1;
        public static int dataColumn = 1;

        private static void ImprimirHojaDatos(DataTable dt, ExcelWorksheet ws, string S_Desde, string S_Hasta, Dictionary<string, List<string>> listHeader)
        {
            List<string> HojaDatos = listHeader["HojaDatos"];

            ws.Cells[1, 1].Value = "Desde:";
            ws.Cells[1, 2].Value = S_Desde;
            ws.Cells[2, 1].Value = "Hasta:";
            ws.Cells[2, 2].Value = S_Hasta;

            dataColumn = 1;
            dataRow = 4;
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
            using (ExcelRange subrng = ws.Cells[1, 1, 2, 1])
            {
                subrng.Style.Font.Bold = true;
                subrng.Style.Font.Size = 10;
                subrng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                subrng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                subrng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                subrng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                subrng.AutoFitColumns();
                subrng.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#009FE0"));
                subrng.Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
            }

            using (ExcelRange subrng = ws.Cells[4, 1, 4, HojaDatos.Count])
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

            using (ExcelRange subrng = ws.Cells[5, 1, dt.Rows.Count + 4, HojaDatos.Count])
            {
                subrng.Style.Font.Size = 10;
                subrng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                subrng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                subrng.AutoFitColumns();
            }
            #endregion

            ws.Column(1).Width = 25;
        }

        public string ObtenerNombre(string S_nombreoriginal)
        {
            string valor = "";
            switch (S_nombreoriginal)
            {
                case "S_FechaRegistro":
                    valor = "Fecha Postulación";
                    break;
                case "N_IDPostulacion":
                    valor = "ID";
                    break;              
                case "S_Categoria":
                    valor = "Categoría";
                    break;               
                case "S_Especialidad":
                    valor = "Especialidad";
                    break;             
                case "S_Requisitos":
                    valor = "Requisitos";
                    break;
                case "S_DNI":
                    valor = "DNI";
                    break;
                case "S_NombreCompleto":
                    valor = "Usuario";
                    break;
                case "S_Categoria_Postulante":
                    valor = "Categoría Postulante";
                    break;
                case "S_Especialidad_Postulante":
                    valor = "Especialidad Postulante";
                    break;
                case "S_NivelAcademico":
                    valor = "Nivel Académico";
                    break;
                case "S_PostulaEn":
                    valor = "Postula En";
                    break;
                case "S_CorreoElectronico":
                    valor = "Correo Electrónico";
                    break;
                case "S_NumeroContacto1":
                    valor = "Número Contacto";
                    break;
                case "S_NumeroContacto2":
                    valor = "Número Contacto 2";
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