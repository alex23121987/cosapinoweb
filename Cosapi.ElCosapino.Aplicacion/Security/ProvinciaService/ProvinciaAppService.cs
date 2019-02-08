using Cosapi.ElCosapino.Dominio.Security.Aggregates.ProvinciaAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.ProvinciaService
{
    public class ProvinciaAppService : IProvinciaAppService
    {
        readonly IProvinciaRepository _ProvinciaRepository;

        public ProvinciaAppService()
        {
            _ProvinciaRepository = new ProvinciaRepository();
        }

        public List<ProvinciaBE> List_Provincia_APP(int DepartamentoId)
        {
            return _ProvinciaRepository.List_Provincia_APP(DepartamentoId);
        }

    }
}
