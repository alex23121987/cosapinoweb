using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.EspecialidadService
{
    public class EspecialidadAppService : IEspecialidadAppService
    {
        readonly IEspecialidadRepository _EspecialidadRepository;
        public EspecialidadAppService()
        {
            _EspecialidadRepository = new EspecialidadRepository();
        }
        public List<Especialidad> List_Especialidad_Paginate(PaginateParams paginateParams)
        {
            return _EspecialidadRepository.List_Especialidad_Paginate(paginateParams);
        }

        public List<Especialidad> List_Especialidad_Paginate_Filtro(PaginateParams paginateParams, Especialidad filtro)
        {
            return _EspecialidadRepository.List_Especialidad_Paginate_Filtro(paginateParams, filtro);
        }

        public List<EspecialidadBE> List_Especialidad_APP(int CategoriaId)
        {
            return _EspecialidadRepository.List_Especialidad_APP(CategoriaId);
        }

        public Especialidad Update(Especialidad item)
        {
            return _EspecialidadRepository.Update(item);
        }

        public Especialidad Insert(Especialidad item)
        {
            return _EspecialidadRepository.Insert(item);
        }

        public Especialidad Delete(Especialidad item)
        {
            return _EspecialidadRepository.Delete(item);
        }

        public DataTable DT_Especialidad()
        {
            return _EspecialidadRepository.DT_Especialidad();
        }
    }
}
