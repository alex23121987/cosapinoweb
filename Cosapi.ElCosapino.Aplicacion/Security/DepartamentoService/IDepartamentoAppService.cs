using Cosapi.ElCosapino.Dominio.Security.Aggregates.DepartamentoAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.DepartamentoService
{
    public interface IDepartamentoAppService
    {      
        List<DepartamentoBE> List_Departamento_APP();     
    }
}
