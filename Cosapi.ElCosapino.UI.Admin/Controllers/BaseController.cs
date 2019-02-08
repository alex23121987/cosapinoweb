using Cosapi.ElCosapino.UI.Admin.Helpers;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    // Esto asegurará que cada solicitud posterior de esta página se traducirá en 
    // un nuevo viaje al servidor y una copia nueva que está cumpliendo.
    //TODO: CAMBIAR CUANDO SE UTILICE LA AUTENTICACIÓN

    //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.RemoveOutputCacheItem(Request.Url.AbsolutePath);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            base.OnActionExecuting(filterContext);
        }
        //
        // GET: /Base/
        public List<VMLanguage> Languages;

        public List<VMLanguage> GetLanguages()
        {
            var languages = new List<VMLanguage>
            {
            new VMLanguage { code = "en-US", description = "" },
            new VMLanguage { code = "es-PE", description = "" },
            new VMLanguage { code = "pt-BR", description = "" }
        };
            return languages;
        }
        public List<VMLanguage> SetLanguage()
        {
            var languages = GetLanguages();

            var languageSelected = (Session["LanguageSelected"] != null) ? languages.FirstOrDefault(y => y.code == ((VMLanguage)Session["LanguageSelected"]).code) : languages.FirstOrDefault(x => x.code == "en-US");

            ViewBag.LanguageSelected = languageSelected.description;
            return languages;
        }
        public ActionResult ChangeCulture(string lang)
        {
            Session["Culture"] = new CultureInfo(lang);
            var languageSelected = GetLanguages().FirstOrDefault(x => x.code.ToUpper() == lang.ToUpper());
            Session["LanguageSelected"] = languageSelected;
            return Redirect(Request.UrlReferrer.ToString());
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
    }
}