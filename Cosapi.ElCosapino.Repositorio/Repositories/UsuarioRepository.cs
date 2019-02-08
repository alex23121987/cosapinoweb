using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public UsuarioRepository(){}

        public List<Usuario> List_Usuario_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Usuario>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Usuario_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),                
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objUsuario = new Usuario();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objUsuario.UsuarioId = DataConvert.ToInt32(dataReader["N_UsuarioId"]);
                        objUsuario.ApellidoPaterno = DataConvert.ToString(dataReader["S_ApellidoPaterno"]);
                        objUsuario.ApellidoMaterno = DataConvert.ToString(dataReader["S_ApellidoMaterno"]);
                        objUsuario.Nombres = DataConvert.ToString(dataReader["S_Nombres"]);
                        objUsuario.DNI = DataConvert.ToString(dataReader["S_DNI"]);
                        objUsuario.CorreoElectronico = DataConvert.ToString(dataReader["S_CorreoElectronico"]);
                        objUsuario.NumeroContacto1 = DataConvert.ToString(dataReader["S_NumeroContacto1"]);
                        objUsuario.NumeroContacto2 = DataConvert.ToString(dataReader["S_NumeroContacto2"]);                      
                        objUsuario.EspecialidadNombre = DataConvert.ToString(dataReader["S_Especialidad"]);
                        objUsuario.CategoriaNombre = DataConvert.ToString(dataReader["S_Categoria"]);
                        objUsuario.DepartamentoNombre = DataConvert.ToString(dataReader["S_Departamento"]);
                        objUsuario.strFechaNacimiento = DataConvert.ToString(dataReader["S_FechaNacimiento"]);
                        objUsuario.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : (DataConvert.ToString(dataReader["S_Estado"]).Equals("REG") ? "Registrado":"Inactivo");
                        objUsuario.Origen = DataConvert.ToString(dataReader["S_Origen"]);
                        list.Add(objUsuario);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_Usuario_Paginate(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }         
            return list;
        }

        public DataTable DT_Usuario()
        {
            DataTable dtResult = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["BDElCosapino"].ConnectionString);

                if (cnn.State == ConnectionState.Closed) cnn.Open();

                string sqlStatement = "exec up_Usuario_Sellst_Exportar";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlStatement, cnn);
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;
                sqlDataAdapter.Fill(dtResult);
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:DT_Postulacion(Repository Postulacion) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return dtResult;
        }

        public Usuario Update(Usuario item)
        {
            var objResult = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Upd");
                oDatabase.AddInParameter(oDbCommand, "@UsuarioId", DbType.String, item.UsuarioId);
                oDatabase.AddInParameter(oDbCommand, "@ApellidoMaterno", DbType.String, item.ApellidoMaterno);
                oDatabase.AddInParameter(oDbCommand, "@ApellidoPaterno", DbType.String, item.ApellidoPaterno);
                //oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, item.CorreoElectronico);
                oDatabase.AddInParameter(oDbCommand, "@DistritoId", DbType.Int32, item.DistritoId);
                oDatabase.AddInParameter(oDbCommand, "@DNI", DbType.String, item.DNI);
                oDatabase.AddInParameter(oDbCommand, "@EspecialidadId", DbType.Int32, item.EspecialidadId);
                oDatabase.AddInParameter(oDbCommand, "@EsTrabajador", DbType.Boolean, item.EsTrabajador);
                oDatabase.AddInParameter(oDbCommand, "@strFechaNacimiento", DbType.String, item.strFechaNacimiento);
                oDatabase.AddInParameter(oDbCommand, "@Nombres", DbType.String, item.Nombres);
                oDatabase.AddInParameter(oDbCommand, "@NumeroContacto1", DbType.String, item.NumeroContacto1);
                oDatabase.AddInParameter(oDbCommand, "@NumeroContacto2", DbType.String, item.NumeroContacto2);
                oDatabase.AddInParameter(oDbCommand, "@TieneEstudios", DbType.Boolean, item.TieneEstudios);                
                oDatabase.AddInParameter(oDbCommand, "@DeviceToken", DbType.String, item.DeviceToken);
                objResult.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Update(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Usuario UpdateDevice(Usuario item)
        {
            var objResult = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Upd_DeviceToken");
                oDatabase.AddInParameter(oDbCommand, "@IDUsuario", DbType.Int32, item.UsuarioId);
                oDatabase.AddInParameter(oDbCommand, "@DeviceToken", DbType.String , item.DeviceToken);   
                objResult.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:UpdateDevice(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Usuario UpdateEstado(Usuario item)
        {
            var objResult = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Upd_Estado");
                oDatabase.AddInParameter(oDbCommand, "@IDUsuario", DbType.Int32, item.UsuarioId);
                oDatabase.AddInParameter(oDbCommand, "@Estado", DbType.String, item.Estado);
                objResult.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:UpdateEstado(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Usuario UpdatePassword(Usuario item)
        {
            var objResult = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Upd_Password");
                oDatabase.AddInParameter(oDbCommand, "@IDUsuario", DbType.Int32, item.UsuarioId);
                oDatabase.AddInParameter(oDbCommand, "@Password", DbType.String, item.Password);
                objResult.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:UpdateDevice(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Usuario Insert(Usuario item)
        {
            var objResult = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Ins");
                oDatabase.AddInParameter(oDbCommand, "@ApellidoMaterno", DbType.String, item.ApellidoMaterno);
                oDatabase.AddInParameter(oDbCommand, "@ApellidoPaterno", DbType.String, item.ApellidoPaterno);
                oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, item.CorreoElectronico);
                oDatabase.AddInParameter(oDbCommand, "@DistritoId", DbType.Int32, item.DistritoId);
                oDatabase.AddInParameter(oDbCommand, "@DNI", DbType.String, item.DNI);
                oDatabase.AddInParameter(oDbCommand, "@EspecialidadId", DbType.Int32, item.EspecialidadId);
                oDatabase.AddInParameter(oDbCommand, "@EsTrabajador", DbType.Int32, item.EsTrabajador?1:0);
                oDatabase.AddInParameter(oDbCommand, "@strFechaNacimiento", DbType.String, item.strFechaNacimiento);
                oDatabase.AddInParameter(oDbCommand, "@Nombres", DbType.String, item.Nombres);
                oDatabase.AddInParameter(oDbCommand, "@NumeroContacto1", DbType.String, item.NumeroContacto1);
                oDatabase.AddInParameter(oDbCommand, "@NumeroContacto2", DbType.String, item.NumeroContacto2);
                oDatabase.AddInParameter(oDbCommand, "@TieneEstudios", DbType.Int32, item.TieneEstudios?1:0);
                oDatabase.AddInParameter(oDbCommand, "@Password", DbType.String, item.Password);
                oDatabase.AddInParameter(oDbCommand, "@Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@DeviceToken", DbType.String, item.DeviceToken);
                oDatabase.AddInParameter(oDbCommand, "@Origen", DbType.String, item.Origen);
                oDatabase.AddInParameter(oDbCommand, "@UsuarioIDExterno", DbType.String, item.UsuarioIDExterno);
                objResult.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Usuario Insert_RedesSociales(Usuario item)
        {
            var objResult = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Ins_RedesSociales");                
                oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, item.CorreoElectronico);                
                oDatabase.AddInParameter(oDbCommand, "@Nombres", DbType.String, item.Nombres);               
                oDatabase.AddInParameter(oDbCommand, "@Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@DeviceToken", DbType.String, item.DeviceToken);
                oDatabase.AddInParameter(oDbCommand, "@Origen", DbType.String, item.Origen);
                oDatabase.AddInParameter(oDbCommand, "@UsuarioIDExterno", DbType.String, item.UsuarioIDExterno);
                objResult.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert_RedesSociales(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Usuario Delete(Usuario item)
        {
            var Resultado = new Usuario();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDUsuario", DbType.Int32, item.UsuarioId);                
                Resultado.UsuarioId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Delete(Repository Usuario) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }        

        public List<DDLOptionsText> ListarPerfil(int N_IDUnidad)
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sellst_Perfil");
            oDatabase.AddInParameter(oDbCommand, "@N_IDUnidad", DbType.Int32, N_IDUnidad);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["N_IDPerfil"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> ListarEspecialidad(int N_IDUnidad)
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sellst_Especialidad");
            oDatabase.AddInParameter(oDbCommand, "@N_IDUnidad", DbType.Int32, N_IDUnidad);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["N_IDEspecialidad"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public List<DDLOptionsText> ListarCargo(int N_IDUnidad)
        {
            var lstParametro = new List<DDLOptionsText>();
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sellst_Cargo");
            oDatabase.AddInParameter(oDbCommand, "@N_IDUnidad", DbType.Int32, N_IDUnidad);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var oParametro = new DDLOptionsText
                    {

                        S_ID = DataConvert.ToString(oReader["N_IDParametro"]),
                        S_Descripcion = DataConvert.ToString(oReader["S_Descripcion"])
                    };
                    lstParametro.Add(oParametro);
                }
                oReader.Close();
            }
            return lstParametro;
        }

        public Usuario UsuarioPorID(int IDUsuario)
        {
            Usuario objUsuario = null;          
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sel_Por_ID");
            oDatabase.AddInParameter(oDbCommand, "@N_IDUsuario", DbType.Int32, IDUsuario);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objUsuario = new Usuario
                    {
                        UsuarioId = DataConvert.ToInt32(oReader["N_UsuarioId"]),
                        ApellidoPaterno = DataConvert.ToString(oReader["S_ApellidoPaterno"]),
                        ApellidoMaterno = DataConvert.ToString(oReader["S_ApellidoMaterno"]),                        
                        CorreoElectronico = DataConvert.ToString(oReader["S_CorreoElectronico"]),
                        DistritoId = DataConvert.ToInt32(oReader["N_DistritoId"]),
                        CategoriaId = DataConvert.ToInt32(oReader["N_CategoriaId"]),
                        ProvinciaId = DataConvert.ToInt32(oReader["N_ProvinciaId"]),
                        DepartamentoId = DataConvert.ToInt32(oReader["N_DepartamentoId"]),
                        DNI = DataConvert.ToString(oReader["S_DNI"]),
                        EspecialidadId = DataConvert.ToInt32(oReader["N_EspecialidadId"]),
                        EsTrabajador = DataConvert.ToBool(oReader["N_EsTrabajador"]),
                        FechaNacimiento = DataConvert.ToDateTime(oReader["D_FechaNacimiento"]),
                        strFechaNacimiento = DataConvert.ToString(oReader["S_FechaNacimiento"]),
                        Nombres = DataConvert.ToString(oReader["S_Nombres"]),
                        NumeroContacto1 = DataConvert.ToString(oReader["S_NumeroContacto1"]),
                        NumeroContacto2 = DataConvert.ToString(oReader["S_NumeroContacto2"]),
                        TieneEstudios = DataConvert.ToBool(oReader["N_TieneEstudios"]),
                        DeviceToken = DataConvert.ToString(oReader["S_DeviceToken"]),
                        Estado = DataConvert.ToString(oReader["S_Usuario_Estado"])
                    };                 
                }
                oReader.Close();
            }
            return objUsuario;
        }

        public Usuario UsuarioPorCredenciales(string CorreoElectronico,string Clave)
        {
            Usuario objUsuario = null;
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sel_APP");
            oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, CorreoElectronico);
            oDatabase.AddInParameter(oDbCommand, "@Clave", DbType.String, Clave);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objUsuario = new Usuario
                    {
                        UsuarioId = DataConvert.ToInt32(oReader["N_UsuarioId"]),
                        ApellidoPaterno = DataConvert.ToString(oReader["S_ApellidoPaterno"]),
                        ApellidoMaterno = DataConvert.ToString(oReader["S_ApellidoMaterno"]),                        
                        CorreoElectronico = DataConvert.ToString(oReader["S_CorreoElectronico"]),
                        DistritoId = DataConvert.ToInt32(oReader["N_DistritoId"]),
                        CategoriaId = DataConvert.ToInt32(oReader["N_CategoriaId"]),
                        ProvinciaId = DataConvert.ToInt32(oReader["N_ProvinciaId"]),
                        DepartamentoId = DataConvert.ToInt32(oReader["N_DepartamentoId"]),
                        DNI = DataConvert.ToString(oReader["S_DNI"]),
                        EspecialidadId = DataConvert.ToInt32(oReader["N_EspecialidadId"]),
                        EsTrabajador = DataConvert.ToBool(oReader["N_EsTrabajador"]),
                        FechaNacimiento = DataConvert.ToDateTime(oReader["D_FechaNacimiento"]),
                        Nombres = DataConvert.ToString(oReader["S_Nombres"]),
                        NumeroContacto1 = DataConvert.ToString(oReader["S_NumeroContacto1"]),
                        NumeroContacto2 = DataConvert.ToString(oReader["S_NumeroContacto2"]),
                        TieneEstudios = DataConvert.ToBool(oReader["N_TieneEstudios"]),
                        DeviceToken = DataConvert.ToString(oReader["S_DeviceToken"]),
                        Estado  = DataConvert.ToString(oReader["S_Usuario_Estado"])
                    };
                }
                oReader.Close();
            }
            return objUsuario;
        }

        public Usuario UsuarioPorCorreo(string CorreoElectronico)
        {
            Usuario objUsuario = null;
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sel_Usuario_Por_Correo");
            oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, CorreoElectronico);          

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objUsuario = new Usuario
                    {
                        UsuarioId = DataConvert.ToInt32(oReader["N_UsuarioId"]),
                        ApellidoPaterno = DataConvert.ToString(oReader["S_ApellidoPaterno"]),
                        ApellidoMaterno = DataConvert.ToString(oReader["S_ApellidoMaterno"]),                        
                        CorreoElectronico = DataConvert.ToString(oReader["S_CorreoElectronico"]),
                        DistritoId = DataConvert.ToInt32(oReader["N_DistritoId"]),
                        CategoriaId = DataConvert.ToInt32(oReader["N_CategoriaId"]),
                        ProvinciaId = DataConvert.ToInt32(oReader["N_ProvinciaId"]),
                        DepartamentoId = DataConvert.ToInt32(oReader["N_DepartamentoId"]),
                        DNI = DataConvert.ToString(oReader["S_DNI"]),
                        EspecialidadId = DataConvert.ToInt32(oReader["N_EspecialidadId"]),
                        EsTrabajador = DataConvert.ToBool(oReader["N_EsTrabajador"]),
                        FechaNacimiento = DataConvert.ToDateTime(oReader["D_FechaNacimiento"]),
                        Nombres = DataConvert.ToString(oReader["S_Nombres"]),
                        NumeroContacto1 = DataConvert.ToString(oReader["S_NumeroContacto1"]),
                        NumeroContacto2 = DataConvert.ToString(oReader["S_NumeroContacto2"]),
                        TieneEstudios = DataConvert.ToBool(oReader["N_TieneEstudios"]),
                        DeviceToken = DataConvert.ToString(oReader["S_DeviceToken"]),
                        Estado = DataConvert.ToString(oReader["S_Usuario_Estado"])
                    };
                }
                oReader.Close();
            }
            return objUsuario;
        }

        public Usuario UsuarioPorCorreoRS(string CorreoElectronico,string Key)
        {
            Usuario objUsuario = null;
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sel_Usuario_Por_CorreoRS");
            oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, CorreoElectronico);
            oDatabase.AddInParameter(oDbCommand, "@Key", DbType.String, Key);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objUsuario = new Usuario
                    {
                        UsuarioId = DataConvert.ToInt32(oReader["N_UsuarioId"]),
                        ApellidoPaterno = DataConvert.ToString(oReader["S_ApellidoPaterno"]),
                        ApellidoMaterno = DataConvert.ToString(oReader["S_ApellidoMaterno"]),                        
                        CorreoElectronico = DataConvert.ToString(oReader["S_CorreoElectronico"]),
                        DistritoId = DataConvert.ToInt32(oReader["N_DistritoId"]),
                        CategoriaId = DataConvert.ToInt32(oReader["N_CategoriaId"]),
                        ProvinciaId = DataConvert.ToInt32(oReader["N_ProvinciaId"]),
                        DepartamentoId = DataConvert.ToInt32(oReader["N_DepartamentoId"]),
                        DNI = DataConvert.ToString(oReader["S_DNI"]),
                        EspecialidadId = DataConvert.ToInt32(oReader["N_EspecialidadId"]),
                        EsTrabajador = DataConvert.ToBool(oReader["N_EsTrabajador"]),
                        FechaNacimiento = DataConvert.ToDateTime(oReader["D_FechaNacimiento"]),
                        Nombres = DataConvert.ToString(oReader["S_Nombres"]),
                        NumeroContacto1 = DataConvert.ToString(oReader["S_NumeroContacto1"]),
                        NumeroContacto2 = DataConvert.ToString(oReader["S_NumeroContacto2"]),
                        TieneEstudios = DataConvert.ToBool(oReader["N_TieneEstudios"]),
                        DeviceToken = DataConvert.ToString(oReader["S_DeviceToken"]),
                        Estado = DataConvert.ToString(oReader["S_Usuario_Estado"])
                    };
                }
                oReader.Close();
            }
            return objUsuario;
        }

        public Usuario UsuarioPorCorreo_Sin_RedesSociales(string CorreoElectronico)
        {
            Usuario objUsuario = null;
            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Usuario_Sel_Usuario_Por_Correo_SinRRSS");
            oDatabase.AddInParameter(oDbCommand, "@CorreoElectronico", DbType.String, CorreoElectronico);

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objUsuario = new Usuario
                    {
                        UsuarioId = DataConvert.ToInt32(oReader["N_UsuarioId"]),
                        ApellidoPaterno = DataConvert.ToString(oReader["S_ApellidoPaterno"]),
                        ApellidoMaterno = DataConvert.ToString(oReader["S_ApellidoMaterno"]),                        
                        CorreoElectronico = DataConvert.ToString(oReader["S_CorreoElectronico"]),
                        DistritoId = DataConvert.ToInt32(oReader["N_DistritoId"]),
                        CategoriaId = DataConvert.ToInt32(oReader["N_CategoriaId"]),
                        ProvinciaId = DataConvert.ToInt32(oReader["N_ProvinciaId"]),
                        DepartamentoId = DataConvert.ToInt32(oReader["N_DepartamentoId"]),
                        DNI = DataConvert.ToString(oReader["S_DNI"]),
                        EspecialidadId = DataConvert.ToInt32(oReader["N_EspecialidadId"]),
                        EsTrabajador = DataConvert.ToBool(oReader["N_EsTrabajador"]),
                        FechaNacimiento = DataConvert.ToDateTime(oReader["D_FechaNacimiento"]),
                        Nombres = DataConvert.ToString(oReader["S_Nombres"]),
                        NumeroContacto1 = DataConvert.ToString(oReader["S_NumeroContacto1"]),
                        NumeroContacto2 = DataConvert.ToString(oReader["S_NumeroContacto2"]),
                        TieneEstudios = DataConvert.ToBool(oReader["N_TieneEstudios"]),
                        DeviceToken = DataConvert.ToString(oReader["S_DeviceToken"])
                    };
                }
                oReader.Close();
            }
            return objUsuario;
        }
    }
}
