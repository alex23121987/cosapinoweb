using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DepartamentoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public List<DepartamentoBE> List_Departamento_APP()
        {
            var list= new List<DepartamentoBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Departamento_Sellst_APP", CommandType.StoredProcedure);
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objentidad = new DepartamentoBE();
                        objentidad.DepartamentoId = DataConvert.ToInt(dataReader["N_IDDepartamento"]);
                        objentidad.Descripcion = DataConvert.ToString(dataReader["S_Descripcion"]);                        
                        list.Add(objentidad);
                    }
                }               
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Categoria_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }          
            return list;
        }
    }
}
