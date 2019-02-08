using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public List<DistritoBE> List_Distrito_APP(int ProvinciaId)
        {
            var list = new List<DistritoBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Distrito_Sellst_APP", CommandType.StoredProcedure,
                SQLServer.CreateParameter("ProvinciaId", SqlDbType.Int, ProvinciaId)
                    );
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objentidad = new DistritoBE();
                        objentidad.DistritoId = DataConvert.ToInt(dataReader["N_IDDistrito"]);
                        objentidad.ProvinciaId = ProvinciaId;
                        objentidad.Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);
                        list.Add(objentidad);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Distrito_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }          
            return list;
        }
    }
}
