using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.VacanteService
{
    public class VacanteAppService : IVacanteAppService
    {
        readonly IVacanteRepository _VacanteRepository;
        public VacanteAppService()
        {
            _VacanteRepository = new VacanteRepository();
        }
        public List<Vacante> List_Vacante_Paginate(PaginateParams paginateParams)
        {
            return _VacanteRepository.List_Vacante_Paginate(paginateParams);
        }

        public Vacante Update(Vacante item)
        {
            return _VacanteRepository.Update(item);
        }

        public Vacante Insert(Vacante item)
        {
            return _VacanteRepository.Insert(item);
        }

        public Vacante Delete(Vacante item)
        {
            return _VacanteRepository.Delete(item);
        }

        public DataTable DT_Vacante()
        {
            return _VacanteRepository.DT_Vacante();
        }
    }
}
