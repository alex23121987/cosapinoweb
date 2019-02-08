using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg
{
    public interface IVacanteRepository
    {
        List<Vacante> List_Vacante_Paginate(PaginateParams paginateParams);

        Vacante Update(Vacante item);

        Vacante Insert(Vacante item);

        Vacante Delete(Vacante item);

        DataTable DT_Vacante();
    }
}
