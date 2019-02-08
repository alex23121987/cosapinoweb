using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PerfilAgg
{
    public interface IPerfilRepository
    {
        List<PerfilBE> List_Perfil_APP(int CategoriaId, int UsuarioId);
    }
}
