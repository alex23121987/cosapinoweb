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
    public class NoticiasController : Controller
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        private PageViewModelAdmin model = new PageViewModelAdmin();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginateSeccionNoticias(string arg)
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

            modelData.Data = _InterfaceWebService.List_InterfaceWebNoticia_Paginate(paginateParams, ModulosWeb.Noticias, ModulosSeccionWeb.Noticias_Noticias);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGridNoticias", modelData);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Insert_SeccionNoticias()
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
                            fname = Path.Combine(Server.MapPath("~/Upload/Noticias/ListaNoticias/"), myfile);
                            file.SaveAs(fname);

                            string Param_Titulo = Request.Params["Titulo"].ToString();
                            string Param_SubTitulo = Request.Params["SubTitulo"].ToString();
                            string Param_FechaPublicacion = Request.Params["FechaPublicacion"].ToString();
                            int Param_Destacado = Convert.ToInt32( Request.Params["Destacado"].ToString());

                            InterfaceWeb entidad = new InterfaceWeb
                            {
                                IDModulo = ModulosWeb.Noticias,
                                IDSeccion = ModulosSeccionWeb.Noticias_Noticias,
                                Titulo = Param_Titulo,
                                SubTitulo = Param_SubTitulo,
                                FechaPublicacion= Param_FechaPublicacion,
                                Destacado= Param_Destacado,
                                UsuarioCreacion = VMDatosUsuario.GetUserAlias(),
                                Imagen = myfile,
                                Estado = "A"
                            };

                            var Resultado = _InterfaceWebService.InsertNoticia(entidad);
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Upd_SeccionNoticias()
        {
            var allowedExtensions = new[] { ".JPG", ".PNG", ".GIF", ".JPEG" };
            string myfile = "";
            var result = new Resultado
            {
                EsExito = true
            };

            try { 
                if (Request.Files.Count > 0)
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
                            fname = Path.Combine(Server.MapPath("~/Upload/CosapiEnBreve/Conocer/"), myfile);
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

                if (result.EsExito)
                {
                    int Param_IDInterface = Convert.ToInt32(Request.Params["IDInterface"].ToString());
                    string Param_Titulo = Request.Params["Titulo"].ToString();
                    string Param_SubTitulo = Request.Params["SubTitulo"].ToString();
                    string Param_Estado = Request.Params["Estado"].ToString();
                    string Param_FechaPublicacion = Request.Params["FechaPublicacion"].ToString();
                    int Param_Destacado = Convert.ToInt32(Request.Params["Destacado"].ToString());

                    InterfaceWeb entidad = new InterfaceWeb
                    {
                        IDInterfaceWeb = Param_IDInterface,
                        Titulo = Param_Titulo,
                        SubTitulo = Param_SubTitulo,
                        FechaPublicacion = Param_FechaPublicacion,
                        Destacado = Param_Destacado,
                        UsuarioModificacion = VMDatosUsuario.GetUserAlias(),
                        Imagen = myfile,
                        Estado = Param_Estado.ToUpper().Equals("TRUE") ? "A" : "I"
                    };

                    var Resultado = _InterfaceWebService.UpdateNoticia(entidad);
                    result.Codigo = Resultado.IDInterfaceWeb;
                }
            }
            catch (System.Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
                result.Codigo = -1;
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
                var Resultado = _InterfaceWebService.DeleteNoticia(ParametroGeneralDelete);
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
    }
}