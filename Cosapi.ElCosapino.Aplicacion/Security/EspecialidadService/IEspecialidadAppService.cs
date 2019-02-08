using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.EspecialidadService
{
    public interface IEspecialidadAppService
    {
        List<Especialidad> List_Especialidad_Paginate(PaginateParams paginateParams);

        List<Especialidad> List_Especialidad_Paginate_Filtro(PaginateParams paginateParams, Especialidad filtro);

        List<EspecialidadBE> List_Especialidad_APP(int CategoriaId);

        Especialidad Update(Especialidad item);

        Especialidad Insert(Especialidad item);

        Especialidad Delete(Especialidad item);

        DataTable DT_Especialidad();
    }
}
