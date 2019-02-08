using Cosapi.ElCosapino.Aplicacion.Security.CategoriaService;
using Cosapi.ElCosapino.Aplicacion.Security.DepartamentoService;
using Cosapi.ElCosapino.Aplicacion.Security.DistritoService;
using Cosapi.ElCosapino.Aplicacion.Security.EspecialidadService;
using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.Aplicacion.Security.LogService;
using Cosapi.ElCosapino.Aplicacion.Security.PerfilService;
using Cosapi.ElCosapino.Aplicacion.Security.PostulacionService;
using Cosapi.ElCosapino.Aplicacion.Security.ProvinciaService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DepartamentoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ErrorAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PerfilAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ProvinciaAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Servicio.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Cosapi.ElCosapino.Servicio.Controllers
{
    public class AppController : ApiController
    {
        readonly ICategoriaAppService _CategoriaService = new CategoriaAppService();
        readonly IEspecialidadAppService _EspecialidadService = new EspecialidadAppService();
        readonly IDepartamentoAppService _DepartamentoService = new DepartamentoAppService();
        readonly IProvinciaAppService _ProvinciaService = new ProvinciaAppService();
        readonly IDistritoAppService _DistritoService = new DistritoAppService();
        readonly IPerfilAppService _PerfilService = new PerfilAppService();
        readonly IPostulacionAppService _PostulacionService = new PostulacionAppService();
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        readonly ILogAppService _LogService = new LogAppService();

        [System.Web.Mvc.OutputCache(Duration = 7200)]
        [Route("api/App/GetNoticias")]
        [HttpGet]
        public List<InterfaceWeb> GetNoticias()
        {
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "S_FechaPublicacion", SortDirection = "Descending", Activo = "A" };

            //Obtenemos los valores
            List<InterfaceWeb> lst_iweb_SeccionNoticias = new List<InterfaceWeb>();
            return _InterfaceWebService.List_InterfaceWebNoticia_Paginate(paginateParams, Constantes.ModulosWeb.Noticias, Constantes.ModulosSeccionWeb.Noticias_Noticias);          
        }

        [System.Web.Mvc.OutputCache(Duration = 3600)]
        [Route("api/App/GetCategoria")]
        [HttpGet]
        public List<CategoriaBE> GetCategoria()
        {
            return _CategoriaService.List_Categoria_APP();                
        }

        [Route("api/App/GetEspecialidad")]
        [HttpGet]
        public List<EspecialidadBE> GetEspecialidad(int CategoriaId)
        {
            try
            {
                return _EspecialidadService.List_Especialidad_APP(CategoriaId); 
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 3,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:GetEspecialidad(Controller APPController) - Error:" + ex.GetBaseException().ToString()
                };
                _LogService.Insert(_log);
                return null;
            }
                    
        }

        [System.Web.Mvc.OutputCache(Duration = 7200)]
        [Route("api/App/GetDepartamento")]
        [HttpGet]
        public List<DepartamentoBE> GetDepartamento()
        {
            return _DepartamentoService.List_Departamento_APP();           
        }

        [Route("api/App/GetProvincia")]
        [HttpGet]
        public List<ProvinciaBE> GetProvincia(int DepartamentoId)
        {
            return _ProvinciaService.List_Provincia_APP(DepartamentoId);    
        }

        [Route("api/App/GetDistrito")]
        [HttpGet]
        public List<DistritoBE> GetDistrito(int ProvinciaId)
        {
            return _DistritoService.List_Distrito_APP(ProvinciaId);           
        }

        [Route("api/App/GetVacante")]
        [HttpGet]
        public List<PerfilBE> GetVacante(int CategoriaId, int UsuarioId)
        {
            return _PerfilService.List_Perfil_APP(CategoriaId, UsuarioId);          
        }

        [Route("api/App/GetPostulacion")]
        [HttpGet]
        public List<PostulacionBE> GetPostulacion(int UsuarioId)
        {           
            return _PostulacionService.List_Postulacion_APP(UsuarioId); 
        }

        [Route("api/App/GetUsuario")]
        [HttpGet]
        public UsuarioBE GetUsuario(String CorreoElectronico, String Password, String DeviceToken)
        {
            try
            {              
                Usuario objUsuario = _UsuarioService.UsuarioPorCredenciales(CorreoElectronico, Password); //context.Usuario.Where(x => x.CorreoElectronico == CorreoElectronico && x.Password == Password).FirstOrDefault();
                
                if (string.IsNullOrEmpty(objUsuario.DeviceToken) || objUsuario.DeviceToken != DeviceToken)
                {
                    objUsuario.DeviceToken = DeviceToken;
                    _UsuarioService.UpdateDevice(objUsuario);                   
                }

                if (objUsuario == null)
                    return new UsuarioBE("El usuario y/o contraseña incorrecto.");

                if (objUsuario.Estado != "ACT")
                    return new UsuarioBE("El usuario no se encuentra habilitado.");

                UsuarioBE objUsuarioBE = new UsuarioBE();

                objUsuarioBE.ApellidoMaterno = objUsuario.ApellidoMaterno;
                objUsuarioBE.ApellidoPaterno = objUsuario.ApellidoPaterno;
                objUsuarioBE.CorreoElectronico = objUsuario.CorreoElectronico;
                objUsuarioBE.DistritoId = objUsuario.DistritoId;
                objUsuarioBE.CategoriaId = objUsuario.CategoriaId;
                objUsuarioBE.ProvinciaId = objUsuario.ProvinciaId;
                objUsuarioBE.DepartamentoId = objUsuario.DepartamentoId;
                objUsuarioBE.DNI = objUsuario.DNI;
                objUsuarioBE.EspecialidadId = objUsuario.EspecialidadId;
                objUsuarioBE.EsTrabajador = objUsuario.EsTrabajador;
                objUsuarioBE.FechaNacimiento = objUsuario.FechaNacimiento;
                objUsuarioBE.strFechaNacimiento = objUsuario.FechaNacimiento.ToString();
                objUsuarioBE.Nombres = objUsuario.Nombres;
                objUsuarioBE.NumeroContacto1 = objUsuario.NumeroContacto1;
                objUsuarioBE.NumeroContacto2 = objUsuario.NumeroContacto2;
                objUsuarioBE.TieneEstudios = objUsuario.TieneEstudios;
                objUsuarioBE.UsuarioId = objUsuario.UsuarioId;
                objUsuarioBE.DeviceToken = DeviceToken;

                return objUsuarioBE;
            }
            catch
            {
                return new UsuarioBE("Error en el sistema, por favor intente más tarde.");
            }
        }

        [Route("api/App/GetUsuarioRS")]
        [HttpGet]
        public UsuarioBE GetUsuarioRS(String CorreoElectronico,String Key, String DeviceToken)
        {
            try
            {
                Usuario objUsuario = _UsuarioService.UsuarioPorCorreoRS(CorreoElectronico, Key); //context.Usuario.Where(x => x.CorreoElectronico == CorreoElectronico && x.Password == Password).FirstOrDefault();

                if (string.IsNullOrEmpty(objUsuario.DeviceToken) || objUsuario.DeviceToken != DeviceToken)
                {
                    objUsuario.DeviceToken = DeviceToken;
                    _UsuarioService.UpdateDevice(objUsuario);
                }

                if (objUsuario == null)
                    return new UsuarioBE("El usuario y/o contraseña incorrecto.");

                if (objUsuario.Estado != "ACT")
                    return new UsuarioBE("El usuario no se encuentra habilitado.");

                UsuarioBE objUsuarioBE = new UsuarioBE();

                objUsuarioBE.ApellidoMaterno = objUsuario.ApellidoMaterno;
                objUsuarioBE.ApellidoPaterno = objUsuario.ApellidoPaterno;
                objUsuarioBE.CorreoElectronico = objUsuario.CorreoElectronico;
                objUsuarioBE.DistritoId = objUsuario.DistritoId;
                objUsuarioBE.CategoriaId = objUsuario.CategoriaId;
                objUsuarioBE.ProvinciaId = objUsuario.ProvinciaId;
                objUsuarioBE.DepartamentoId = objUsuario.DepartamentoId;
                objUsuarioBE.DNI = objUsuario.DNI;
                objUsuarioBE.EspecialidadId = objUsuario.EspecialidadId;
                objUsuarioBE.EsTrabajador = objUsuario.EsTrabajador;
                objUsuarioBE.FechaNacimiento = objUsuario.FechaNacimiento;
                objUsuarioBE.strFechaNacimiento = objUsuario.FechaNacimiento.ToString();
                objUsuarioBE.Nombres = objUsuario.Nombres;
                objUsuarioBE.NumeroContacto1 = objUsuario.NumeroContacto1;
                objUsuarioBE.NumeroContacto2 = objUsuario.NumeroContacto2;
                objUsuarioBE.TieneEstudios = objUsuario.TieneEstudios;
                objUsuarioBE.UsuarioId = objUsuario.UsuarioId;
                objUsuarioBE.DeviceToken = DeviceToken;

                return objUsuarioBE;
            }
            catch(Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 3,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:GetUsuarioRS(Controller APPController) - Error:" + ex.GetBaseException().ToString()
                };
                _LogService.Insert(_log);

                return new UsuarioBE("Error en el sistema, por favor intente más tarde.");
            }
        }

        [Route("api/App/SaveUsuario")]
        [HttpPost]
        public UsuarioBE SaveUsuario(UsuarioBE objUsuarioBE)
        {
            try
            {               
                Usuario objUsuario = _UsuarioService.UsuarioPorCorreo(objUsuarioBE.CorreoElectronico.Trim()); //context.Usuario.FirstOrDefault(x => x.CorreoElectronico == objUsuarioBE.CorreoElectronico.Trim());

                if (objUsuario != null)
                    return new UsuarioBE("El usuario ya se encuentra registrado.");

                objUsuario = new Usuario();
                objUsuario.ApellidoMaterno = objUsuarioBE.ApellidoMaterno;
                objUsuario.ApellidoPaterno = objUsuarioBE.ApellidoPaterno;
                objUsuario.CorreoElectronico = objUsuarioBE.CorreoElectronico;
                objUsuario.DistritoId = objUsuarioBE.DistritoId;
                objUsuario.DNI = objUsuarioBE.DNI;
                objUsuario.EspecialidadId = objUsuarioBE.EspecialidadId;
                objUsuario.EsTrabajador = objUsuarioBE.EsTrabajador;
                objUsuario.strFechaNacimiento = objUsuarioBE.strFechaNacimiento;
                objUsuario.Nombres = objUsuarioBE.Nombres;
                objUsuario.NumeroContacto1 = objUsuarioBE.NumeroContacto1;
                objUsuario.NumeroContacto2 = objUsuarioBE.NumeroContacto2;
                objUsuario.TieneEstudios = objUsuarioBE.TieneEstudios;
                objUsuario.Password = objUsuarioBE.Password;
                objUsuario.Estado = "REG";
                objUsuario.DeviceToken = objUsuario.DeviceToken;
                objUsuario.Origen = objUsuario.Origen;
                objUsuario.UsuarioIDExterno = objUsuario.UsuarioIDExterno;
                _UsuarioService.Insert(objUsuario);          

                try
                {
                   var emailLogic = new EmailLogic();
                   emailLogic.SendEmailVerificacionCorreo(objUsuario.UsuarioId);
                }
                catch
                {
                    return new UsuarioBE("El correo electrónico ingresado es incorrecto.");
                }

                return objUsuarioBE;
            }
            catch
            {
                return new UsuarioBE("Por favor, ingrese todos los campos completos.");
            }
        }

        [Route("api/App/EditUsuario")]
        [HttpPost]
        public UsuarioBE EditUsuario(UsuarioBE objUsuarioBE)
        {
            try
            {               
                Usuario objUsuario = new Usuario();
                objUsuario.UsuarioId = objUsuarioBE.UsuarioId;
                objUsuario.ApellidoMaterno = objUsuarioBE.ApellidoMaterno;
                objUsuario.ApellidoPaterno = objUsuarioBE.ApellidoPaterno;
                objUsuario.DistritoId = objUsuarioBE.DistritoId;
                objUsuario.DNI = objUsuarioBE.DNI;
                objUsuario.EspecialidadId = objUsuarioBE.EspecialidadId;
                objUsuario.EsTrabajador = objUsuarioBE.EsTrabajador;
                objUsuario.strFechaNacimiento = objUsuarioBE.strFechaNacimiento;
                objUsuario.Nombres = objUsuarioBE.Nombres;
                objUsuario.NumeroContacto1 = objUsuarioBE.NumeroContacto1;
                objUsuario.NumeroContacto2 = objUsuarioBE.NumeroContacto2;
                objUsuario.TieneEstudios = objUsuarioBE.TieneEstudios;
                objUsuario.DeviceToken = objUsuarioBE.DeviceToken;

                _UsuarioService.Update(objUsuario);         
                return objUsuarioBE;
            }
            catch 
            {
                return new UsuarioBE("Por favor, ingrese todos los campos completos.");
            }
        }

        [Route("api/App/EditPassword")]
        [HttpPost]
        public UsuarioBE EditPassword(UsuarioBE objUsuarioBE)
        {
            try
            {            
                Usuario objUsuario = new Usuario();
                objUsuario.UsuarioId = objUsuarioBE.UsuarioId;
                objUsuario.Password = objUsuarioBE.Password;          
                _UsuarioService.UpdatePassword(objUsuario);
                return objUsuarioBE;
            }
            catch
            {
                return new UsuarioBE("Por favor, ingrese todos los campos completos.");
            }
        }

        [Route("api/App/SavePostulacion")]
        [HttpPost]
        public ErrorBE SavePostulacion(int PerfilId, int UsuarioId)
        {
            try
            {             
                PostulacionBE objPostulacion = new PostulacionBE();

                objPostulacion.PerfilId = PerfilId;
                objPostulacion.UsuarioId = UsuarioId;               
                _PostulacionService.Insert(objPostulacion);
                return new ErrorBE();
            }
            catch (Exception ex)
            {
                return new ErrorBE(ex.Message.ToString());
            }
        }

        [Route("api/App/SendPassword")]
        [HttpGet]
        public ErrorBE SendPassword(String CorreoElectronico)
        {
            try
            {            
                Usuario objUsuario = _UsuarioService.UsuarioPorCorreo(CorreoElectronico); 

                if (objUsuario == null)
                    return new ErrorBE("El correo electrónico no se encuentra registrado en el sistema.");

                Usuario objNewUsuario = new Usuario();
                objNewUsuario.UsuarioId = objUsuario.UsuarioId;
                objNewUsuario.Password = Guid.NewGuid().ToString().Replace("-", String.Empty).Substring(0, 8);
                _UsuarioService.UpdatePassword(objNewUsuario);
               
                try
                {
                    var emailLogic = new EmailLogic();
                    emailLogic.SendEmailResetPassword(objUsuario.UsuarioId);
                }
                catch
                {
                    return new ErrorBE("El correo electrónico ingresado es incorrecto.");
                }

                return new ErrorBE();
            }
            catch (Exception ex)
            {
                return new ErrorBE(ex.Message.ToString());
            }
        }
    }
}