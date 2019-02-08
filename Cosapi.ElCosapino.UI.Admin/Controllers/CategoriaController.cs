using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.CategoriaService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class CategoriaController : BaseController
    {        
        readonly ICategoriaAppService _CategoriaService = new CategoriaAppService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<Categoria>>(arg);

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
            
            modelData.Data = _CategoriaService.List_Categoria_Paginate(paginateParams);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult Insert(VMCategoria model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Categoria.Descripcion))
                {
                    return View(model);
                }                
                model.Categoria.UsuarioCreacion = VMDatosUsuario.GetUserAlias();
                model.Categoria.Estado = "ACT";
                var Resultado = _CategoriaService.Insert(model.Categoria);
                result.Codigo = Resultado.IDCategoria;
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
        public ActionResult Update(VMCategoria model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Categoria.Descripcion))
                {
                    return View(model);
                }

                model.Categoria.UsuarioModificacion = VMDatosUsuario.GetUserAlias();
                model.Categoria.Estado = model.Categoria.Estado.Equals("True") ? "ACT" : "INA";
                var Resultado = _CategoriaService.Update(model.Categoria);
                result.Codigo = Resultado.IDCategoria;
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
                var CategoriaDelete = new Categoria
                {
                    IDCategoria = CodParameter,
                    UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _CategoriaService.Delete(CategoriaDelete);
                result.Codigo = Resultado.IDCategoria;
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