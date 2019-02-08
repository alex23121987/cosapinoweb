using Canvas.CustomHtmlHelpers.HtmlGenericGrid;
using Cosapi.ElCosapino.Aplicacion.Security.NotificacionService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.UI.Admin.Helpers;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using Cosapi.ElCosapino.UI.Admin.TransferObject.Json;
using Cosapi.ElCosapino.UI.Admin.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class NotificacionController : BaseController
    {
        readonly INotificacionAppService _NotificacionService = new NotificacionAppService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPaginate(string arg,int CategoriaId, int EspecialidadId, string filtro)
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

            Usuario _Filtro = new Usuario
            {
                CategoriaId= CategoriaId,
                EspecialidadId= EspecialidadId,
                Filtro= filtro
            };

            modelData.Data = _NotificacionService.List_Notificacion_Paginate(paginateParams, _Filtro);
            modelData.TotalRows = paginateParams.TotalRows;

            return PartialView("_IndexGrid", modelData);
        }

        [HttpPost]
        public ActionResult Insert(VMNotificacion model)
        {
            var result = new Resultado();
            try
            {
                if (string.IsNullOrEmpty(model.Notificacion.Titulo))
                {
                    return View(model);
                }

                var paginateParams = new PaginateParams
                {
                    IsPaginate = false,PageIndex = 0,RowsPerPage = 0,SortColumn ="",SortDirection = ""
                };              

                Usuario _Filtro = new Usuario
                {
                    CategoriaId = model.Usuario.CategoriaId,
                    EspecialidadId = model.Usuario.EspecialidadId,
                    Filtro = !string.IsNullOrEmpty(model.Usuario.Filtro)? model.Usuario.Filtro : ""
                };

                List<Usuario> listaUsuarios= _NotificacionService.List_Notificacion_Paginate(paginateParams, _Filtro);

                List<String> listDevice = new List<String>();
                
                foreach (var item in listaUsuarios.Where(x=>x.DeviceToken!=""))
                {                    
                    listDevice.Add(item.DeviceToken);
                }

                List<String> listEmail = new List<String>();
                foreach (var item in listaUsuarios.Where(x => x.CorreoElectronico != ""))
                {
                    listEmail.Add(item.CorreoElectronico);
                }

               
                if (model.Notificacion.EnviarEmail == 1)
                {
                    try
                    {                            
                        var emailLogic = new EmailLogic();
                        emailLogic.SendNotificaion(listEmail, model.Notificacion.Titulo, model.Notificacion.Subtitulo);
                        PostMessage(MessageType.Success, "Correo Electronico enviado satisfactoriamente");
                    }
                    catch (Exception ex)
                    {
                        PostMessage(MessageType.Info, "No se pudo realizar el envio de los correos.");
                    }
                }

                if (model.Notificacion.EnviarCelular == 1)
                {                                                                      
                    var fireBaseNotification = new FirebaseLogic();
                    var response2 = fireBaseNotification.EnviarNotificacionMBG(listDevice, model.Notificacion.Titulo, model.Notificacion.Subtitulo, String.Empty, String.Empty);
                   // var response2 = fireBaseNotification.SendNotification(listDevice, model.Notificacion.Titulo, model.Notificacion.Subtitulo, String.Empty, String.Empty);

                    PostMessage(MessageType.Success, "Se enviaron notificaciones a " + response2.success + " usuarios.");                    
                }                

                var Resultado = _NotificacionService.Insert(model.Notificacion);
                result.Codigo = Resultado.IDNotificacion;             
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