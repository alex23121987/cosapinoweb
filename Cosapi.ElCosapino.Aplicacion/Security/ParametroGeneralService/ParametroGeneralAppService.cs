using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.ParametroGeneralService
{
    public class ParametroGeneralAppService : IParametroGeneralAppService
    {
        readonly IParametroGeneralRepository _ParametroGeneralRepository;
        public ParametroGeneralAppService()
        {
            _ParametroGeneralRepository = new ParametroGeneralRepository();
        }
        public List<ParametroGeneral> List_ParametroGeneral_Paginate(PaginateParams paginateParams, int IDUserUnidad)
        {
            return _ParametroGeneralRepository.List_ParametroGeneral_Paginate(paginateParams, IDUserUnidad);
        }

        public ParametroGeneral Update(ParametroGeneral item)
        {
            return _ParametroGeneralRepository.Update(item);
        }

        public ParametroGeneral Insert(ParametroGeneral item)
        {
            return _ParametroGeneralRepository.Insert(item);
        }

        public ParametroGeneral Delete(ParametroGeneral item)
        {
            return _ParametroGeneralRepository.Delete(item);
        }

        public ParametroGeneral Activar(ParametroGeneral item)
        {
            return _ParametroGeneralRepository.Activar(item);
        }
    }
}
