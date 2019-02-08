using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.VacanteService
{
    public interface IVacanteAppService
    {
        List<Vacante> List_Vacante_Paginate(PaginateParams paginateParams);

        Vacante Update(Vacante item);

        Vacante Insert(Vacante item);

        Vacante Delete(Vacante item);

        DataTable DT_Vacante();
    }
}
