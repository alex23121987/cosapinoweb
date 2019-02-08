using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg
{
    public interface IDistritoRepository
    {
        List<DistritoBE> List_Distrito_APP(int ProvinciaId);
    }
}
