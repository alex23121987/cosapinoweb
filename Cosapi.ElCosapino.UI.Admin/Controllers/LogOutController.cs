using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOutClient()
        {
            FormsAuthentication.SignOut();
            VMDatosUsuario.LogOut();
            var UrlAdmin = ConfigurationManager.AppSettings["UrlAdmin"];
            return Redirect(UrlAdmin);
        }
    }
}