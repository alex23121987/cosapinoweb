using Cosapi.ElCosapino.CrossCutting.Util;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Cosapi.ElCosapino.UI.Web.Models.ViewModels
{
    public class VMLoginUserExterno
    {
        [Required(ErrorMessage = "Ingrese el Nombre de usuario")]
        [Display(Name = "Nombre de usuario:")]
        public string UserAlias { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ingrese la contraseña")]
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }
    }

    public static class VMDatosUsuarioExterno
    {
        public static void SetValueLogin(string userAlias, string userName, string userRole, byte[] b_photo, string id,string estado,string dni)
        {
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERALIAS, userAlias);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERNAME, userName);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERROLE, userRole);
            // HttpContext.Current.Session.Add(Constantes.SessionKey.USERPHOTO, b_photo);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERID, id);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERESTADO, estado);
            HttpContext.Current.Session.Add(Constantes.SessionKey.USERDNI, dni);
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

        public static string GetUserDNI()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERDNI] != null)
                return HttpContext.Current.Session[Constantes.SessionKey.USERDNI].ToString();
            return null;
        }

        public static void SetUserDNI(string DNI)
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERDNI] != null)
            {
                HttpContext.Current.Session[Constantes.SessionKey.USERDNI] = DNI;
            }
        }

        public static void SetUserEstado(string Estado)
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERESTADO] != null)
            {
                HttpContext.Current.Session[Constantes.SessionKey.USERESTADO] = Estado;
            }
        }

        public static string GetUserEstado()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERESTADO] != null)
                return HttpContext.Current.Session[Constantes.SessionKey.USERESTADO].ToString();
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

        public static string GetUUserId()
        {
            if (HttpContext.Current.Session[Constantes.SessionKey.USERID] != null)
                return HttpContext.Current.Session[Constantes.SessionKey.USERID].ToString();
            return "-1";
        }    
    }
}