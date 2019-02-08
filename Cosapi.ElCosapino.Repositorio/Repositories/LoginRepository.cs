using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public LoginRepository(){}

        public Usuario Verificar_Usuario_Externo(Login login)
        {
            var objusuario = new Usuario();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Login_Sel_ValidaUsuarioExterno", CommandType.StoredProcedure,
                SQLServer.CreateParameter("S_Usuario", SqlDbType.VarChar, login.S_Usuario),
                SQLServer.CreateParameter("S_Password", SqlDbType.VarChar, login.S_Password));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        objusuario.UsuarioId = DataConvert.ToInt(dataReader["N_IDUsuario"]);
                        objusuario.CorreoElectronico = DataConvert.ToString(dataReader["S_UserName"]);
                        objusuario.NombreCompleto = DataConvert.ToString(dataReader["S_Nombres"]);
                        objusuario.ApellidoPaterno = DataConvert.ToString(dataReader["S_ApellidoPaterno"]);
                        objusuario.ApellidoMaterno = DataConvert.ToString(dataReader["S_ApellidoMaterno"]);
                        objusuario.Estado = DataConvert.ToString(dataReader["S_Estado"]);
                        objusuario.DNI = DataConvert.ToString(dataReader["S_DNI"]);
                        objusuario.Resultado = DataConvert.ToInt(dataReader["N_Resultado"]);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Verificar_Usuario_Externo(Repository Login) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return objusuario;
        }

        public Usuario Verificar_Usuario(Login login)
        {
            var objusuario = new Usuario();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Login_Sel_ValidaUsuario", CommandType.StoredProcedure,
                SQLServer.CreateParameter("S_Usuario", SqlDbType.VarChar, login.S_Usuario),
                SQLServer.CreateParameter("S_Password", SqlDbType.VarChar, login.S_Password));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        objusuario.UsuarioId = DataConvert.ToInt(dataReader["N_IDUsuario"]);
                        objusuario.CorreoElectronico = DataConvert.ToString(dataReader["S_UserName"]);
                        objusuario.Nombres = DataConvert.ToString(dataReader["S_Nombres"]);
                        objusuario.ApellidoPaterno = DataConvert.ToString(dataReader["S_ApellidoPaterno"]);
                        objusuario.ApellidoMaterno = DataConvert.ToString(dataReader["S_ApellidoMaterno"]);
                        //objusuario.N_IDRol = DataConvert.ToInt(dataReader["N_IDRol"]);                        
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Verificar_Usuario(Repository Login) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return objusuario;
        }

        public Administrador Verificar_Usuario_Admin(Login login)
        {
            var objAdministrador = new Administrador();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Login_Sel_ValidaUsuario", CommandType.StoredProcedure,
                SQLServer.CreateParameter("S_Usuario", SqlDbType.VarChar, login.S_Usuario),
                SQLServer.CreateParameter("S_Password", SqlDbType.VarChar, login.S_Password));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        objAdministrador.AdministradorId = DataConvert.ToInt(dataReader["N_IDUsuario"]);
                        objAdministrador.Codigo = DataConvert.ToString(dataReader["S_UserName"]);
                        objAdministrador.Estado = DataConvert.ToString(dataReader["S_Nombres"]);                        
                        objAdministrador.IDRol = DataConvert.ToInt(dataReader["N_IDRol"]);                        
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Verificar_Usuario(Repository Login) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return objAdministrador;
        }

        public Usuario Verificar_Usuario_Por_UserNameValidado(string UserName)
        {
            var objusuario = new Usuario();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Login_Sel_ValidaUsuario_UserName", CommandType.StoredProcedure,                
                SQLServer.CreateParameter("S_UserName", SqlDbType.VarChar, UserName));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        objusuario.UsuarioId = DataConvert.ToInt(dataReader["N_IDUsuario"]);
                        objusuario.CorreoElectronico = DataConvert.ToString(dataReader["S_UserName"]);
                        objusuario.Nombres = DataConvert.ToString(dataReader["S_Nombres"]);
                        objusuario.ApellidoPaterno = DataConvert.ToString(dataReader["S_ApellidoPaterno"]);
                        objusuario.ApellidoMaterno = DataConvert.ToString(dataReader["S_ApellidoMaterno"]);
                        //objusuario.N_IDRol = DataConvert.ToInt(dataReader["N_IDRol"]);                        
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Verificar_Usuario_Por_UserNameValidado(Repository Login) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return objusuario;
        }

        public List<PermisoUsuario> Listar_PermisosUsuario(string S_UserName)
        {
            var listPermission = new List<PermisoUsuario>();

            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Login_PermisosUsuario");
            oDatabase.AddInParameter(oDbCommand, "@S_UserName", DbType.String, S_UserName);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var objPermission = new PermisoUsuario
                    {
                        N_IDPermiso = DataConvert.ToInt32(oReader["N_IDPermiso"]),
                        S_IDGrupo = DataConvert.ToString(oReader["S_IDGrupo"]),
                        S_GrupoDescripcion = DataConvert.ToString(oReader["S_GrupoDescripcion"]),
                        S_IDPagina = DataConvert.ToString(oReader["S_IDPagina"]),
                        S_PaginaDescripcion = DataConvert.ToString(oReader["S_PaginaDescripcion"]),
                        S_IDAccion = DataConvert.ToString(oReader["S_IDAccion"]),
                        S_AccionDescripcion = DataConvert.ToString(oReader["S_AccionDescripcion"])
                    };
                 
                    listPermission.Add(objPermission);
                }
                oReader.Close();
            }
            return listPermission;
        }
    }
}
