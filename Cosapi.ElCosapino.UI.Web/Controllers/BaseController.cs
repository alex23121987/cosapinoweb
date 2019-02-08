using Cosapi.ElCosapino.UI.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public void InvalidarContext()
        {
        }

        public void PostMessage(FlashMessage Message)
        {
            if (TempData["FlashMessages"] == null)
                TempData["FlashMessages"] = new List<FlashMessage>();

            var list = ((List<FlashMessage>)TempData["FlashMessages"]);
            list.Add(Message);
        }

        public void PostMessage(MessageType Type, Boolean RemoveOnError = false)
        {
            String Body = "";

            switch (Type)
            {
                case MessageType.Error: Body = "Ha ocurrido un error al procesar la solicitud."; break;
                case MessageType.Info: Body = ""; break;
                case MessageType.Success: Body = "Los datos se guardaron exitosamente."; break;
                case MessageType.Warning: Body = "."; break;
            }
            PostMessage(Type, Body, RemoveOnError);
        }

        public void PostMessage(Exception ex, Boolean RemoveOnError = false)
        {
            PostMessage(MessageType.Error, "Ha ocurrido un error al procesar la solicitud: " + ex.Message.ToString(), RemoveOnError);
        }

        public void PostMessage(MessageType Type, String Title, String Body, Boolean RemoveOnError = false)
        {
            PostMessage(new FlashMessage { Title = Title, Body = Body, Type = Type, RemoveOnError = RemoveOnError });
        }

        public void PostMessage(MessageType Type, String Body, Boolean RemoveOnError = false)
        {
            String Title = "";

            switch (Type)
            {
                case MessageType.Error: Title = "¡Error!"; break;
                case MessageType.Info: Title = "Ojo."; break;
                case MessageType.Success: Title = "¡Éxito!"; break;
                case MessageType.Warning: Title = "¡Atención!"; break;
            }

            PostMessage(new FlashMessage { Title = Title, Body = Body, Type = Type, RemoveOnError = RemoveOnError });
        }

        public ActionResult Error(Exception ex)
        {
            return View("Error", ex);
        }

        public ActionResult RedirectToActionPartialView(String actionName)
        {
            return RedirectToActionPartialView(actionName, null, null);
        }

        public ActionResult RedirectToActionPartialView(String actionName, object routeValues)
        {
            return RedirectToActionPartialView(actionName, null, routeValues);
        }

        public ActionResult RedirectToActionPartialView(String actionName, String controllerName)
        {
            return RedirectToActionPartialView(actionName, controllerName, null);
        }

        public ActionResult RedirectToActionPartialView(String actionName, String controllerName, object routeValues)
        {
            var url = Url.Action(actionName, controllerName, routeValues);
            return Content("<script> window.location = '" + url + "'</script>");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.Params == filterContext.ActionParameters)
            {
            }

            base.OnActionExecuting(filterContext);
        }
    }
}