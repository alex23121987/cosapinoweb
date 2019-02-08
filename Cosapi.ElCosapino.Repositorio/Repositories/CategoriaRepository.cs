using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public CategoriaRepository(){}

        public List<Categoria> List_Categoria_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Categoria>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Categoria_Sellst_Paginate", CommandType.StoredProcedure,
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
                        var objCategoria = new Categoria();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objCategoria.IDCategoria = DataConvert.ToInt(dataReader["N_IDCategoria"]);
                        objCategoria.Orden = DataConvert.ToInt(dataReader["N_Orden"]);
                        objCategoria.Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);
                        objCategoria.Color = DataConvert.ToString(dataReader["S_Color"]);
                        objCategoria.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : "Inactivo";
                        list.Add(objCategoria);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Categoria_Paginate(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);                
            }           
            return list;
        }

        public List<CategoriaBE> List_Categoria_APP()
        {
            var list= new List<CategoriaBE>();
            try
            {                
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Categoria_Sellst_APP");          

                using (var oReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    while (oReader.Read())
                    {
                        var objCategoria = new CategoriaBE();
                        objCategoria.CategoriaId = DataConvert.ToInt(oReader["N_IDCategoria"]);
                        objCategoria.Nombre = DataConvert.ToString(oReader["S_Descripcion"]);
                        objCategoria.Color = DataConvert.ToString(oReader["S_Color"]);
                        list.Add(objCategoria);                       
                    }
                    oReader.Close();
                }                           
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Categoria_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
         
            return list;
        }

        public Categoria Update(Categoria item)
        {
            var objResult = new Categoria();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Categoria_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDCategoria", DbType.Int32, item.IDCategoria);
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.Descripcion.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_Color", DbType.String, item.Color);
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion.Trim());
                objResult.IDCategoria = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:Update(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Categoria Insert(Categoria item)
        {
            var objResult = new Categoria();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Categoria_Ins");
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.Descripcion.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_Color", DbType.String, item.Color);
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.UsuarioCreacion.Trim());                
                objResult.IDCategoria = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:Insert(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Categoria Delete(Categoria item)
        {
            var Resultado = new Categoria();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Categoria_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDCategoria", DbType.Int32, item.IDCategoria);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion);
                Resultado.IDCategoria = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:Delete(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }       
    }
}
