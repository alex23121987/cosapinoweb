using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.DepartamentoAgg
{
    public interface IDepartamentoRepository
    {
        List<DepartamentoBE> List_Departamento_APP();
    }
}
