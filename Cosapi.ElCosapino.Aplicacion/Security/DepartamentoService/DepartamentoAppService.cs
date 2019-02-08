using Cosapi.ElCosapino.Dominio.Security.Aggregates.DepartamentoAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.DepartamentoService
{
    public class DepartamentoAppService : IDepartamentoAppService
    {
        readonly IDepartamentoRepository _DepartamentoRepository;

        public DepartamentoAppService()
        {
            _DepartamentoRepository = new DepartamentoRepository();
        }

        public List<DepartamentoBE> List_Departamento_APP()
        {
            return _DepartamentoRepository.List_Departamento_APP();
        }

    }
}
