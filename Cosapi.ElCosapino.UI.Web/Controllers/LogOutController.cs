using Cosapi.ElCosapino.UI.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cosapi.ElCosapino.UI.Web.Controllers
{   
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOutExterno()
        {
            FormsAuthentication.SignOut();
            VMDatosUsuarioExterno.LogOut();
            var UrlCosapino = ConfigurationManager.AppSettings["UrlCosapino"];
            return Redirect(UrlCosapino);
        }
    }
}