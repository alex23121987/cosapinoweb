using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web.Mvc;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.Aplicacion.Security.BaseService;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Cosapi.ElCosapino.UI.Admin.Util;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class UsuarioController : BaseController
    {
        // GET: Usuario
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        readonly IBaseAppService _BaseService = new BaseAppService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<Usuario>>(arg);

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
           
            modelData.Data = _UsuarioService.List_Usuario_Paginate(paginateParams);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }    

        [HttpPost]
        public ActionResult Delete(int CodParameter)
        {
            var result = new Resultado();
            try
            {
                var UsuarioDelete = new Usuario
                {
                    UsuarioId = CodParameter
                };
                var Resultado = _UsuarioService.Delete(UsuarioDelete);
                result.Codigo = Resultado.UsuarioId;
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
        public ActionResult EnviarCorreoPendiente()
        {
            int Resultado = 0;
            var paginateParams = new PaginateParams
            {
                IsPaginate = false,PageIndex =0,RowsPerPage =0,SortColumn = "",SortDirection = ""
            };

            List<Usuario> _ListUsuario= _UsuarioService.List_Usuario_Paginate(paginateParams).Where(x=> x.Estado=="Registrado").ToList();

            foreach (var item in _ListUsuario)
            {
                try
                {
                    if (item.UsuarioId != 0)
                    {
                        var emailLogic = new EmailLogic();
                        emailLogic.SendEmailVerificacionCorreo(item.UsuarioId);
                    }
                }
                catch (Exception ex)
                {
                    //result.EsExito = false;
                    //result.Mensaje = ex.Message;
                    //return View();
                }
                Resultado = 1;
            }

            if (Resultado == 0)
            {
                Resultado = 2;
            }
            return new JsonResult()
            {
                Data = new { Codigo = Resultado }
            };
        }

        [HttpPost]
        public ActionResult GenerarPlantillaReporte()
        {
            var buffer = DescargarReporteProcesado();
            string handle = "Reporte_Usuario";
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
            
            DataTable dt = _UsuarioService.DT_Usuario();
           

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
            using (ExcelRange subrng = ws.Cells[2, 1, dt.Rows.Count+1, HojaDatos.Count])
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
                case "S_CorreoElectronico":
                    valor = "Correo Electrónico";
                    break;
                case "S_DNI":
                    valor = "DNI";
                    break;
                case "S_Nombres":
                    valor = "Nombres";
                    break;
                case "S_ApellidoPaterno":
                    valor = "Apellido Paterno";
                    break;
                case "S_ApellidoMaterno":
                    valor = "Apellido Materno";
                    break;
                case "S_FechaNacimiento":
                    valor = "Fecha Nacimiento";
                    break;
                case "S_NumeroContacto1":
                    valor = "Número Contacto1";
                    break;
                case "S_NumeroContacto2":
                    valor = "Número Contacto2";
                    break;
                case "S_EsTrabajador":
                    valor = "¿Es Trabajador?";
                    break;
                case "S_TieneEstudios":
                    valor = "¿Tiene Estudios?";
                    break;
                case "S_Especialidad":
                    valor = "Especialidad";
                    break;
                case "S_Categoria":
                    valor = "Categoría";
                    break;
                case "S_Departamento":
                    valor = "Departamento";
                    break;
                case "S_Provincia":
                    valor = "Provincia";
                    break;
                case "S_Distrito":
                    valor = "Distrito";
                    break;
                case "S_Estado":
                    valor = "Estado";
                    break;
                case "S_Origen":
                    valor = "Origen";
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