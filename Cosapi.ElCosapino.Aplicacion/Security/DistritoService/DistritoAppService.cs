using Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.DistritoService
{
    public class DistritoAppService : IDistritoAppService
    {
        readonly IDistritoRepository _DistritoRepository;

        public DistritoAppService()
        {
            _DistritoRepository = new DistritoRepository();
        }

        public List<DistritoBE> List_Distrito_APP(int ProvinciaId)
        {
            return _DistritoRepository.List_Distrito_APP(ProvinciaId);
        }

    }
}
