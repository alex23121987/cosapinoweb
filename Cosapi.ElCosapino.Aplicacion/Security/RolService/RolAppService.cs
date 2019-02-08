using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.RolAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PermissionAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.RolService
{
    public class RolAppService : IRolAppService
    {
        readonly IRolRepository _RolRepository;
        public RolAppService()
        {
            _RolRepository = new RolRepository();
        }
        public List<Rol> List_Rol_Paginate(PaginateParams paginateParams)
        {
            return _RolRepository.List_Rol_Paginate(paginateParams);
        }
     
        public Rol Update(Rol item)
        {
            return _RolRepository.Update(item);
        }

        public Rol Insert(Rol item)
        {
            return _RolRepository.Insert(item);
        }

        public Rol Delete(Rol item)
        {
            return _RolRepository.Delete(item);
        }

        public Rol Desactivar(Rol item)
        {
            return _RolRepository.Desactivar(item);
        }

        public Rol Activar(Rol item)
        {
            return _RolRepository.Activar(item);
        }

        public List<Permission.ListPermissionTree> List_Permission_Tree(string S_IDRol)
        {
            return _RolRepository.List_Permission_Tree(S_IDRol);
        }
       
        public List<Permission> PermissionTreeInsert(List<Permission> item)
        {
            return _RolRepository.PermissionTreeInsert(item);
        }
    }
}
