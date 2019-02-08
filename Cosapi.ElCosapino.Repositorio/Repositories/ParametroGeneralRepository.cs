using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{

    public class ParametroGeneralRepository : IParametroGeneralRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public ParametroGeneralRepository(){}

        public List<ParametroGeneral> List_ParametroGeneral_Paginate(PaginateParams paginateParams, int IDUserUnidad)
        {
            var list = new List<ParametroGeneral>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_ParametroGeneral_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("IDUserUnidad", SqlDbType.Int, IDUserUnidad),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objParametroGeneral = new ParametroGeneral();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objParametroGeneral.N_IDParametroGeneral = DataConvert.ToInt(dataReader["N_IDParametroGeneral"]);
                        objParametroGeneral.S_IDTabla = DataConvert.ToString(dataReader["S_IDTabla"]);                        
                        objParametroGeneral.S_IDParametro = DataConvert.ToString(dataReader["S_IDParametro"]);
                        objParametroGeneral.S_Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);
                        objParametroGeneral.S_Tipo = DataConvert.ToString(dataReader["S_Tipo"]);
                        objParametroGeneral.N_Orden = DataConvert.ToInt(dataReader["N_Orden"]);
                        objParametroGeneral.S_Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objParametroGeneral);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_ParametroGeneral_Paginate(Repository ParametroGeneral) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }           
            return list;
        }
        public ParametroGeneral Update(ParametroGeneral item)
        {
            var objResult = new ParametroGeneral();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_ParametroGeneral_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDParametroGeneral", DbType.Int32, item.N_IDParametroGeneral);
                oDatabase.AddInParameter(oDbCommand, "@S_IDTabla", DbType.String, item.S_IDTabla.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_IDParametro", DbType.String, item.S_IDParametro.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.S_Descripcion.Trim());                
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.N_Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.S_Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion.Trim());
                objResult.N_IDParametroGeneral = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Update(Repository ParametroGeneral) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public ParametroGeneral Insert(ParametroGeneral item)
        {
            var objResult = new ParametroGeneral();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_ParametroGeneral_Ins");
                oDatabase.AddInParameter(oDbCommand, "@S_IDTabla", DbType.String, item.S_IDTabla.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_IDParametro", DbType.String, item.S_IDParametro.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.S_Descripcion.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Tipo", DbType.String, item.S_Tipo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.N_Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.S_Estado);
                oDatabase.AddInParameter(oDbCommand, "@N_IDUnidad", DbType.Int32, item.N_IDUnidad);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.S_UsuarioCreacion.Trim());
                objResult.N_IDParametroGeneral = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert(Repository ParametroGeneral) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }
        public ParametroGeneral Delete(ParametroGeneral item)
        {
            var Resultado = new ParametroGeneral();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_ParametroGeneral_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDParametroGeneral", DbType.Int32, item.N_IDParametroGeneral);
                oDatabase.AddInParameter(oDbCommand, "@N_IDUnidad", DbType.Int32, item.N_IDUnidad);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion);
                Resultado.N_IDParametroGeneral = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Delete(Repository ParametroGeneral) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }

        public ParametroGeneral Activar(ParametroGeneral item)
        {
            var Resultado = new ParametroGeneral();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_ParametroGeneral_Upd_ActivarParametroGeneral");
                oDatabase.AddInParameter(oDbCommand, "@N_IDParametroGeneral", DbType.Int32, item.N_IDParametroGeneral);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion);
                Resultado.N_IDParametroGeneral = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Activar(Repository ParametroGeneral) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }
    }
}
