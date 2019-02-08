using Cosapi.ElCosapino.Dominio.Security.Aggregates.PerfilAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.PerfilService
{
    public class PerfilAppService : IPerfilAppService
    {
        readonly IPerfilRepository _PerfilRepository;

        public PerfilAppService()
        {
            _PerfilRepository = new PerfilRepository();
        }

        public List<PerfilBE> List_Perfil_APP(int CategoriaId, int UsuarioId)
        {
            return _PerfilRepository.List_Perfil_APP(CategoriaId, UsuarioId);
        }

    }
}
