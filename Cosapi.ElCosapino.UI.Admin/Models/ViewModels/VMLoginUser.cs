using Cosapi.ElCosapino.CrossCutting.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMLoginUser
    {
        [Required(ErrorMessage = "Ingrese el Nombre de usuario")]
        [Display(Name = "Nombre de usuario:")]
        public string UserAlias { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ingrese la contraseña")]
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }
    }

    public static class VMDatosUsuario
    {
        public static void SetValueLogin(string userAlias, string userName, string userRole, byte[] b_photo, int id)
        {
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERALIAS, userAlias);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERNAME, userName);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERROLE, userRole);
           // HttpContext.Current.Session.Add(Constantes.SessionKey.USERPHOTO, b_photo);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERID, id);
        }

        public static void LogOut()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static string GetUserAlias()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERALIAS] != null)
                return HttpContext.Current.Session[Constantes.SessionKey.USERALIAS].ToString();
            return null;
        }

        public static string GetUserName()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERNAME] != null)
                return HttpContext.Current.Session[Constantes.SessionKey.USERNAME].ToString();
            return null;
        }

        public static byte[] GetUserPhoto()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERPHOTO] != null)
                return (byte[])HttpContext.Current.Session[Constantes.SessionKey.USERPHOTO];
            return null;
        }

        public static string GetUUserRole()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERROLE] != null)
                return HttpContext.Current.Session[Constantes.SessionKey.USERROLE].ToString();
            return null;
        }

        public static int GetUUserId()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERID] != null)
                return (int)HttpContext.Current.Session[Constantes.SessionKey.USERID];
            return -1;
        }

        public static int GetUUserIdUnidad()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERIDUNIDAD] != null)
                return (int)HttpContext.Current.Session[Constantes.SessionKey.USERIDUNIDAD];
            return -1;
        }
    }
}