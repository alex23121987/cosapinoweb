using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.NotificacionAgg;
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
    public class NotificacionRepository : INotificacionRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public NotificacionRepository() { }

        public List<Usuario> List_Notificacion_Paginate(PaginateParams paginateParams,Usuario _Usuario)
        {
            var list = new List<Usuario>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Notificacion_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("CategoriaId", SqlDbType.Int, _Usuario.CategoriaId),
                SQLServer.CreateParameter("EspecialidadId", SqlDbType.Int, _Usuario.EspecialidadId),
                SQLServer.CreateParameter("FiltroBuscar", SqlDbType.VarChar, _Usuario.Filtro),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters)
                );
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
                        objUsuario.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : (DataConvert.ToString(dataReader["S_Estado"]).Equals("REG") ? "Registrado" : "Inactivo");
                        objUsuario.Origen = DataConvert.ToString(dataReader["S_Origen"]);
                        objUsuario.DeviceToken = DataConvert.ToString(dataReader["S_DeviceToken"]);
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

        public List<Notificacion> List_HistorialNotificacion_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Notificacion>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_HistorialNotificacion_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),               
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters)
                );
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objUsuario = new Notificacion();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objUsuario.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objUsuario.Subtitulo = DataConvert.ToString(dataReader["S_Subtitulo"]);                     
                        objUsuario.Fecha = DataConvert.ToString(dataReader["S_Fecha"]);
                        objUsuario.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ENV") ? "Enviado" : "";                   
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

        public DataTable DT_Notificacion()
        {
            DataTable dtResult = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["BDElCosapino"].ConnectionString);

                if (cnn.State == ConnectionState.Closed) cnn.Open();

                string sqlStatement = "exec up_Notificacion_Sellst_Exportar";

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

        public Notificacion Insert(Notificacion item)
        {
            var objResult = new Notificacion();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Notificacion_Ins");
                oDatabase.AddInParameter(oDbCommand, "@Titulo", DbType.String, item.Titulo);
                oDatabase.AddInParameter(oDbCommand, "@Subtitulo", DbType.String, item.Subtitulo);             
                oDatabase.AddInParameter(oDbCommand, "@EnviarCelular", DbType.String, item.EnviarCelular);
                oDatabase.AddInParameter(oDbCommand, "@EnviarEmail", DbType.String, item.EnviarEmail);                
                objResult.IDNotificacion = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert(Repository Notificacion) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }
    }
}
