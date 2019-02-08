using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class AccesoRepository : IAccesoRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public AccesoRepository() { }

        public List<Administrador> List_Acceso_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Administrador>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Administrador_Sellst_Paginate", CommandType.StoredProcedure,
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
                        var objAdministrador = new Administrador();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objAdministrador.AdministradorId = DataConvert.ToInt(dataReader["N_IDAdministrador"]);
                        objAdministrador.Codigo = DataConvert.ToString(dataReader["S_Codigo"]);
                        objAdministrador.Clave= DataConvert.ToString(dataReader["S_Password"]);
                        objAdministrador.IDRol = DataConvert.ToInt(dataReader["N_IDRol"]);
                        objAdministrador.Rol = DataConvert.ToString(dataReader["S_Rol"]);                                                                   
                        objAdministrador.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("ACT") ? "Activo" : "Inactivo";                    
                        list.Add(objAdministrador);
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

        public Administrador Update(Administrador item)
        {
            var objResult = new Administrador();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Administrador_Upd");
                oDatabase.AddInParameter(oDbCommand, "@AdministradorId", DbType.String, item.AdministradorId);
                oDatabase.AddInParameter(oDbCommand, "@Codigo", DbType.String, item.Codigo);
                oDatabase.AddInParameter(oDbCommand, "@Clave", DbType.String, item.Clave);
                oDatabase.AddInParameter(oDbCommand, "@IDRol", DbType.Int32, item.IDRol);
                oDatabase.AddInParameter(oDbCommand, "@Estado", DbType.String, item.Estado);
                objResult.AdministradorId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
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

        public Administrador Insert(Administrador item)
        {
            var objResult = new Administrador();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Administrador_Ins");
                oDatabase.AddInParameter(oDbCommand, "@Codigo", DbType.String, item.Codigo);
                oDatabase.AddInParameter(oDbCommand, "@Clave", DbType.String, item.Clave);
                oDatabase.AddInParameter(oDbCommand, "@IDRol", DbType.Int32, item.IDRol);
                objResult.AdministradorId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
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

        public Administrador Delete(Administrador item)
        {
            var Resultado = new Administrador();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Administrador_Del");
                oDatabase.AddInParameter(oDbCommand, "@AdministradorId", DbType.Int32, item.AdministradorId);                
                Resultado.AdministradorId = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
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
    }
}
