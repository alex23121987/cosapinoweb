using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.RolService;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.RolAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PermissionAgg;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.UI.Admin.Models;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class RolController : BaseController
    {
        // GET: Rol
        readonly RolAppService _RolService = new RolAppService();                

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg)
        {
            var modelData = JsonConvert.DeserializeObject<CanvasGridModel<Rol>>(arg);

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

            modelData.Data = _RolService.List_Rol_Paginate(paginateParams);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult Insert(VMRol model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Rol.S_Descripcion))
                {
                    return View(model);
                }

                model.Rol.S_UsuarioCreacion = VMDatosUsuario.GetUserAlias();
                model.Rol.S_Estado = "A";
                var Resultado = _RolService.Insert(model.Rol);
                result.Codigo = Resultado.N_IDRol;
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
        public ActionResult Update(VMRol model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Rol.S_Descripcion))
                {
                    return View(model);
                }

                model.Rol.S_UsuarioModificacion = VMDatosUsuario.GetUserAlias();
                model.Rol.S_Estado = model.Rol.S_Estado.Equals("True") ? "A" : "I";
                var Resultado = _RolService.Update(model.Rol);
                result.Codigo = Resultado.N_IDRol;
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
                var RolDelete = new Rol
                {
                    N_IDRol = CodParameter,
                    S_UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _RolService.Delete(RolDelete);
                result.Codigo = Resultado.N_IDRol;
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
        public ActionResult Desactivar(int CodParameter)
        {
            var result = new Resultado();
            try
            {
                var RolDelete = new Rol
                {
                    N_IDRol = CodParameter,
                    S_UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _RolService.Desactivar(RolDelete);
                result.Codigo = Resultado.N_IDRol;
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
                var RolActivar = new Rol
                {
                    N_IDRol = CodParameter,
                    S_UsuarioModificacion = VMDatosUsuario.GetUserAlias()
                };
                var Resultado = _RolService.Activar(RolActivar);
                result.Codigo = Resultado.N_IDRol;
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
        public ActionResult BuscarTreeView(string S_IDRol)
        {
            var _VMRol = new VMRol();

            var model = GetTreeData(S_IDRol);
            var jsonModel = new JavaScriptSerializer().Serialize(model);

            _VMRol.javaSerial = jsonModel;

            return Json(model);
        }

        private JsTreeModel[] GetTreeData(string S_IDRol)
        {
            var tmpFinal = new List<Permission.ListPermissionTree>();

            List<Permission.ListPermissionTree> lstPermission = _RolService.List_Permission_Tree(S_IDRol);

            foreach (var item in lstPermission)
            {
                if (!item.S_ID_PARENT_RESULT.Equals(""))
                {
                    var padre = lstPermission.FirstOrDefault(i => i.S_ID_RESULT.Equals(item.S_ID_PARENT_RESULT));
                    padre.Hijos = padre.Hijos ?? new List<Permission.ListPermissionTree>();
                    padre.Hijos.Add(item);
                }
                else
                {
                    tmpFinal.Add(item);
                }
            }
            var tree1 = ArmarLista(tmpFinal);

            return tree1.ToArray();
        }

        public List<JsTreeModel> ArmarLista(List<Permission.ListPermissionTree> lista)
        {
            var tree_list = new List<JsTreeModel>();
            foreach (var item in lista)
            {

                var model = new JsTreeModel
                {
                    data = item.S_NAME_RESULT,
                    attr = new JsTreeAttribute { id = item.S_IDS, selected = (item.S_STATUS == Constantes.EstadosHardCode.Activo) }
                };

                if (item.Hijos != null)
                {
                    model.children = ArmarLista(item.Hijos).ToArray();
                }
                tree_list.Add(model);
            }

            return tree_list;
        }

        [HttpPost]
        public ActionResult SaveTree(string[] arrChecked, string[] arrUnChecked, string tsIdRol, string S_IDRol = null)
        {
            var result = new Resultado();
            var laIds = new string[4];
            try
            {                
                var strLstToInsert = new List<Permission>();               

                if (arrChecked != null)
                {
                    foreach (var item in arrChecked)
                    {                        
                        laIds = item.Split('_');
                        if (Convert.ToInt32(laIds[2]) > 0 && Convert.ToInt32(laIds[1]) > 0)
                        {                           
                            strLstToInsert.Add(new Permission
                            {
                                S_STATUS = Constantes.EstadosHardCode.Activo,
                                N_ID_ACTION = Convert.ToInt32(laIds[2]),
                                N_ID_OPTION = Convert.ToInt32(laIds[1]),                                   
                                S_USER_CREATION = "",                                  
                                S_IDRol = S_IDRol
                            });                            
                        }
                    }
                }                

                if (strLstToInsert.Count > 0)
                {
                    List<Permission> strLstToInsertResult = _RolService.PermissionTreeInsert(strLstToInsert);
                    if (strLstToInsertResult.Count > 0)
                    {
                        result.EsExito = true;
                        result.Mensaje = "Registros Insertados y/o actualizados correctamente.";
                    }
                }                
            }
            catch (Exception ex)
            {
                result.EsExito = false;
                result.Mensaje = ex.Message;
            }
            return Json(result);
        }
    }
}