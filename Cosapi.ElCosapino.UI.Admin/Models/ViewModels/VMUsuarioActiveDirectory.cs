using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{  
    public class VMUsuarioActiveDirectory : VMLogin
    {
        //[Display(Name = "Dominio")]
        //[Required(ErrorMessage = "Ingrese el dominio del usuario")]
        public string UserDomain { get; set; }

        public string Message { get; set; }
    }

    public class VMLoginForm : VMLogin
    {

    }

    public class VMLogin
    {
        //[Display(Name = "Usuario")]
        //[Required(ErrorMessage = "Ingrese el nombre de usuario")]
        public string UserAlias { get; set; }

        //[Display(Name = "Contraseña")]
        //[Required(ErrorMessage = "Ingrese la contraseña")]
        public string UserPassWord { get; set; }

        //[Display(Name = "Impersonar")]
        //[Required(ErrorMessage = "Ingrese usuario impersonar")]
        public string Impersonar { get; set; }
    }
}