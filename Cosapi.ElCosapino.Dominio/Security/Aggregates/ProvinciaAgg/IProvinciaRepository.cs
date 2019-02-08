using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.ProvinciaAgg
{
    public interface IProvinciaRepository
    {
        List<ProvinciaBE> List_Provincia_APP(int DepartamentoId);
    }
}
