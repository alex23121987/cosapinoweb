using Cosapi.ElCosapino.Dominio.Security.Aggregates.PerfilAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.PerfilService
{
    public interface IPerfilAppService
    {
        List<PerfilBE> List_Perfil_APP(int CategoriaId,int UsuarioId);
    }
}
