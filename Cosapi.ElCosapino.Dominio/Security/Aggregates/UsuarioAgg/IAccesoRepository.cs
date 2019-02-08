using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg
{
    public interface IAccesoRepository
    {
        List<Administrador> List_Acceso_Paginate(PaginateParams paginateParams);

        Administrador Insert(Administrador item);

        Administrador Delete(Administrador item);

        Administrador Update(Administrador item);
    }
}
