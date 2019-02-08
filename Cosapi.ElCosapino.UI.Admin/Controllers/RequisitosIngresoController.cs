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
    public class RequisitosIngresoController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModelAdmin model = new PageViewModelAdmin();
        public ActionResult Index()
        {
            return View();
        }

        #region SeccionImagenes
        public ActionResult IndexPaginateSeccionImagenes(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<InterfaceWeb>>(arg);

            var paginateParams = new PaginateParams
            {
                IsPaginate = true,
                PageIndex = modelData.CurrentPageIndex,
                RowsPerPage = 2,
                SortColumn = modelData.OrderBy,
                SortDirection = modelData.DirectionOrder
            };
            if (modelData.Filters != null)
                paginateParams.Filters = Converter.ListaToDatatable<CanvasFilter>(modelData.Filters.ToList());

            modelData.Data = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, ModulosWeb.RequisitosIngreso, ModulosSeccionWeb.RequisitosIngreso_ImagenesLaterales);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGridSeccionImagenes", modelData);
        }

        [HttpPost]
        public ActionResult Insert_SeccionImagenes()
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
                            fname = Path.Combine(Server.MapPath("~/Upload/RequisitosIngreso/ImagenesLaterales/"), myfile);
                            file.SaveAs(fname);

                            InterfaceWeb entidad = new InterfaceWeb
                            {
                                IDModulo = ModulosWeb.RequisitosIngreso,
                                IDSeccion = ModulosSeccionWeb.RequisitosIngreso_ImagenesLaterales,
                                Titulo = "",
                                SubTitulo = "",
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
                    result.Codigo = -1;
                }
            }
            else
            {
                result.Codigo = -2;
            }

            return Json(result);
        }        
        #endregion

        #region SeccionRequisitos
        public ActionResult IndexPaginateSeccionRequisitos(string arg)
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

            modelData.Data = _InterfaceWebService.List_InterfaceWeb_Paginate(paginateParams, ModulosWeb.RequisitosIngreso, ModulosSeccionWeb.RequisitosIngreso_RequisitosIngreso);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGridSeccionRequisitos", modelData);
        }

        [HttpPost]
        public ActionResult InsertSeccionRequisitos(VMInterfaceWeb model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.InterfaceWeb.Titulo))
                {
                    return View(model);
                }
                model.InterfaceWeb.IDModulo = ModulosWeb.RequisitosIngreso;
                model.InterfaceWeb.IDSeccion = ModulosSeccionWeb.RequisitosIngreso_RequisitosIngreso;
                model.InterfaceWeb.UsuarioCreacion = VMDatosUsuario.GetUserAlias();
                model.InterfaceWeb.Estado = "A";
                var Resultado = _InterfaceWebService.Insert(model.InterfaceWeb);
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Upd_SeccionRequisitos()
        {
            var result = new Resultado
            {
                EsExito = true
            };

            if (result.EsExito)
            {
                int Param_IDInterface = Convert.ToInt32(Request.Params["IDInterface"].ToString());
                string Param_Titulo = Request.Params["Titulo"].ToString();
                string Param_SubTitulo = Request.Params["SubTitulo"].ToString();
                string Param_Estado = Request.Params["Estado"].ToString();
                int Param_Orden = Convert.ToInt32(Request.Params["Orden"].ToString());

                InterfaceWeb entidad = new InterfaceWeb
                {
                    IDInterfaceWeb = Param_IDInterface,
                    Titulo = Param_Titulo,
                    SubTitulo = Param_SubTitulo,
                    UsuarioModificacion = VMDatosUsuario.GetUserAlias(),
                    Imagen = "",
                    Orden= Param_Orden,
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
        public ActionResult InsertUnique(VMInterfaceWeb model)
        {
            var result = new Resultado();
            try
            {
                if (model.InterfaceWeb.IDModulo == 0 || model.InterfaceWeb.IDSeccion == 0)
                {
                    return View(model);
                }
                model.InterfaceWeb.UsuarioCreacion = VMDatosUsuario.GetUserAlias();
                model.InterfaceWeb.Estado = "A";
                var Resultado = _InterfaceWebService.InsertUnique(model.InterfaceWeb);
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