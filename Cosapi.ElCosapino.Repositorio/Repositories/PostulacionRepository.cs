using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class PostulacionRepository : IPostulacionRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public List<PostulacionBE> List_Postulacion_Paginate(PaginateParams paginateParams)
        {
            var list = new List<PostulacionBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Postulacion_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("UsuarioId", SqlDbType.Int, paginateParams.UsuarioId),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objPostulacion = new PostulacionBE();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objPostulacion.FechaPostulacion = DataConvert.ToString(dataReader["S_FechaRegistro"]);
                        objPostulacion.PostulacionId = DataConvert.ToInt(dataReader["N_IDPostulacion"]);
                        objPostulacion.Categoria = DataConvert.ToString(dataReader["S_Categoria"]);
                        objPostulacion.Especialidad = DataConvert.ToString(dataReader["S_Especialidad"]);

                        objPostulacion.PostulanteNombre = DataConvert.ToString(dataReader["S_NombreCompleto"]);
                        objPostulacion.PostulanteEmail = DataConvert.ToString(dataReader["S_CorreoElectronico"]);
                        objPostulacion.PostulanteNumeroContacto1 = DataConvert.ToString(dataReader["S_NumeroContacto1"]);
                      
                        objPostulacion.Requisitos = DataConvert.ToString(dataReader["S_Requisitos"]);
                        objPostulacion.NivelAcademico = DataConvert.ToString(dataReader["S_NivelAcademico"]);
                        objPostulacion.PostulaEn = DataConvert.ToString(dataReader["S_PostulaEn"]);                       
                        list.Add(objPostulacion);
                    }
                }
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_Postulacion_Paginate(Repository Postulacion) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return list;
        }

        public DataTable DT_Postulacion(string S_FechaInicio, string S_FechaFin)
        {
            DataTable dtResult = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["BDElCosapino"].ConnectionString);

                if (cnn.State == ConnectionState.Closed) cnn.Open();

                string sqlStatement = "exec up_Postulacion_Sellst_Exportar @S_Desde = '" + S_FechaInicio + "', @S_Hasta = '" + S_FechaFin + "'";

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

        public List<PostulacionBE> List_Postulacion_APP(int UsuarioId)
        {
            var list = new List<PostulacionBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Postulacion_Sellst_APP", CommandType.StoredProcedure,                
                SQLServer.CreateParameter("UsuarioId", SqlDbType.Int, UsuarioId));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objentidad = new PostulacionBE();
                        objentidad.PerfilId = DataConvert.ToInt(dataReader["N_IDPerfil"]);
                        objentidad.PostulacionId = DataConvert.ToInt(dataReader["N_IDPostulacion"]);
                        objentidad.Especialidad = DataConvert.ToString(dataReader["S_Especialidad_Nombre"]);
                        objentidad.Categoria = DataConvert.ToString(dataReader["S_Categoria_Nombre"]);
                        objentidad.Requisitos = DataConvert.ToString(dataReader["S_Requisitos"]);
                        objentidad.NivelAcademico = DataConvert.ToString(dataReader["S_NivelAcademico"]);
                        objentidad.PostulaEn = DataConvert.ToString(dataReader["S_PostulaEn"]);
                        objentidad.Fecha = DataConvert.ToDateTime(dataReader["D_FechaPostulacion"]);
                        objentidad.UsuarioId = UsuarioId;
                        objentidad.ColorCategoria = DataConvert.ToString(dataReader["S_CategoriaColor"]);
                        
                        
                        list.Add(objentidad);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Postulacion_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return list;
        }

        public PostulacionBE Insert(PostulacionBE item)
        {
            var objResult = new PostulacionBE();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Postulacion_Ins");               
                oDatabase.AddInParameter(oDbCommand, "@PerfilId", DbType.Int32, item.PerfilId);        
                oDatabase.AddInParameter(oDbCommand, "@UsuarioId", DbType.Int32, item.UsuarioId);
               
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
    }
}
