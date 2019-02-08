using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class LogRepository : ILogRepository
    {        
        public LogRepository(){}
        public Log Insert(Log item)
        {
            var objResult = new Log();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_Log_Ins");
                oDatabase.AddInParameter(oDbCommand, "@IDCategoria", DbType.Int32, item.IDCategoria);
                oDatabase.AddInParameter(oDbCommand, "@Mensaje", DbType.String, item.Mensaje);
                oDatabase.AddInParameter(oDbCommand, "@UsuarioCreacion", DbType.String, item.UsuarioCreacion);
                objResult.IDLog = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch 
            {                
            }
            return objResult;
        }
    }
}
