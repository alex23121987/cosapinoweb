using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PerfilAgg;
using Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public List<PerfilBE> List_Perfil_APP(int CategoriaId, int UsuarioId)
        {
            var list = new List<PerfilBE>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_Perfil_Sellst_APP", CommandType.StoredProcedure,
                SQLServer.CreateParameter("CategoriaId", SqlDbType.Int, CategoriaId),
                SQLServer.CreateParameter("UsuarioId", SqlDbType.Int, UsuarioId));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objentidad = new PerfilBE();
                        objentidad.PerfilId = DataConvert.ToInt(dataReader["N_IDPerfil"]);
                        objentidad.EspecialidadId = DataConvert.ToInt(dataReader["N_IDEspecialidad"]); 
                        objentidad.Especialidad = DataConvert.ToString(dataReader["S_Especialidad_Nombre"]);
                        objentidad.Categoria = DataConvert.ToString(dataReader["S_Categoria_Nombre"]);
                        objentidad.Requisitos = DataConvert.ToString(dataReader["S_Requisitos"]);
                        objentidad.NivelAcademico = DataConvert.ToString(dataReader["S_NivelAcademico"]);
                        objentidad.CategoriaId = CategoriaId;
                        objentidad.PostulaEn = DataConvert.ToString(dataReader["S_PostulaEn"]);
                        list.Add(objentidad);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_Perfil_APP(Repository Categoria) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return list;
        }
    }
}
