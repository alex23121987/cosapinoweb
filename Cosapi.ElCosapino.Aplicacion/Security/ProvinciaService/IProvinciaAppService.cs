using Cosapi.ElCosapino.Dominio.Security.Aggregates.ProvinciaAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.ProvinciaService
{
    public interface IProvinciaAppService
    {
        List<ProvinciaBE> List_Provincia_APP(int DepartamentoId);
    }
}
