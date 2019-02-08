using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.RolAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PermissionAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class RolRepository : IRolRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public RolRepository(){}

        public List<Rol> List_Rol_Paginate(PaginateParams paginateParams)
        {
            var list = new List<Rol>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Rol_Sellst_Paginate", CommandType.StoredProcedure,
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
                        var objRol = new Rol();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objRol.N_IDRol = DataConvert.ToInt(dataReader["N_IDRol"]);                        
                        objRol.S_Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);                        
                        objRol.S_Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objRol);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_Rol_Paginate(Repository Rol) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }          
            return list;
        }      
        public Rol Update(Rol item)
        {
            var objResult = new Rol();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDRol", DbType.Int32, item.N_IDRol);                
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.S_Descripcion.Trim());                
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.S_Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion.Trim());
                objResult.N_IDRol = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Update(Repository Rol) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public Rol Insert(Rol item)
        {
            var objResult = new Rol();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Ins");                                
                oDatabase.AddInParameter(oDbCommand, "@S_Descripcion", DbType.String, item.S_Descripcion.Trim());                
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.S_Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_Accion", DbType.String, item.S_Accion);
                oDatabase.AddInParameter(oDbCommand, "@S_IDRol", DbType.String, item.S_IDRol);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.S_UsuarioCreacion.Trim());
                objResult.N_IDRol = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert(Repository Rol) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }
        public Rol Delete(Rol item)
        {
            var Resultado = new Rol();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDRol", DbType.Int32, item.N_IDRol);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion);
                Resultado.N_IDRol = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Delete(Repository Rol) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }

        public Rol Desactivar(Rol item)
        {
            var Resultado = new Rol();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Upd_Desactivar");
                oDatabase.AddInParameter(oDbCommand, "@N_IDRol", DbType.Int32, item.N_IDRol);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion);
                Resultado.N_IDRol = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Desactivar(Repository Rol) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }

        public Rol Activar(Rol item)
        {
            var Resultado = new Rol();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Upd_ActivarRol");
                oDatabase.AddInParameter(oDbCommand, "@N_IDRol", DbType.Int32, item.N_IDRol);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.S_UsuarioModificacion);
                Resultado.N_IDRol = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Activar(Repository Rol) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }

        public List<Permission.ListPermissionTree> List_Permission_Tree(string S_IDRol)
        {
            var listPermission = new List<Permission.ListPermissionTree>();

            var oDatabase = DatabaseFactory.CreateDatabase();
            var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Sellst_OpcionSistema");
            oDatabase.AddInParameter(oDbCommand, "@S_IDRol", DbType.String, S_IDRol);            

            using (var oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var objPermission = new Permission.ListPermissionTree
                    {
                        N_ID_PERMISSION = DataConvert.ToInt32(oReader["N_ID_PERMISSION"]),
                        S_ID_RESULT = DataConvert.ToString(oReader["S_ID_RESULT"]),
                        S_NAME_RESULT = DataConvert.ToString(oReader["S_NAME_RESULT"]),
                        S_ID_PARENT_RESULT = DataConvert.ToString(oReader["S_ID_PARENT_RESULT"]),
                        S_STATUS = DataConvert.ToString(oReader["S_STATUS"]),
                        S_IDS = DataConvert.ToString(oReader["S_IDS"])
                    };
                  
                    listPermission.Add(objPermission);
                }
                oReader.Close();
            }
            return listPermission;
        }
     
        public List<Permission> PermissionTreeInsert(List<Permission> ListTree)
        {
            var lstPermission = new List<Permission>();
            foreach (var item in ListTree)
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Rol_Ins_OpcionSistema");             
                oDatabase.AddInParameter(oDbCommand, "@N_ID_ACTION", DbType.Int32, item.N_ID_ACTION);
                oDatabase.AddInParameter(oDbCommand, "@N_ID_OPTION", DbType.Int32, item.N_ID_OPTION);               
                oDatabase.AddInParameter(oDbCommand, "@S_USER_CREATION", DbType.String, item.S_USER_CREATION);              
                oDatabase.AddInParameter(oDbCommand, "@S_IDRol", DbType.String, item.S_IDRol);

                item.N_ID_PERMISSION = int.Parse(oDatabase.ExecuteScalar(oDbCommand).ToString());
                lstPermission.Add(item);
            }
            return lstPermission;
        }
    }
}
