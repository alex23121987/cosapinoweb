using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class VacanteRepository : IVacanteRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public VacanteRepository(){}

        public List<Vacante> List_Vacante_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Vacante>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Vacante_Sellst_Paginate", CommandType.StoredProcedure,
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
                        var objVacante = new Vacante();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objVacante.IDVacante = DataConvert.ToInt(dataReader["N_IDVacante"]);
                        objVacante.IDCategoria = DataConvert.ToInt(dataReader["N_IDCategoria"]);
                        objVacante.IDEspecialidad = DataConvert.ToInt(dataReader["N_IDEspecialidad"]);
                        objVacante.Requisitos = DataConvert.ToString(dataReader["S_Requisitos"]);
                        objVacante.NivelAcademico = DataConvert.ToString(dataReader["S_NivelAcademico"]);
                        objVacante.PostulaEn = DataConvert.ToString(dataReader["S_PostulaEn"]);
                        objVacante.Categoria = DataConvert.ToString(dataReader["S_Categoria"]);
                        objVacante.Especialidad = DataConvert.ToString(dataReader["S_Especialidad"]);
                        objVacante.FechaRegistro = DataConvert.ToString(dataReader["S_FechaRegistro"]);
                        objVacante.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : "Inactivo";
                        list.Add(objVacante);
                    }
                }            
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_Vacante_Paginate(Repository Vacante) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }           
            return list;
        }

        public DataTable DT_Vacante()
        {
            DataTable dtResult = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["BDElCosapino"].ConnectionString);

                if (cnn.State == ConnectionState.Closed) cnn.Open();

                string sqlStatement = "exec up_Vacante_Sellst_Exportar";

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
                    Mensaje = "Origen:REPOSITORY - Método:DT_Vacante(Repository Postulacion) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return dtResult;
        }

        public Vacante Update(Vacante item)
        {
            var objResult = new Vacante();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Vacante_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDVacante", DbType.Int32, item.IDVacante);
                oDatabase.AddInParameter(oDbCommand, "@S_Requisitos", DbType.String, item.Requisitos);
                oDatabase.AddInParameter(oDbCommand, "@S_NivelAcademico", DbType.String, item.NivelAcademico);
                oDatabase.AddInParameter(oDbCommand, "@S_PostulaEn", DbType.String, item.PostulaEn);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);                
                oDatabase.AddInParameter(oDbCommand, "@N_IDEspecialidad", DbType.Int32, item.IDEspecialidad);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion.Trim());
                objResult.IDVacante = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Update(Repository Vacante) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Vacante Insert(Vacante item)
        {
            var objResult = new Vacante();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Vacante_Ins");
                oDatabase.AddInParameter(oDbCommand, "@S_Requisitos", DbType.String, item.Requisitos);
                oDatabase.AddInParameter(oDbCommand, "@S_NivelAcademico", DbType.String, item.NivelAcademico);
                oDatabase.AddInParameter(oDbCommand, "@S_PostulaEn", DbType.String, item.PostulaEn);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);                
                oDatabase.AddInParameter(oDbCommand, "@N_IDEspecialidad", DbType.Int32, item.IDEspecialidad);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.Int32, Convert.ToInt32(item.UsuarioCreacion));
                objResult.IDVacante = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert(Repository Vacante) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Vacante Delete(Vacante item)
        {
            var Resultado = new Vacante();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Vacante_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDVacante", DbType.Int32, item.IDVacante);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion);
                Resultado.IDVacante = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Delete(Repository Vacante) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }
    }
}
