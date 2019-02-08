using Cosapi.ElCosapino.Aplicacion.Security.LoginService;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.UI.Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Web.Mvc;
using System.Web.Security;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class LoginController : Controller
    {
        readonly ILoginAppService _LoginService = new LoginAppService();
        readonly VMUsuario _VMUsuario = new VMUsuario();        

        #region Vistas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult Index(string msg)
        {
            if (!string.IsNullOrEmpty(msg) && msg == "LoginError")
            {
                ModelState.AddModelError("LoginError", "Usuario y/o contraseña incorrectos");
            }

            string ADAutomatico = ConfigurationManager.AppSettings["ADAutomatico"].ToString();

            if (ADAutomatico.Equals("S"))
            {
                //WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                //var userlogin = currentUser.Name;
                //var dominio = userlogin.Split('\\')[0];
                //var user = userlogin.Split('\\')[1];
                //if (dominio.ToUpper().Equals("COSAPI.COM.PE"))
                //{
                //    if (!string.IsNullOrEmpty(user))
                //    {
                //        var Resultado = _LoginService.Verificar_Usuario_Por_UserNameValidado(user);

                //        if (!string.IsNullOrEmpty(Resultado.UsuarioId.ToString()))
                //        {

                //            _VMUsuario.Usuario = Resultado;
                //            VMDatosUsuario.SetValueLogin(_VMUsuario.Usuario.S_UserName, _VMUsuario.Usuario.S_Nombres, _VMUsuario.Usuario.N_IDRol.ToString(), null, _VMUsuario.Usuario.N_IDUsuario);

                //            FormsAuthentication.SetAuthCookie(VMDatosUsuario.GetUserAlias(), false);
                //            List<PermisoUsuario> lstPermission = _LoginService.Listar_PermisosUsuario(_VMUsuario.Usuario.S_UserName);


                //            Session[Constantes.SessionKey.USERPERMISSIONS] = lstPermission;


                //            return Redirect(string.Format("{0}", Url.Action("Index", "Home")));
                //        }
                //        else
                //        {
                //            return Redirect(string.Format("{0}", Url.Action("IndexSinAcceso", "Login")));
                //        }
                //    }
                //}
            }
           
            return View();
        }

        public ActionResult IndexSinAcceso()
        {           
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// 
        public ActionResult AutenticateByAD(Login login)
        {
            return PartialView(login);
        }

        [HttpPost]      
        public ActionResult LoginUsuarioActiveDirectory(Login request)
        {
            string LogueoSQL = ConfigurationManager.AppSettings["LogueoSQL"].ToString();

            if (LogueoSQL.Equals("S"))
            {
                var Resultado = _LoginService.Verificar_Usuario_Admin(request);

                if (!string.IsNullOrEmpty(Resultado.Codigo))
                {

                    _VMUsuario.Administrador = Resultado;
                    VMDatosUsuario.SetValueLogin(_VMUsuario.Administrador.Codigo, _VMUsuario.Administrador.Codigo, _VMUsuario.Administrador.IDRol.ToString(), null, _VMUsuario.Administrador.AdministradorId);

                    FormsAuthentication.SetAuthCookie(VMDatosUsuario.GetUserAlias(), false);
                    List<PermisoUsuario> lstPermission = _LoginService.Listar_PermisosUsuario(_VMUsuario.Administrador.Codigo);


                    Session[Constantes.SessionKey.USERPERMISSIONS] = lstPermission;


                    return Redirect(string.Format("{0}", Url.Action("Index", "Home")));
                }
                else
                {
                    return Redirect(string.Format("{0}", Url.Action("Index", "Login")));
                }
            }
            else {
                var UserName = BuscarUsuariosAD(request);
                if (!string.IsNullOrEmpty(UserName))
                {
                    var Resultado = _LoginService.Verificar_Usuario_Por_UserNameValidado(UserName);

                    //if (!string.IsNullOrEmpty(Resultado.S_UserName))
                    //{

                    //    _VMUsuario.Usuario = Resultado;
                    //    VMDatosUsuario.SetValueLogin(_VMUsuario.Usuario.S_UserName, _VMUsuario.Usuario.S_Nombres, _VMUsuario.Usuario.N_IDRol.ToString(), null, _VMUsuario.Usuario.N_IDUsuario);

                    //    FormsAuthentication.SetAuthCookie(VMDatosUsuario.GetUserAlias(), false);
                    //    List<PermisoUsuario> lstPermission = _LoginService.Listar_PermisosUsuario(_VMUsuario.Usuario.S_UserName);


                    //    Session[Constantes.SessionKey.USERPERMISSIONS] = lstPermission;


                    //    return Redirect(string.Format("{0}", Url.Action("Index", "Home")));
                    //}
                    //else
                    //{
                        return Redirect(string.Format("{0}", Url.Action("IndexSinAcceso", "Login")));
                    //}
                }
                else {
                    return Redirect(string.Format("{0}", Url.Action("Index", "Login")));
                }
            }
        }

        public string BuscarUsuariosAD(Login request)
        {
            DirectoryEntry searchRoot = null;
            DirectorySearcher searcher = null;
            DirectoryEntry userEntry = null;

            var listUserAD = new List<Usuario>();
            try
            {
                string adminUser = request.S_Usuario;
                string adminPassword = request.S_Password;
                string container = ConfigurationManager.AppSettings["AD_Container"].ToString();
                string domainController = ConfigurationManager.AppSettings["AD_Name"].ToString();           

                searchRoot = new DirectoryEntry(String.Format("LDAP://{0}", domainController), adminUser, adminPassword);

                searcher = new DirectorySearcher(searchRoot);

                SearchResult result = searcher.FindOne();
                if (result == null)
                {
                    return "";                    
                }
                else
                {
                    return request.S_Usuario;
                }
            }
            catch
            {
                return "";
            }
            finally
            {
                if (userEntry != null) userEntry.Dispose();
                if (searcher != null) searcher.Dispose();
                if (searchRoot != null) searchRoot.Dispose();
            }
        }
        #endregion
    }
}