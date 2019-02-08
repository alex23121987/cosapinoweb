using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.AccesoService
{
    public interface IAccesoAppService
    {
        List<Administrador> List_Acceso_Paginate(PaginateParams paginateParams);

        Administrador Insert(Administrador item);

        Administrador Delete(Administrador item);

        Administrador Update(Administrador item);
    }
}
