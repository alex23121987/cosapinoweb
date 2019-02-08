using Cosapi.ElCosapino.Dominio.Security.Aggregates.PermissionAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.RolAgg
{
    public interface IRolRepository
    {          
        List<Rol> List_Rol_Paginate(PaginateParams paginateParams);

        Rol Update(Rol item);

        Rol Insert(Rol item);

        Rol Delete(Rol item);

        Rol Activar(Rol item);

        Rol Desactivar(Rol item);

        List<Permission.ListPermissionTree> List_Permission_Tree(string S_IDRol);
        
        List<Permission> PermissionTreeInsert(List<Permission> ListTree);
    }
}
