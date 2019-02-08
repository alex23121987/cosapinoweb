using Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.DistritoService
{
    public interface IDistritoAppService
    {
        List<DistritoBE> List_Distrito_APP(int ProvinciaId);
    }
}
