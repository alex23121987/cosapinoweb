using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ProvinciaAgg;
using Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public List<ProvinciaBE> List_Provincia_APP(int DepartamentoId)
        {
            var list = new List<ProvinciaBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Provincia_Sellst_APP", CommandType.StoredProcedure,
                SQLServer.CreateParameter("DepartamentoId", SqlDbType.Int, DepartamentoId)
                    );
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objentidad = new ProvinciaBE();
                        objentidad.ProvinciaId = DataConvert.ToInt(dataReader["N_IDProvincia"]);
                        objentidad.Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);
                        list.Add(objentidad);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Provincia_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }         
            return list;
        }
    }
}
