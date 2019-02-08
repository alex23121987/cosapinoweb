using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.UI.Admin.Models;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Cosapi.ElCosapino.CrossCutting.Util.Constantes;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class PieController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModelAdmin model = new PageViewModelAdmin();

        public ActionResult Index()
        {
            return View();
        }

        #region SeccionPie
        public ActionResult IndexPaginateSeccionPie(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<InterfaceWeb>>(arg);

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

            modelData.Data = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, ModulosWeb.Footer, ModulosSeccionWeb.Footer_RedesSociales);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGridPie", modelData);
        }

        [HttpPost]
        public ActionResult Insert_Pie()
        {
            var allowedExtensions = new[] { ".JPG", ".PNG", ".GIF", ".JPEG" };
            string myfile = "";
            var result = new Resultado
            {
                EsExito = true
            };

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var ext = Path.GetExtension(fname);

                        if (allowedExtensions.Contains(ext.ToUpper()))
                        {
                            if (file.ContentLength > (1024 * 1024))
                            {
                                //Restriccion de Tamaño
                                result.EsExito = false;
                                result.Codigo = -3;
                                return Json(result);
                            }

                            myfile = Guid.NewGuid().ToString() + ext;
                            fname = Path.Combine(Server.MapPath("~/Upload/Pie/Iconos/"), myfile);
                            file.SaveAs(fname);

                            string Param_Titulo = Request.Params["Titulo"].ToString();
                            string Param_SubTitulo = "";



                            InterfaceWeb entidad = new InterfaceWeb
                            {
                                IDModulo = ModulosWeb.Footer,
                                IDSeccion = ModulosSeccionWeb.Footer_RedesSociales,
                                Titulo = Param_Titulo,
                                SubTitulo = Param_SubTitulo,
                                UsuarioCreacion = VMDatosUsuario.GetUserAlias(),
                                Imagen = myfile,
                                Estado = "A"
                            };

                            var Resultado = _InterfaceWebService.Insert(entidad);
                            result.Codigo = Resultado.IDInterfaceWeb;
                        }
                        else
                        {
                            //Restriccion de Extensión
                            result.EsExito = false;
                            result.Codigo = -1;
                            return Json(result);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    result.EsExito = false;
                    result.Mensaje = ex.Message;
                    result.Codigo = -99;
                }
            }
            else
            {
                result.Codigo = -2;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult Upd_SeccionPie()
        {
            var allowedExtensions = new[] { ".JPG", ".PNG", ".GIF", ".JPEG" };
            string myfile = "";
            var result = new Resultado
            {
                EsExito = true
            };

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        var ext = Path.GetExtension(fname);
                        if (allowedExtensions.Contains(ext.ToUpper()))
                        {
                            if (file.ContentLength > (1024 * 1024))
                            {
                                //Restriccion de Tamaño
                                result.EsExito = false;
                                result.Codigo = -3;
                                return Json(result);
                            }

                            myfile = Guid.NewGuid().ToString() + ext;
                            fname = Path.Combine(Server.MapPath("~/Upload/Pie/Iconos/"), myfile);
                            file.SaveAs(fname);
                        }
                        else
                        {
                            //Restriccion de Extensión
                            result.EsExito = false;
                            result.Codigo = -1;
                            return Json(result);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    result.EsExito = false;
                    result.Mensaje = ex.Message;
                    result.Codigo = -99;
                }
            }

            if (result.EsExito)
            {
                int Param_IDInterface = Convert.ToInt32(Request.Params["IDInterface"].ToString());
                string Param_Titulo = Request.Params["Titulo"].ToString();
                string Param_SubTitulo = "";
                string Param_Estado = Request.Params["Estado"].ToString();

                InterfaceWeb entidad = new InterfaceWeb
                {
                    IDInterfaceWeb = Param_IDInterface,
                    Titulo = Param_Titulo,
                    SubTitulo = Param_SubTitulo,
                    UsuarioModificacion = VMDatosUsuario.GetUserAlias(),
                    Imagen = myfile,
                    Estado = Param_Estado.ToUpper().Equals("TRUE") ? "A" : "I"
                };

                var Resultado = _InterfaceWebService.Update(entidad);
                result.Codigo = Resultado.IDInterfaceWeb;
            }
            return Json(result);
        }
        #endregion

        #region Eventos
        [HttpPost]
        public ActionResult Delete(int CodParameter)
        {
            var result = new Resultado();
            try
            {
                var ParametroGeneralDelete = new InterfaceWeb
                {
                    IDInterfaceWeb = CodParameter,
                    UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _InterfaceWebService.Delete(ParametroGeneralDelete);
                result.Codigo = Resultado.IDInterfaceWeb;
            }
            catch (System.Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
                return View();
            }
            return Json(result);
        }      
        #endregion
    }
}