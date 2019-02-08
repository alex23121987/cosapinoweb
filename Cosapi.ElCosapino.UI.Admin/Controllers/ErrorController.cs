using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            var userlogin = currentUser.Name;
            var dominio = userlogin.Split('\\')[0];
            var user = userlogin.Split('\\')[1];

            return Redirect(string.Format("{0}", Url.Action("Index?dominio="+ dominio+"&user="+ user, "New")));
            //return View("~/Views/Error/General.cshtml");
        }

        public ActionResult NotFound()
        {
            return View("~/Views/Error/NotFound.cshtml");
        }

        public ActionResult General()
        {
            return View("~/Views/Error/General.cshtml");
        }

        public ActionResult BadRequest()
        {
            return View("~/Views/Error/BadRequest.cshtml");
        }
    }
}