using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.AccesoService
{
    public class AccesoAppService : IAccesoAppService
    {
        readonly IAccesoRepository _AccesoRepository;

        public AccesoAppService()
        {
            _AccesoRepository = new AccesoRepository();
        }
    

        public List<Administrador> List_Acceso_Paginate(PaginateParams paginateParams)
        {
            return _AccesoRepository.List_Acceso_Paginate(paginateParams);
        }

        public Administrador Update(Administrador item)
        {
            return _AccesoRepository.Update(item);
        }       

        public Administrador Insert(Administrador item)
        {
            return _AccesoRepository.Insert(item);
        }       

        public Administrador Delete(Administrador item)
        {
            return _AccesoRepository.Delete(item);
        }                  
    }
}
