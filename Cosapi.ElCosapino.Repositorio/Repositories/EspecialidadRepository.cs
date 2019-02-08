using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public EspecialidadRepository(){}
        public List<Especialidad> List_Especialidad_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Especialidad>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Especialidad_Sellst_Paginate", CommandType.StoredProcedure,
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
                        var objEspecialidad = new Especialidad();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objEspecialidad.IDEspecialidad = DataConvert.ToInt(dataReader["N_IDEspecialidad"]);
                        objEspecialidad.IDCategoria = DataConvert.ToInt(dataReader["N_IDCategoria"]);
                        objEspecialidad.Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);
                        objEspecialidad.Categoria = DataConvert.ToString(dataReader["S_Categoria"]);
                        objEspecialidad.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : "Inactivo";
                        list.Add(objEspecialidad);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Especialidad_Paginate(Repository Especialidad) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return list;
        }

        public List<EspecialidadBE> List_Especialidad_APP(int CategoriaId)
        {
            var list = new List<EspecialidadBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Especialidad_Sellst_APP", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CategoriaId", SqlDbType.Int, CategoriaId));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objEspecialidad = new EspecialidadBE();                        
                        objEspecialidad.EspecialidadId = DataConvert.ToInt(dataReader["N_IDEspecialidad"]);
                        objEspecialidad.CategoriaId = DataConvert.ToInt(dataReader["N_IDCategoria"]);
                        objEspecialidad.Nombre = DataConvert.ToString(dataReader["S_Descripcion"]);                        
                        list.Add(objEspecialidad);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Categoria_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return list;
        }

        public DataTable DT_Especialidad()
        {
            DataTable dtResult = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["BDElCosapino"].ConnectionString);

                if (cnn.State == ConnectionState.Closed) cnn.Open();

                string sqlStatement = "exec up_Especialidad_Sellst_Exportar";

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
                    Mensaje = "Origen:REPOSITORY - Método:DT_Especialidad(Repository Postulacion) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return dtResult;
        }

        public List<Especialidad> List_Especialidad_Paginate_Filtro(PaginateParams paginateParams, Especialidad filtro)
        {
            var list = new List<Especialidad>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Especialidad_Sellst_Paginate_Filtro", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("IDCategoria", SqlDbType.Int, filtro.IDCategoria),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objEspecialidad = new Especialidad();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objEspecialidad.IDEspecialidad = DataConvert.ToInt(dataReader["N_IDEspecialidad"]);
                        objEspecialidad.IDCategoria = DataConvert.ToInt(dataReader["N_IDCategoria"]);
                        objEspecialidad.Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);
                        objEspecialidad.Categoria = DataConvert.ToString(dataReader["S_Categoria"]);
                        objEspecialidad.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : "Inactivo";
                        list.Add(objEspecialidad);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Especialidad_Paginate_Filtro(Repository Especialidad) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return list;
        }

        public Especialidad Update(Especialidad item)
        {
            var objResult = new Especialidad();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Especialidad_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDEspecialidad", DbType.Int32, item.IDEspecialidad);
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.Descripcion.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);                
                oDatabase.AddInParameter(oDbCommand, "@N_IDCategoria", DbType.Int32, item.IDCategoria);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion.Trim());
                objResult.IDEspecialidad = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:Update(Repository Especialidad) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Especialidad Insert(Especialidad item)
        {
            var objResult = new Especialidad();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Especialidad_Ins");
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.Descripcion.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);                
                oDatabase.AddInParameter(oDbCommand, "@N_IDCategoria", DbType.Int32, item.IDCategoria);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.UsuarioCreacion.Trim());
                objResult.IDEspecialidad = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:Insert(Repository Especialidad) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Especialidad Delete(Especialidad item)
        {
            var Resultado = new Especialidad();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Especialidad_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDEspecialidad", DbType.Int32, item.IDEspecialidad);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion);
                Resultado.IDEspecialidad = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:Delete(Repository Especialidad) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }
    }
}
