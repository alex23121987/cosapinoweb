using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.BaseAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.BaseService
{
    public class BaseAppService : IBaseAppService
    {
        readonly IBaseRepository _BaseRepository;
        public BaseAppService()
        {
            _BaseRepository = new BaseRepository();
        }    

        public List<DDLOptionsText> Lista_ParametroCombo(string sTabla, int IDUserUnidad)
        {
            return _BaseRepository.Lista_ParametroCombo(sTabla, IDUserUnidad);
        }

        public List<CHKListOptionsText> Lista_ParametroCheckBox(string sTabla, int IDUserUnidad)
        {
            return _BaseRepository.Lista_ParametroCheckBox(sTabla, IDUserUnidad);
        }

        public List<DDLOptionsText> Lista_Responsable()
        {
            return _BaseRepository.Lista_Responsable();
        }

        public List<DDLOptionsText> Lista_Especialidad(int N_IDUnidad)
        {
            return _BaseRepository.Lista_Especialidad(N_IDUnidad);
        }
      
        public List<DDLOptionsText> Lista_Especialidad_Con_ID()
        {
            return _BaseRepository.Lista_Especialidad_Con_ID();
        }

        public List<DDLOptionsText> Lista_Entregable()
        {
            return _BaseRepository.Lista_Entregable();
        }

        public List<DDLOptionsText> Lista_Perfil()
        {
            return _BaseRepository.Lista_Perfil();
        }
    }
}
